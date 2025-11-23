namespace LMS_SoulCode.Features.Certificates.Models
{
    public class Certificate
    {
        public int Id { get; set; }
        public string CertificateCode { get; set; } = null!; // e.g. short GUID or custom
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public decimal? Score { get; set; } // optional
        public DateTime IssuedAt { get; set; } = DateTime.UtcNow;
        public string FilePath { get; set; } = null!; // path or blob url
        public bool IsRevoked { get; set; } = false;
        public string? VerificationUrl => $"http://localhost:5209/api/certificates/validate/{CertificateCode}";
    }   
}
