using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF that contains attachments
        const string inputPdfPath = "input.pdf";

        // Directory where extracted attachments will be saved
        const string outputDir = "ExtractedAttachments";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Use PdfExtractor (Facade) to work with attachments
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF file
            extractor.BindPdf(inputPdfPath);

            // Extract attachment information from the PDF
            extractor.ExtractAttachment();

            // Get the list of attachment names
            IList<string> attachmentNames = extractor.GetAttachNames();

            // If there are no attachments, inform the user
            if (attachmentNames == null || attachmentNames.Count == 0)
            {
                Console.WriteLine("No attachments found in the PDF.");
                return;
            }

            // Save each attachment to the output directory
            for (int i = 0; i < attachmentNames.Count; i++)
            {
                string attachmentName = attachmentNames[i];
                string outputPath = Path.Combine(outputDir, attachmentName);

                // Store the attachment into a file
                extractor.GetAttachment(outputPath);
                Console.WriteLine($"Extracted: {attachmentName} → {outputPath}");
            }
        }

        Console.WriteLine("Attachment extraction completed.");
    }
}