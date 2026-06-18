using System;
using System.IO;
using System.Collections.Generic;
using System.Security.Cryptography;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputDir = "Attachments";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Bind the PDF and extract all attachments
        PdfExtractor extractor = new PdfExtractor();
        extractor.BindPdf(pdfPath);
        extractor.ExtractAttachment();

        // Retrieve attachment names and streams using the generic IList<string>
        IList<string> attachNames = extractor.GetAttachNames();
        MemoryStream[] streams = extractor.GetAttachment();

        for (int i = 0; i < streams.Length; i++)
        {
            // Guard against a possible null name returned by the extractor
            string name = attachNames[i] ?? $"attachment_{i}";
            string filePath = Path.Combine(outputDir, name);

            // Save each attachment to disk
            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                streams[i].Position = 0;
                streams[i].CopyTo(fs);
            }

            // Compute SHA‑256 hash of the saved file
            string hash = ComputeSha256(filePath);
            Console.WriteLine($"{name}: {hash}");
        }
    }

    static string ComputeSha256(string filePath)
    {
        using (FileStream fs = File.OpenRead(filePath))
        using (SHA256 sha = SHA256.Create())
        {
            byte[] hashBytes = sha.ComputeHash(fs);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
        }
    }
}
