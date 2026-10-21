using System;
using System.IO;
using System.Diagnostics;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF files to encrypt
        string[] pdfFiles = { "doc1.pdf", "doc2.pdf", "doc3.pdf" };

        // Encryption parameters
        const string userPassword = "user123";
        const string ownerPassword = "owner123";
        Permissions permissions = Permissions.PrintDocument | Permissions.ExtractContent;
        CryptoAlgorithm algorithm = CryptoAlgorithm.AESx256;

        foreach (string inputPath in pdfFiles)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            string outputPath = Path.Combine(
                Path.GetDirectoryName(inputPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(inputPath) + "_encrypted.pdf");

            // Measure encryption time
            Stopwatch sw = Stopwatch.StartNew();

            // Load the PDF, encrypt it, and save the result
            Document doc = new Document(inputPath);
            doc.Encrypt(userPassword, ownerPassword, permissions, algorithm);
            doc.Save(outputPath);

            sw.Stop();

            Console.WriteLine($"Encrypted '{Path.GetFileName(inputPath)}' in {sw.ElapsedMilliseconds} ms. Output: {outputPath}");
        }
    }
}