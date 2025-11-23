using LMS_SoulCode.Features.Certificates.DTOs;
using LMS_SoulCode.Features.Certificates.Models;
using LMS_SoulCode.Features.Certificates.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

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

        public CertificateService(ICertificateRepository repo, IWebHostEnvironment env, IHttpContextAccessor http)
        {
            _repo = repo;
            _env = env;
            _http = http;
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

            // 4) save file (local example)
            var uploads = Path.Combine(_env.WebRootPath ?? "wwwroot", "certificates");
            if (!Directory.Exists(uploads)) Directory.CreateDirectory(uploads);
            var fileName = $"certificate_{cert.CertificateCode}.pdf";
            var fullPath = Path.Combine(uploads, fileName);
            //await File.WriteAllBytesAsync(fullPath, pdfBytes);

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
            if (!File.Exists(path)) return null;
            var bytes = await File.ReadAllBytesAsync(path);
            return new FileContentResult(bytes, "application/pdf")
            {
                FileDownloadName = Path.GetFileName(path)
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
            // Simple QuestPDF document
            var doc = QuestPDF.Fluent.Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(40);
                    page.Size(PageSizes.A4);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(14));

                    page.Header()
                        .Height(80)
                        .AlignCenter()
                        .Row(row =>
                        {
                            row.RelativeItem().AlignCenter().Text("Institute Name").FontSize(20).SemiBold();
                        });

                    page.Content()
                        .PaddingVertical(10)
                        .Column(col =>
                        {
                            col.Item().AlignCenter().Text("Certificate of Completion").FontSize(24).SemiBold();
                            col.Item().Height(20);
                            col.Item().AlignCenter().Text($"This is to certify that").FontSize(14);
                            col.Item().Height(10);
                            col.Item().AlignCenter().Text($"[Student Name]") // replace with real user name if available
                                .FontSize(18).Bold();
                            col.Item().Height(10);
                            col.Item().AlignCenter().Text($"has successfully completed the course").FontSize(14);
                            col.Item().Height(8);
                            col.Item().AlignCenter().Text($"[Course Title]").FontSize(16).SemiBold();
                            col.Item().Height(20);
                            col.Item().AlignCenter().Text($"Score: {cert.Score ?? 0}").FontSize(12);
                            col.Item().Height(20);
                            col.Item().AlignCenter().Text($"Issued on: {cert.IssuedAt:yyyy-MM-dd}").FontSize(12);
                            col.Item().Height(30);

                            col.Item().Row(r =>
                            {
                                //r.RelativeColumn().AlignLeft().Text("Instructor Signature").FontSize(12);
                                //r.RelativeColumn().AlignRight().Text($"Certificate ID: {cert.CertificateCode}").FontSize(10);
                            });
                        });

                    page.Footer()
                        .Height(40)
                        .AlignCenter()
                        .Text($"Verify: {cert.VerificationUrl}").FontSize(9);
                });
            });

            var bytes = doc.GeneratePdf();
            return bytes;
        }
    }

}
