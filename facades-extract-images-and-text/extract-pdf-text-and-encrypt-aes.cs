using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string txtPath = "extracted.txt";
        const string encryptedPath = "extracted_encrypted.bin";
        const string password = "StrongPassword123";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Extract text from PDF using Aspose.Pdf.Facades.PdfExtractor
        string extractedText;
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(pdfPath);
            extractor.ExtractText();

            // GetText requires a destination stream
            using (MemoryStream textStream = new MemoryStream())
            {
                extractor.GetText(textStream);
                textStream.Position = 0;
                using (StreamReader reader = new StreamReader(textStream, Encoding.UTF8))
                {
                    extractedText = reader.ReadToEnd();
                }
            }
        }

        // Save extracted text to a temporary .txt file
        File.WriteAllText(txtPath, extractedText);

        // Derive AES key and IV from password (simple SHA-256 based derivation)
        byte[] key, iv;
        using (SHA256 sha = SHA256.Create())
        {
            key = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            iv = new byte[16];
            Array.Copy(key, iv, 16);
        }

        // Encrypt the .txt file using AES (CBC, PKCS7 padding)
        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using (FileStream inputFile = new FileStream(txtPath, FileMode.Open, FileAccess.Read))
            using (FileStream outputFile = new FileStream(encryptedPath, FileMode.Create, FileAccess.Write))
            using (CryptoStream cryptoStream = new CryptoStream(outputFile, aes.CreateEncryptor(), CryptoStreamMode.Write))
            {
                inputFile.CopyTo(cryptoStream);
            }
        }

        // Clean up the plain text file
        try { File.Delete(txtPath); } catch { }

        Console.WriteLine($"Text extracted and encrypted to '{encryptedPath}'.");
    }
}
