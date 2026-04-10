using System;
using System.Diagnostics;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF files to encrypt
        string[] inputFiles = { "file1.pdf", "file2.pdf" };

        // Passwords for encryption
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        foreach (string inputPath in inputFiles)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            // Output path for the encrypted PDF
            string outputPath = Path.Combine(
                Path.GetDirectoryName(inputPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(inputPath) + "_encrypted.pdf");

            // Measure encryption time
            Stopwatch timer = Stopwatch.StartNew();

            // Load the PDF document and encrypt it using the correct Aspose.Pdf API
            Document pdfDoc = new Document(inputPath);
            pdfDoc.Encrypt(
                userPassword,
                ownerPassword,
                Permissions.PrintDocument,   // use Permissions enum
                CryptoAlgorithm.AESx256);    // use CryptoAlgorithm enum
            pdfDoc.Save(outputPath);

            timer.Stop();
            Console.WriteLine($"Encrypted '{inputPath}' to '{outputPath}' in {timer.Elapsed.TotalMilliseconds} ms.");
        }
    }
}