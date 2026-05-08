using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Aspose.Pdf.Facades; // PdfExtractor resides here

class Program
{
    static void Main()
    {
        const string pdfPath        = "input.pdf";          // source PDF
        const string txtPath        = "extracted.txt";      // intermediate plain text
        const string encryptedPath  = "extracted.enc";      // AES‑encrypted output
        const string password       = "StrongPassword123";  // password for encryption

        // Verify that the source PDF exists before proceeding
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"Source PDF '{pdfPath}' not found. Operation aborted.");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Extract all text from the PDF using Aspose.Pdf.Facades.PdfExtractor
        // -----------------------------------------------------------------
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(pdfPath);

            // Extract text using Unicode encoding (default)
            extractor.ExtractText();

            // Save the extracted text to a temporary .txt file
            extractor.GetText(txtPath);
        }

        // ---------------------------------------------------------------
        // 2. Encrypt the resulting .txt file with AES (CBC, 256‑bit key)
        // ---------------------------------------------------------------
        // Derive a 256‑bit key and a 128‑bit IV from the password using the
        // recommended static Pbkdf2 method (Rfc2898DeriveBytes constructors are obsolete).
        byte[] salt = GenerateRandomBytes(16); // random salt stored with the ciphertext
        const int iterations = 100_000;
        byte[] key = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, HashAlgorithmName.SHA256, 32); // 256‑bit key
        byte[] iv  = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, HashAlgorithmName.SHA256, 16); // 128‑bit IV

        // Read the plain‑text bytes
        byte[] plainBytes = File.ReadAllBytes(txtPath);

        // Perform AES encryption
        byte[] cipherBytes;
        using (Aes aes = Aes.Create())
        {
            aes.KeySize   = 256;
            aes.BlockSize = 128;
            aes.Mode      = CipherMode.CBC;
            aes.Padding   = PaddingMode.PKCS7;
            aes.Key       = key;
            aes.IV        = iv;

            using (ICryptoTransform encryptor = aes.CreateEncryptor())
            using (MemoryStream msCipher = new MemoryStream())
            {
                // Prepend the salt so it can be used during decryption
                msCipher.Write(salt, 0, salt.Length);
                using (CryptoStream cs = new CryptoStream(msCipher, encryptor, CryptoStreamMode.Write))
                {
                    cs.Write(plainBytes, 0, plainBytes.Length);
                }
                cipherBytes = msCipher.ToArray();
            }
        }

        // Write the encrypted data to the output file
        File.WriteAllBytes(encryptedPath, cipherBytes);

        // Optional: delete the intermediate plain‑text file for security
        try { File.Delete(txtPath); } catch { /* ignore any error */ }

        Console.WriteLine($"Text extracted from '{pdfPath}' and encrypted to '{encryptedPath}'.");
    }

    // Helper: generate cryptographically strong random bytes
    private static byte[] GenerateRandomBytes(int count)
    {
        byte[] bytes = new byte[count];
        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(bytes);
        }
        return bytes;
    }
}
