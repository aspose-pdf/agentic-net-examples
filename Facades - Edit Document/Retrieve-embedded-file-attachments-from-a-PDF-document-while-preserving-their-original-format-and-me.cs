using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "Attachments";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Create PdfExtractor, bind to the PDF, and extract attachments
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);
            extractor.ExtractAttachment(); // extracts all attachments

            // Retrieve attachment names
            IList<string> attachmentNames = extractor.GetAttachNames();

            // Retrieve attachment streams (preserves original format and metadata)
            MemoryStream[] attachmentStreams = extractor.GetAttachment();

            for (int i = 0; i < attachmentNames.Count; i++)
            {
                string name = attachmentNames[i];
                string outPath = Path.Combine(outputDir, name);

                // Save each attachment to the output directory
                using (FileStream fs = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                {
                    attachmentStreams[i].Position = 0;
                    attachmentStreams[i].CopyTo(fs);
                }

                Console.WriteLine($"Saved attachment: {outPath}");
            }
        }
    }
}