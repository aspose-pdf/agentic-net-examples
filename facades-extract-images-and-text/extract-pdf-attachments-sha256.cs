using System;
using System.IO;
using System.Collections.Generic;
using System.Security.Cryptography;
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

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Bind the PDF and extract all attachments
        var extractor = new PdfExtractor();
        extractor.BindPdf(pdfPath);
        extractor.ExtractAttachment();

        // Retrieve attachment names (generic IList<string>) and their data streams
        IList<string> attachNames = extractor.GetAttachNames();
        MemoryStream[] streams = extractor.GetAttachment();

        if (attachNames == null || streams == null)
        {
            Console.WriteLine("No attachments found.");
            return;
        }

        // Process each attachment – guard against mismatched counts and null names
        int count = Math.Min(attachNames.Count, streams.Length);
        for (int i = 0; i < count; i++)
        {
            string? name = attachNames[i];
            if (string.IsNullOrEmpty(name))
                continue; // skip null/empty names

            string filePath = Path.Combine(outputDir, name);

            // Save the attachment to a file
            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                streams[i].Position = 0;
                streams[i].CopyTo(fs);
            }

            // Compute SHA‑256 hash of the saved file
            byte[] hashBytes;
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (SHA256 sha256 = SHA256.Create())
            {
                hashBytes = sha256.ComputeHash(fs);
            }

            // Convert hash to a hex string
            string hashHex = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();

            Console.WriteLine($"{name}: {hashHex}");
        }
    }
}
