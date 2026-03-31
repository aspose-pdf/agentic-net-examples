using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputDirectory = "input-pdfs";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Directory not found: {inputDirectory}");
            return;
        }

        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf");
        // Evaluation mode allows a maximum of 4 collection elements.
        int filesToProcess = Math.Min(pdfFiles.Length, 4);
        for (int i = 0; i < filesToProcess; i++)
        {
            string inputPath = pdfFiles[i];
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
            string outputFile = fileNameWithoutExt + "_encrypted.pdf";

            using (PdfFileSecurity security = new PdfFileSecurity())
            {
                security.BindPdf(inputPath);
                bool encrypted = security.EncryptFile(
                    userPassword,
                    ownerPassword,
                    DocumentPrivilege.Print,
                    KeySize.x256,
                    Algorithm.AES);

                if (encrypted)
                {
                    security.Save(outputFile);
                    Console.WriteLine($"Encrypted: {outputFile}");
                }
                else
                {
                    Console.Error.WriteLine($"Failed to encrypt: {inputPath}");
                }
            }
        }
    }
}
