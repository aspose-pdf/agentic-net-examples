using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string desiredAttachmentName = "example.txt"; // change to the attachment you need

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Initialize the PDF extractor facade
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document
            extractor.BindPdf(inputPdfPath);

            // Extract attachment metadata (no need to extract file content for this task)
            extractor.ExtractAttachment();

            // Retrieve detailed information about all attachments
            List<FileSpecification> attachmentInfos = extractor.GetAttachmentInfo();

            // Locate the specific attachment by its name (heading)
            FileSpecification targetAttachment = null;
            foreach (FileSpecification spec in attachmentInfos)
            {
                if (string.Equals(spec.Name, desiredAttachmentName, StringComparison.OrdinalIgnoreCase))
                {
                    targetAttachment = spec;
                    break;
                }
            }

            if (targetAttachment != null)
            {
                // Output the heading (name) and additional metadata
                Console.WriteLine("Attachment found:");
                Console.WriteLine($"Name        : {targetAttachment.Name}");
                Console.WriteLine($"Description : {targetAttachment.Description}");
                Console.WriteLine($"MIME Type   : {targetAttachment.MIMEType}");
                // Additional parameters (if needed) can be accessed via targetAttachment.Params
            }
            else
            {
                Console.WriteLine($"Attachment with name '{desiredAttachmentName}' was not found in the PDF.");
            }
        }
    }
}