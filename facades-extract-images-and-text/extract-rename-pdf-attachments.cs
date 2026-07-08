using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";          // Path to source PDF
        const string outputDir = "Attachments";       // Directory to store renamed attachments

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Extract attachments using PdfExtractor (Facade API)
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);               // Load the PDF
            extractor.ExtractAttachment();             // Perform extraction

            IList<string> attachmentNames = extractor.GetAttachNames(); // Original names
            MemoryStream[] attachmentStreams = extractor.GetAttachment(); // Corresponding streams

            for (int i = 0; i < attachmentStreams.Length; i++)
            {
                // Reset stream position before reading
                attachmentStreams[i].Position = 0;

                // Build a unique timestamp prefix (yyyyMMddHHmmssfff) and include index
                string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                string newFileName = $"{timestamp}_{i}_{attachmentNames[i]}";

                string outputPath = Path.Combine(outputDir, newFileName);

                // Write the attachment stream to disk with the new name
                using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    attachmentStreams[i].CopyTo(fs);
                }

                Console.WriteLine($"Saved attachment: {outputPath}");
            }
        }
    }
}