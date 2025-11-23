using Microsoft.AspNetCore.Mvc;
using LMS_SoulCode.Features.Security.Services;

namespace LMS_SoulCode.Features.Security.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CryptoController : ControllerBase
    {
        private readonly CryptographyService _crypto;

        public CryptoController(CryptographyService crypto)
        {
            _crypto = crypto;
        }

        //[HttpPost("encrypt")]
        //public IActionResult Encrypt([FromBody] object data)
        //{
        //    string encrypted = _crypto.EncryptDynamic(data);
        //    return Ok(encrypted);
        //}

        //[HttpPost("decrypt")]
        //public IActionResult Decrypt([FromBody] string encrypted)
        //{
        //    // Dynamic object me convert karega
        //    var obj = _crypto.Decrypt(encrypted);
        //    return Ok(obj);
        //}

        [HttpPost("upload-file")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file selected.");

            // 🔹 1. Read file into byte array
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            var fileBytes = ms.ToArray();
            // 🔹 2. Encrypt file content
            string encryptedData = _crypto.EncryptBytes(fileBytes);
            // 🔹 3. Identify folder based on file extension
            string ext = Path.GetExtension(file.FileName).ToLower();
            string folderName = ext switch
            {
                ".jpg" or ".jpeg" or ".png" or ".gif" => "images",
                ".mp4" or ".avi" or ".mov" => "videos",
                ".pdf" => "pdfs",
                ".doc" or ".docx" => "documents",
                ".xls" or ".xlsx" => "excels",
                _ => "others"
            };

            // 🔹 4. Create folder if not exists
            string folderPath = Path.Combine("wwwroot/uploads", folderName);
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            // 🔹 5. Save encrypted file with .enc extension
            string savePath = Path.Combine(folderPath, file.FileName + ".enc");
            await System.IO.File.WriteAllTextAsync(savePath, encryptedData);

            return Ok(new
            {
                message = "File encrypted and saved successfully.",
                folder = folderName,
                path = savePath.Replace("\\", "/")
            });
        }


        [HttpGet("download/{fileName}")]
        public async Task<IActionResult> DownloadDecryptedFile(string fileName)
        {
            try
            {
                // 🔹 Detect folder based on file extension
                string ext = Path.GetExtension(fileName).ToLower();
                string folderName = ext switch
                {
                    ".jpg" or ".jpeg" or ".png" or ".gif" => "images",
                    ".mp4" or ".avi" or ".mov" => "videos",
                    ".pdf" => "pdfs",
                    ".doc" or ".docx" => "documents",
                    ".xls" or ".xlsx" => "excels",
                    _ => "others"
                };

                var encryptedPath = Path.Combine("wwwroot/uploads", folderName, fileName + ".enc");
                if (!System.IO.File.Exists(encryptedPath))
                    return NotFound($"Encrypted file not found at path: {encryptedPath}");

                string encryptedData = await System.IO.File.ReadAllTextAsync(encryptedPath);

                byte[] decryptedBytes = _crypto.DecryptBytes(encryptedData);

                string contentType = ext switch
                {
                    ".jpg" or ".jpeg" => "image/jpeg",
                    ".png" => "image/png",
                    ".mp4" => "video/mp4",
                    ".pdf" => "application/pdf",
                    ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                    ".doc" => "application/msword",
                    ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    ".xls" => "application/vnd.ms-excel",
                    _ => "application/octet-stream"
                };

                return File(decryptedBytes, contentType, fileName);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error while decrypting and downloading file: {ex.Message}");
            }
        }


        [HttpGet("stream/{fileName}")]
        public async Task<IActionResult> StreamDecryptedVideo(string fileName)
        {
            string ext = Path.GetExtension(fileName).ToLower();

            if (ext != ".mp4")
                return BadRequest("Only mp4 streaming supported.");

            string encryptedPath = Path.Combine("wwwroot/uploads/videos", fileName + ".enc");

            if (!System.IO.File.Exists(encryptedPath))
                return NotFound($"Encrypted file not found: {encryptedPath}");

            // Read encrypted file
            string encryptedData = await System.IO.File.ReadAllTextAsync(encryptedPath);
            byte[] decryptedBytes = _crypto.DecryptBytes(encryptedData);

            // Convert decrypted bytes to memory stream
            var stream = new MemoryStream(decryptedBytes);

            return new FileStreamResult(stream, "video/mp4")
            {
                EnableRangeProcessing = true
            };
        }




    }
}
