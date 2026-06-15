using System;
using System.IO;
using System.Diagnostics;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // List of PDF files to encrypt
        string[] pdfFiles = { "file1.pdf", "file2.pdf", "file3.pdf" };
        // Directory where encrypted PDFs will be saved
        string outputDir = "Encrypted";
        Directory.CreateDirectory(outputDir);

        foreach (string inputPath in pdfFiles)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            string fileName = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputDir, $"{fileName}_enc.pdf");

            // Start timing
            Stopwatch sw = Stopwatch.StartNew();

            // Encrypt using PdfFileSecurity facade
            using (PdfFileSecurity security = new PdfFileSecurity(inputPath, outputPath))
            {
                // Encrypt with user password, owner password, desired privilege and key size
                security.EncryptFile(
                    userPassword: "user123",
                    ownerPassword: "owner123",
                    privilege: DocumentPrivilege.Print,
                    keySize: KeySize.x256);
            }

            // Stop timing
            sw.Stop();
            Console.WriteLine($"Encrypted '{inputPath}' to '{outputPath}' in {sw.ElapsedMilliseconds} ms.");
        }
    }
}