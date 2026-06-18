using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class AttachmentExtractor
{
    static void Main()
    {
        // Path to the source PDF file containing attachments
        const string pdfPath = "input.pdf";

        // Directory where extracted attachments will be saved
        const string outputDir = "Attachments";

        // Validate input PDF file
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {pdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Use PdfExtractor (facade) to extract attachments
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document to the extractor
            extractor.BindPdf(pdfPath);

            // Extract all attachments from the bound PDF
            extractor.ExtractAttachment();

            // Retrieve the list of attachment names
            IList<string> attachmentNames = extractor.GetAttachNames();

            // Retrieve the attachment data as memory streams
            MemoryStream[] attachmentStreams = extractor.GetAttachment();

            // Write each attachment to a file in the output directory
            for (int i = 0; i < attachmentStreams.Length; i++)
            {
                string fileName = attachmentNames[i];
                string outputPath = Path.Combine(outputDir, fileName);

                // Ensure the stream is positioned at the beginning
                attachmentStreams[i].Position = 0;

                // Save the stream to a file
                using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    attachmentStreams[i].CopyTo(fileStream);
                }

                Console.WriteLine($"Extracted: {fileName} → {outputPath}");
            }
        }

        Console.WriteLine("All attachments have been extracted.");
    }
}