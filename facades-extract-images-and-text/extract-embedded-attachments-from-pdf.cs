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
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Use PdfExtractor (Facade) to work with attachments
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the source PDF file
                extractor.BindPdf(inputPdfPath);

                // Extract all attachments from the PDF
                extractor.ExtractAttachment();

                // Get the list of attachment names
                IList<string> attachmentNames = extractor.GetAttachNames();

                // Get the attachment data as memory streams
                MemoryStream[] attachmentStreams = extractor.GetAttachment();

                // Write each attachment to the output directory
                for (int i = 0; i < attachmentStreams.Length; i++)
                {
                    string name = attachmentNames[i];
                    string outputPath = Path.Combine(outputDir, name);

                    // Reset stream position before reading
                    attachmentStreams[i].Position = 0;

                    using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                    {
                        attachmentStreams[i].CopyTo(fileStream);
                    }

                    Console.WriteLine($"Extracted: {outputPath}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error extracting attachments: {ex.Message}");
        }
    }
}