using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for input PDF, intermediate text, and final encrypted file
        const string pdfPath = "input.pdf";
        const string txtPath = "extracted.txt";
        const string encryptedPath = "extracted.enc";

        // Password used to derive the AES key (in real scenarios use a stronger KDF and random IV)
        const string password = "StrongPassword123";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // ---------- Extract text from PDF using PdfExtractor (Facade API) ----------
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Load the PDF document
            extractor.BindPdf(pdfPath);

            // Perform text extraction (Unicode encoding by default)
            extractor.ExtractText();

            // Save extracted text to a temporary .txt file
            extractor.GetText(txtPath);
        }

        // ---------- Encrypt the extracted text file using AES ----------
        // Read the plain text bytes
        byte[] plainBytes = File.ReadAllBytes(txtPath);

        // Derive a 256‑bit key from the password using SHA‑256
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] key = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

            // For simplicity, use the first 16 bytes of the key as IV (not recommended for production)
            byte[] iv = new byte[16];
            Array.Copy(key, iv, iv.Length);

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                // Write encrypted data to the output file
                using (FileStream outFile = new FileStream(encryptedPath, FileMode.Create, FileAccess.Write))
                using (CryptoStream cryptoStream = new CryptoStream(outFile, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainBytes, 0, plainBytes.Length);
                }
            }
        }

        // Optionally delete the intermediate plain‑text file
        try { File.Delete(txtPath); } catch { }

        Console.WriteLine($"Text extracted and encrypted successfully: {encryptedPath}");
    }
}