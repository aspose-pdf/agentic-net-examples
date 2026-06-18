using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF containing attachments
        const string inputPdf = "input.pdf";

        // Directory where renamed attachments will be saved
        const string outputDir = "Attachments";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Use PdfExtractor (facade) to work with attachments
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file
            extractor.BindPdf(inputPdf);

            // Extract all attachments from the document
            extractor.ExtractAttachment();

            // Get attachment names (original file names)
            IList<string> names = extractor.GetAttachNames();

            // Get attachment data as memory streams
            MemoryStream[] streams = extractor.GetAttachment();

            // Iterate through each attachment
            for (int i = 0; i < streams.Length; i++)
            {
                // Original attachment name
                string originalName = names[i];

                // Create a timestamp prefix (yyyyMMddHHmmssfff)
                string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");

                // Build the new file name with timestamp prefix
                string newFileName = $"{timestamp}_{originalName}";

                // Full path for the saved file
                string fullPath = Path.Combine(outputDir, newFileName);

                // Write the attachment stream to disk with the new name
                using (FileStream fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                {
                    streams[i].Position = 0; // Reset stream position
                    streams[i].CopyTo(fs);
                }

                Console.WriteLine($"Saved attachment as {newFileName}");
            }
        }
    }
}