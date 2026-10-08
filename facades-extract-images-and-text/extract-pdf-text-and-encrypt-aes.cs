using System;
using System.IO;
using System.Security.Cryptography;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputPath = "output.txt";
        const string password = "StrongPassword";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Extract text from the PDF using PdfExtractor (Aspose.Pdf.Facades)
        string extractedText;
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(pdfPath);
            extractor.ExtractText();

            using (MemoryStream ms = new MemoryStream())
            {
                extractor.GetText(ms);
                ms.Position = 0;
                using (StreamReader reader = new StreamReader(ms))
                {
                    extractedText = reader.ReadToEnd();
                }
            }
        }

        // Prepare AES encryption parameters
        byte[] salt = new byte[16];
        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        // Derive a 256‑bit key from the password using PBKDF2 (Rfc2898DeriveBytes)
        const int iterations = 100000; // iteration count
        using (var kdf = new Rfc2898DeriveBytes(password, salt, iterations))
        {
            byte[] key = kdf.GetBytes(32); // 256 bits

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.GenerateIV();
                byte[] iv = aes.IV;

                // Write encrypted data (salt + IV + ciphertext) to the output file
                using (FileStream outFile = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    outFile.Write(salt, 0, salt.Length);
                    outFile.Write(iv, 0, iv.Length);

                    using (CryptoStream cryptoStream = new CryptoStream(outFile, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    using (StreamWriter writer = new StreamWriter(cryptoStream))
                    {
                        writer.Write(extractedText);
                    }
                }
            }
        }

        Console.WriteLine($"Extracted text encrypted and saved to '{outputPath}'.");
    }
}