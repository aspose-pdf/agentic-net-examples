using System;
using System.IO;
using System.Security.Cryptography;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";               // source PDF
        const string encryptedTxtPath = "output.enc";     // encrypted text file

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Extract text from PDF into a memory buffer
        byte[] plainTextBytes;
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(pdfPath);          // bind the PDF file
            extractor.ExtractText();             // use default Unicode extraction
            using (MemoryStream textStream = new MemoryStream())
            {
                extractor.GetText(textStream);   // write extracted text to stream
                plainTextBytes = textStream.ToArray();
            }
        }

        // Encrypt the extracted text using AES-256 (CBC) and write to file
        using (Aes aes = Aes.Create())
        {
            aes.KeySize = 256;                   // AES-256
            aes.GenerateKey();                   // random key
            aes.GenerateIV();                    // random IV

            using (FileStream outFile = new FileStream(encryptedTxtPath, FileMode.Create, FileAccess.Write))
            {
                // Store key length, key, IV length, IV before ciphertext (for demo purposes)
                outFile.Write(BitConverter.GetBytes(aes.Key.Length), 0, sizeof(int));
                outFile.Write(aes.Key, 0, aes.Key.Length);
                outFile.Write(BitConverter.GetBytes(aes.IV.Length), 0, sizeof(int));
                outFile.Write(aes.IV, 0, aes.IV.Length);

                // Encrypt the plaintext and write ciphertext
                using (CryptoStream cryptoStream = new CryptoStream(outFile, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                }
            }
        }

        Console.WriteLine($"Text extracted from '{pdfPath}' and encrypted to '{encryptedTxtPath}'.");
    }
}