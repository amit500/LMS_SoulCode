using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace LMS_SoulCode.Features.Security.Services
{
    public class CryptographyService
    {
        private readonly byte[] _key;

        public CryptographyService(IConfiguration config)
        {
            // Appsettings ya environment me key rakho (base64 me)
            var keyString = config["CryptoSettings:KeyBase64"];
            _key = Convert.FromBase64String(keyString ?? throw new Exception("Missing key"));
        }

        // 🔸 String encrypt karne ke liye
        public string Encrypt(string plainText)
        {
            var bytes = Encoding.UTF8.GetBytes(plainText);
            return EncryptBytes(bytes);
        }

        // 🔸 String decrypt karne ke liye
        public string Decrypt(string encryptedText)
        {
            var bytes = DecryptBytes(encryptedText);
            return Encoding.UTF8.GetString(bytes);
        }

        // 🔸 Dynamic object encrypt
        public string EncryptDynamic(object data)
        {
            string json = JsonSerializer.Serialize(data);
            return Encrypt(json);
        }

        // 🔸 Dynamic decrypt (return object)
        public T DecryptDynamic<T>(string encryptedText)
        {
            string json = Decrypt(encryptedText);
            return JsonSerializer.Deserialize<T>(json)!;
        }

        // Internal AES-GCM encrypt bytes
        public string EncryptBytes(byte[] plainBytes)
        {
            byte[] iv = new byte[12];
            RandomNumberGenerator.Fill(iv);

            byte[] cipher = new byte[plainBytes.Length];
            byte[] tag = new byte[16];

            using var aes = new AesGcm(_key);
            aes.Encrypt(iv, plainBytes, cipher, tag);

            byte[] combined = new byte[iv.Length + tag.Length + cipher.Length];
            Buffer.BlockCopy(iv, 0, combined, 0, iv.Length);
            Buffer.BlockCopy(tag, 0, combined, iv.Length, tag.Length);
            Buffer.BlockCopy(cipher, 0, combined, iv.Length + tag.Length, cipher.Length);

            return Convert.ToBase64String(combined);
        }

        public byte[] DecryptBytes(string encrypted)
        {
            byte[] combined = Convert.FromBase64String(encrypted);
            byte[] iv = new byte[12];
            byte[] tag = new byte[16];
            byte[] cipher = new byte[combined.Length - iv.Length - tag.Length];

            Buffer.BlockCopy(combined, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(combined, iv.Length, tag, 0, tag.Length);
            Buffer.BlockCopy(combined, iv.Length + tag.Length, cipher, 0, cipher.Length);

            byte[] plain = new byte[cipher.Length];
            using var aes = new AesGcm(_key);
            aes.Decrypt(iv, cipher, tag, plain);
            return plain;
        }
    }
}
