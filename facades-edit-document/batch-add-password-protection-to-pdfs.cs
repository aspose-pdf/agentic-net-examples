using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string folderPath = "secure";
        const string userPassword = "MySecretPassword";

        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        string[] pdfFiles = Directory.GetFiles(folderPath, "*.pdf", SearchOption.TopDirectoryOnly);
        foreach (string inputPath in pdfFiles)
        {
            string fileName = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(folderPath, $"{fileName}_protected.pdf");

            // Encrypt each PDF with the same user password, random owner password, printing allowed, 256‑bit AES
            using (PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath))
            {
                bool encrypted = fileSecurity.EncryptFile(userPassword, null, DocumentPrivilege.Print, KeySize.x256);
                if (!encrypted)
                {
                    Console.Error.WriteLine($"Encryption failed for: {inputPath}");
                }
            }
        }

        Console.WriteLine("Batch encryption completed.");
    }
}