using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class ExtractAttachmentsExample
{
    static void Main()
    {
        // Input PDF containing attachments
        const string inputPdfPath = "input.pdf";

        // Directory where extracted attachments will be saved
        const string outputDirectory = "ExtractedAttachments";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        try
        {
            // Initialize the PdfExtractor facade
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the PDF document
                extractor.BindPdf(inputPdfPath);

                // Extract all attachments from the PDF
                extractor.ExtractAttachment();

                // Get the list of attachment names
                IList<string> attachmentNames = extractor.GetAttachNames();

                // Get the attachment data as memory streams (order matches names)
                MemoryStream[] attachmentStreams = extractor.GetAttachment();

                // Save each attachment to the output directory
                for (int i = 0; i < attachmentNames.Count; i++)
                {
                    string attachmentName = attachmentNames[i];
                    string outputPath = Path.Combine(outputDirectory, attachmentName);

                    // Reset stream position before reading
                    attachmentStreams[i].Position = 0;

                    using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                    {
                        attachmentStreams[i].CopyTo(fileStream);
                    }

                    Console.WriteLine($"Extracted: {attachmentName} -> {outputPath}");
                }
            }

            Console.WriteLine("All attachments have been extracted successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }
}