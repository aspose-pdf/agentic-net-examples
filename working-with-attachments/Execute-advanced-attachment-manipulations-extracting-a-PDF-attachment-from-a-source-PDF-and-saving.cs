using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "source_with_attachment.pdf";
        const string outputDirectory = "ExtractedAttachments";

        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Use PdfExtractor (facade) to extract attachments
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF
            extractor.BindPdf(sourcePdfPath);

            // Extract attachment data from the PDF
            extractor.ExtractAttachment();

            // Get attachment names (generic IList<string>)
            IList<string> attachNames = extractor.GetAttachNames();

            // Get attachment streams (each stream corresponds to a name)
            MemoryStream[] attachStreams = extractor.GetAttachment();

            // Safety check: number of names should match number of streams
            int count = Math.Min(attachNames.Count, attachStreams.Length);

            for (int i = 0; i < count; i++)
            {
                string attachmentName = attachNames[i] ?? $"attachment_{i}";
                string outputPath = Path.Combine(outputDirectory, attachmentName);

                // Write the stream to a file
                using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    attachStreams[i].Position = 0; // reset stream position
                    attachStreams[i].CopyTo(fs);
                }

                Console.WriteLine($"Extracted attachment saved to: {outputPath}");
            }
        }
    }
}
