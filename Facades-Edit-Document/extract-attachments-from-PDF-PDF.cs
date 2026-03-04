using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // PDF containing attachments
        const string outputDir    = "ExtractedAttachments"; // Folder to store extracted files

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Use PdfExtractor (Facade) inside a using block for deterministic disposal
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF file
            extractor.BindPdf(inputPdfPath);

            // Extract all attachment entries from the PDF
            extractor.ExtractAttachment();

            // Retrieve the list of attachment names
            IList<string> attachmentNames = extractor.GetAttachNames();

            // Get the attachment streams (one stream per attachment, order matches names)
            MemoryStream[] attachmentStreams = extractor.GetAttachment();

            // Write each attachment to a file using its original name
            for (int i = 0; i < attachmentStreams.Length; i++)
            {
                string attachmentName = attachmentNames[i];
                string outputPath = Path.Combine(outputDir, attachmentName);

                // Reset stream position before reading
                attachmentStreams[i].Position = 0;

                using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    attachmentStreams[i].CopyTo(fileStream);
                }

                Console.WriteLine($"Extracted: {attachmentName} -> {outputPath}");
            }
        }

        Console.WriteLine("Attachment extraction completed.");
    }
}