using System;
using System.IO;
using System.Security.Cryptography;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";

        // Output encrypted file path (the .txt content will be AES‑encrypted)
        const string encryptedPath = "output.txt.enc";

        // Password used to derive the AES key (in a real scenario use a secure password)
        const string password = "myStrongPassword";

        // Ensure the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Extract text from the PDF using PdfExtractor (Facades API)
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(pdfPath);

            // Extract text using Unicode encoding (default overload)
            extractor.ExtractText();

            // Retrieve the extracted text into a memory stream
            using (MemoryStream textStream = new MemoryStream())
            {
                extractor.GetText(textStream);
                byte[] plainBytes = textStream.ToArray();

                // Derive a 256‑bit AES key and a 128‑bit IV from the password
                byte[] key;
                byte[] iv;
                using (var derive = new Rfc2898DeriveBytes(password, 16, 10000))
                {
                    key = derive.GetBytes(32); // 256‑bit key
                    iv  = derive.GetBytes(16); // 128‑bit IV
                }

                // Encrypt the plain text bytes with AES and write to the output file
                using (Aes aes = Aes.Create())
                {
                    aes.Key = key;
                    aes.IV = iv;
                    aes.Padding = PaddingMode.PKCS7;

                    using (FileStream outFile = new FileStream(encryptedPath, FileMode.Create, FileAccess.Write))
                    using (CryptoStream crypto = new CryptoStream(outFile, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        crypto.Write(plainBytes, 0, plainBytes.Length);
                        crypto.FlushFinalBlock();
                    }
                }
            }
        }

        Console.WriteLine($"Text extracted from '{pdfPath}' and AES‑encrypted to '{encryptedPath}'.");
    }
}
