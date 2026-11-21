using System;
using System.IO;
using System.Diagnostics;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing PDFs to encrypt
        const string inputFolder = @"C:\PdfInput";
        // Folder where encrypted PDFs will be saved
        const string outputFolder = @"C:\PdfEncrypted";

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        // Permissions to allow printing and content extraction
        Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

        // User and owner passwords for encryption
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputFolder, $"{fileName}_enc.pdf");

            Stopwatch sw = Stopwatch.StartNew();

            // Load the PDF, encrypt it, and save the result
            using (Document pdfDoc = new Document(inputPath))
            {
                pdfDoc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);
                pdfDoc.Save(outputPath);
            }

            sw.Stop();
            Console.WriteLine($"Encrypted '{fileName}.pdf' in {sw.ElapsedMilliseconds} ms.");
        }

        Console.WriteLine("All files processed.");
    }
}