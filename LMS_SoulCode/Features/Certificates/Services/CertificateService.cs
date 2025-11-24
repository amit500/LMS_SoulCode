using LMS_SoulCode.Features.Certificates.DTOs;
using LMS_SoulCode.Features.Certificates.Models;
using LMS_SoulCode.Features.Certificates.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using BCrypt.Net;
using LMS_SoulCode.Features.Security.Services;

namespace LMS_SoulCode.Features.Certificates.Services
{
    public interface ICertificateService
    {
        Task<CertificateDto> GenerateCertificateAsync(CreateCertificateRequest request);
        Task<(bool valid, CertificateDto? dto)> ValidateByCodeAsync(string code);
        Task<FileResult?> GetPdfAsync(int id); // for download
        Task<IEnumerable<CertificateDto>> GetCertificatesByUserAsync(int userId);
        Task<IEnumerable<CertificateDto>> GetAllCertificatesAsync();
    }
    public class CertificateService : ICertificateService
    {
        private readonly ICertificateRepository _repo;
        private readonly IWebHostEnvironment _env; // for saving local files
        private readonly IHttpContextAccessor _http; // to build absolute url if needed
        private readonly CryptographyService _crypto;

        public CertificateService(ICertificateRepository repo, IWebHostEnvironment env, IHttpContextAccessor http, CryptographyService crypto)
        {
            _repo = repo;
            _env = env;
            _http = http;
            _crypto = crypto;
        }

        public async Task<CertificateDto> GenerateCertificateAsync(CreateCertificateRequest request)
        {
            // 1) (Optional) Validate completion logic: check if user completed course
            // assume validated or caller verified before calling

            // 2) create metadata
            var code = GenerateShortCode();
            var cert = new Certificate
            {
                CertificateCode = code,
                UserId = request.UserId,
                CourseId = request.CourseId,
                Score = request.Score,
                IssuedAt = DateTime.UtcNow
            };

            // 3) generate pdf bytes
            var pdfBytes = GeneratePdfBytes(cert);
            string encryptedBase64 = _crypto.EncryptBytes(pdfBytes);

            // 4) save file (local example)
            var uploads = Path.Combine(_env.WebRootPath ?? "wwwroot", "certificates");
            if (!Directory.Exists(uploads)) Directory.CreateDirectory(uploads);
            var fileName = $"certificate_{cert.CertificateCode}.pdf.enc";
            var fullPath = Path.Combine(uploads, fileName);
            await File.WriteAllBytesAsync(fullPath, pdfBytes);

            cert.FilePath = $"/certificates/{fileName}"; // relative url; use blob url if stored in cloud

            await _repo.AddAsync(cert);

            return new CertificateDto
            {
                Id = cert.Id,
                CertificateCode = cert.CertificateCode,
                UserId = cert.UserId,
                CourseId = cert.CourseId,
                Score = cert.Score,
                IssuedAt = cert.IssuedAt,
                FileUrl = cert.FilePath
            };
        }

        public async Task<(bool valid, CertificateDto? dto)> ValidateByCodeAsync(string code)
        {
            var cert = await _repo.GetByCodeAsync(code);
            if (cert == null || cert.IsRevoked) return (false, null);

            var dto = new CertificateDto
            {
                Id = cert.Id,
                CertificateCode = cert.CertificateCode,
                UserId = cert.UserId,
                CourseId = cert.CourseId,
                Score = cert.Score,
                IssuedAt = cert.IssuedAt,
                FileUrl = cert.FilePath
            };
            return (true, dto);
        }

        public async Task<FileResult?> GetPdfAsync(int id)
        {
            var cert = await _repo.GetByIdAsync(id);
            if (cert == null) return null;

            var path = Path.Combine(_env.WebRootPath ?? "wwwroot", cert.FilePath.TrimStart('/'));
            if (!System.IO.File.Exists(path)) return null;

            byte[] encryptedBytes = await System.IO.File.ReadAllBytesAsync(path);
            string encryptedBase64 = Convert.ToBase64String(encryptedBytes);

            byte[] plainBytes = _crypto.DecryptBytes(encryptedBase64);

            return new FileContentResult(plainBytes, "application/pdf")
            {
                FileDownloadName = Path.GetFileName(path).Replace(".enc", "")
            };
        }


        // helpers
        private string GenerateShortCode()
        {
            // generate short friendly code (8 chars)
            return Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();
        }
        public async Task<IEnumerable<CertificateDto>> GetCertificatesByUserAsync(int userId)
        {
            var list = await _repo.GetByUserIdAsync(userId);

            return list.Select(c => new CertificateDto
            {
                Id = c.Id,
                CertificateCode = c.CertificateCode,
                UserId = c.UserId,
                CourseId = c.CourseId,
                Score = c.Score,
                IssuedAt = c.IssuedAt,
                FileUrl = c.FilePath
            }).ToList();
        }

        public async Task<IEnumerable<CertificateDto>> GetAllCertificatesAsync()
        {
            var list = await _repo.GetAllAsync();

            return list.Select(c => new CertificateDto
            {
                Id = c.Id,
                CertificateCode = c.CertificateCode,
                UserId = c.UserId,
                CourseId = c.CourseId,
                Score = c.Score,
                IssuedAt = c.IssuedAt,
                FileUrl = c.FilePath
            }).ToList();
        }

        private byte[] GeneratePdfBytes(Certificate cert)
        {
            var document = QuestPDF.Fluent.Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(50);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(14));

                    page.Header()
                        .Text("Certificate of Completion")
                        .FontSize(24)
                        .Bold()
                        .AlignCenter();

                    page.Content().Column(col =>
                    {
                        col.Item().Text($"This is to certify that").AlignCenter();
                        col.Item().Text("[Student Name]").FontSize(20).Bold().AlignCenter();
                        col.Item().Text("has successfully completed the course").AlignCenter();
                        col.Item().Text("[Course Title]").FontSize(18).SemiBold().AlignCenter();
                        col.Item().Text($"Score: {cert.Score ?? 0}").AlignCenter();
                        col.Item().Text($"Issued On: {cert.IssuedAt:yyyy-MM-dd}").AlignCenter();
                        col.Item().Text($"Certificate Code: {cert.CertificateCode}").AlignCenter();
                    });

                    page.Footer()
                        .AlignCenter()
                        .Text("Verification URL: http://localhost:5209/verify/" + cert.CertificateCode)
                        .FontSize(10);
                });
            });

            return document.GeneratePdf();
        }

    }

}
