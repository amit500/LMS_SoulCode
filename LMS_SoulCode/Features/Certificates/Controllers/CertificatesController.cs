using LMS_SoulCode.Features.Certificates.DTOs;
using LMS_SoulCode.Features.Certificates.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CertificatesController : ControllerBase
{
    private readonly ICertificateService _service;
    public CertificatesController(ICertificateService service) => _service = service;

    [HttpPost("generate")]
    public async Task<IActionResult> Generate([FromBody] CreateCertificateRequest req)
    {
        // optional: check permissions or ensure caller is system/instructor
        var cert = await _service.GenerateCertificateAsync(req);
        return Ok(cert);
    }

    [HttpGet("{id}/download")]
    public async Task<IActionResult> Download(int id)
    {
        var fileResult = await _service.GetPdfAsync(id);
        if (fileResult == null) return NotFound();
        return fileResult;
    }

    [HttpGet("validate/{code}")]
    public async Task<IActionResult> Validate(string code)
    {
        var (valid, dto) = await _service.ValidateByCodeAsync(code);
        if (!valid) return NotFound(new { message = "Certificate not found or revoked." });
        return Ok(dto);
    }

    // optional revoke endpoint
    [HttpPost("{id}/revoke")]
    public async Task<IActionResult> Revoke(int id)
    {
        // implement revoke logic: set IsRevoked
        // check admin/auth
        return NoContent();
    }

    [HttpGet("Certificate/{userId}")]
    public async Task<IActionResult> GetByUserId(int userId)
    {
        var certs = await _service.GetCertificatesByUserAsync(userId);
        return Ok(certs);
    }
    [HttpGet("list")]
    public async Task<IActionResult> GetAll()
    {
        var certs = await _service.GetAllCertificatesAsync();
        return Ok(certs);
    }
}
