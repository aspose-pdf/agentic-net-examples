using System;
using System.IO;
using System.Security.Cryptography;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath = "input.pdf";
        const string outputTxtPath = "extracted.txt";
        const string encryptedPath = "extracted.enc";

        // Simple password for key derivation – in real scenarios use a strong secret
        const string password = "StrongPassword123";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.WriteLine($"Input PDF file '{inputPdfPath}' not found.");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Extract all text from the PDF using Aspose.Pdf.Facades.PdfExtractor
        // -----------------------------------------------------------------
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF file
            extractor.BindPdf(inputPdfPath);

            // Extract text using Unicode encoding (default)
            extractor.ExtractText();

            // Save the extracted text to a temporary .txt file
            extractor.GetText(outputTxtPath);
        }

        // -----------------------------------------------------------------
        // 2. Encrypt the resulting .txt file with AES (CBC, 256‑bit key)
        // -----------------------------------------------------------------
        // Read the plain text bytes
        byte[] plainBytes = File.ReadAllBytes(outputTxtPath);

        // Derive a 256‑bit key and a 128‑bit IV from the password (using PBKDF2 with a random salt)
        byte[] salt = new byte[16];
        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        using (Aes aes = Aes.Create())
        {
            aes.KeySize = 256;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            // Derive key and IV using the instance-based Rfc2898DeriveBytes (compatible with all .NET versions)
            using (var kdf = new Rfc2898DeriveBytes(password, salt, 100_000, HashAlgorithmName.SHA256))
            {
                aes.Key = kdf.GetBytes(aes.KeySize / 8);
                aes.IV = kdf.GetBytes(aes.BlockSize / 8);
            }

            // Create the output file and write the salt first
            using (FileStream outStream = new FileStream(encryptedPath, FileMode.Create, FileAccess.Write))
            {
                outStream.Write(salt, 0, salt.Length); // prepend salt

                // Create a CryptoStream that encrypts data as it is written
                using (CryptoStream cryptoStream = new CryptoStream(outStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainBytes, 0, plainBytes.Length);
                }
            }
        }

        // Optional: delete the intermediate plain text file
        try { File.Delete(outputTxtPath); } catch { /* ignore */ }

        Console.WriteLine($"Text extracted from '{inputPdfPath}' and encrypted to '{encryptedPath}'.");
    }
}
