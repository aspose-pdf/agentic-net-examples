using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string plainTextFile = "extracted.txt";
        const string encryptedFile = "encrypted.bin";
        const string password = "StrongPassword123";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine("Input PDF not found: " + inputPdf);
            return;
        }

        // Extract text from PDF
        PdfExtractor extractor = new PdfExtractor();
        extractor.BindPdf(inputPdf);
        extractor.ExtractText();
        extractor.GetText(plainTextFile);

        // Read extracted text
        string plainText = File.ReadAllText(plainTextFile, Encoding.UTF8);
        byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

        // Derive key and IV from password (SHA-256 hash, first 16 bytes as IV)
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] key = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            byte[] iv = new byte[16];
            Array.Copy(key, iv, 16);

            using (Aes aes = Aes.Create())
            {
                aes.KeySize = 256;
                aes.Key = key;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using (FileStream encryptedStream = new FileStream(encryptedFile, FileMode.Create, FileAccess.Write))
                using (CryptoStream cryptoStream = new CryptoStream(encryptedStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainBytes, 0, plainBytes.Length);
                }
            }
        }

        // Delete temporary plain text file
        try
        {
            File.Delete(plainTextFile);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Could not delete temporary file: " + ex.Message);
        }

        Console.WriteLine("Text extracted and encrypted to " + encryptedFile);
    }
}