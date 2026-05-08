using System;
using System.IO;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Allow the PDF path to be supplied via command‑line arguments; fall back to the default name.
        string pdfPath = args.Length > 0 ? args[0] : "input.pdf";
        const string outputDir = "attachments";

        // Verify that the source PDF exists before attempting to bind it.
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – '{pdfPath}'. Please provide a valid path.");
            return;
        }

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputDir);

        // Initialize the extractor and bind the PDF.
        PdfExtractor extractor = new PdfExtractor();
        extractor.BindPdf(pdfPath);

        // Extract all attachments from the PDF.
        extractor.ExtractAttachment();

        // Retrieve attachment names and their data streams.
        IList<string> attachmentNames = extractor.GetAttachNames();
        MemoryStream[] attachmentStreams = extractor.GetAttachment();

        if (attachmentNames == null || attachmentStreams == null || attachmentNames.Count == 0)
        {
            Console.WriteLine("No attachments were found in the PDF.");
            return;
        }

        // Process each attachment.
        for (int i = 0; i < attachmentStreams.Length; i++)
        {
            // Get the name; fall back to a generated name if the entry is null or empty.
            string name = !string.IsNullOrEmpty(attachmentNames[i])
                ? attachmentNames[i]
                : $"attachment_{i}";

            string outPath = Path.Combine(outputDir, name);

            // Save the attachment to disk.
            using (FileStream fileStream = new FileStream(outPath, FileMode.Create, FileAccess.Write))
            {
                attachmentStreams[i].Position = 0;
                attachmentStreams[i].CopyTo(fileStream);
            }

            // Compute SHA‑256 hash of the saved file.
            string hash = ComputeSha256(outPath);
            Console.WriteLine($"{name}: {hash}");
        }
    }

    // Helper method to compute SHA‑256 hash of a file and return it as a hex string.
    private static string ComputeSha256(string filePath)
    {
        using (FileStream fileStream = File.OpenRead(filePath))
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashBytes = sha256.ComputeHash(fileStream);
            StringBuilder sb = new StringBuilder(hashBytes.Length * 2);
            foreach (byte b in hashBytes)
                sb.Append(b.ToString("x2"));
            return sb.ToString();
        }
    }
}
