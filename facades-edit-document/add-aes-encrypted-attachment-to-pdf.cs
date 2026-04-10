using System;
using System.IO;
using System.Security.Cryptography;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "output_with_attachment.pdf";
        const string originalFile   = "secret.txt";          // file to be attached
        const string attachmentName = "encrypted_secret.bin"; // name of the attached file
        const string description    = "AES‑encrypted attachment";

        // Ensure the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the file to be attached exists
        if (!File.Exists(originalFile))
        {
            Console.Error.WriteLine($"File to attach not found: {originalFile}");
            return;
        }

        // Encrypt the file content using AES‑256
        byte[] encryptedData;
        using (Aes aes = Aes.Create())
        {
            aes.KeySize = 256;               // AES‑256
            aes.GenerateKey();
            aes.GenerateIV();

            // For demonstration we prepend IV so that a consumer could decrypt later
            using (MemoryStream ms = new MemoryStream())
            {
                // Write IV first
                ms.Write(aes.IV, 0, aes.IV.Length);

                using (CryptoStream cryptoStream = new CryptoStream(ms, aes.CreateEncryptor(aes.Key, aes.IV), CryptoStreamMode.Write))
                using (FileStream fileStream = File.OpenRead(originalFile))
                {
                    fileStream.CopyTo(cryptoStream);
                }

                encryptedData = ms.ToArray();
            }
        }

        // Add the encrypted data as an attachment to the PDF
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPdfPath);

            using (MemoryStream encryptedStream = new MemoryStream(encryptedData))
            {
                // Attach the encrypted stream with a custom description
                editor.AddDocumentAttachment(encryptedStream, attachmentName, description);
            }

            // Save the modified PDF
            editor.Save(outputPdfPath);
        }

        Console.WriteLine($"Attachment added and PDF saved to '{outputPdfPath}'.");
    }
}