using System;
using System.IO;
using System.Collections.Generic;
using System.Security.Cryptography;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the source PDF that contains attachments
        const string pdfPath = "input.pdf";

        // Directory where extracted attachments will be saved
        const string outputDir = "ExtractedAttachments";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Use PdfExtractor to extract attachments
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file
            extractor.BindPdf(pdfPath);

            // Extract all attachments from the document
            extractor.ExtractAttachment();

            // Get attachment names and their corresponding streams
            IList<string> attachNames = extractor.GetAttachNames();          // IList<string>
            MemoryStream[] attachStreams = extractor.GetAttachment(); // array of streams

            // Iterate over each attachment, save it to disk, and compute its SHA‑256 hash
            for (int i = 0; i < attachStreams.Length; i++)
            {
                string? fileName = attachNames[i];
                if (string.IsNullOrEmpty(fileName))
                {
                    // Skip entries with no name (should not happen, but guard against null)
                    continue;
                }

                string filePath = Path.Combine(outputDir, fileName);

                // Write the attachment stream to a file
                using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    attachStreams[i].Position = 0;
                    attachStreams[i].CopyTo(fs);
                }

                // Compute SHA‑256 hash of the saved file
                string hashHex = ComputeSha256(filePath);
                Console.WriteLine($"Attachment: {fileName}");
                Console.WriteLine($"Saved to:   {filePath}");
                Console.WriteLine($"SHA‑256:    {hashHex}");
                Console.WriteLine();
            }
        }
    }

    // Helper method to compute SHA‑256 hash of a file and return it as a hex string
    private static string ComputeSha256(string filePath)
    {
        using (FileStream stream = File.OpenRead(filePath))
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashBytes = sha256.ComputeHash(stream);
            // Convert byte array to hexadecimal string
            return BitConverter.ToString(hashBytes).Replace("-", string.Empty).ToLowerInvariant();
        }
    }
}
