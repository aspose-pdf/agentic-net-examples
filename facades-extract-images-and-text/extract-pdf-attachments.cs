using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF containing attachments
        const string inputPdfPath = "input.pdf";

        // Directory where extracted files will be saved
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
                // Bind the source PDF file
                extractor.BindPdf(inputPdfPath);

                // Extract all attachments from the PDF
                extractor.ExtractAttachment();

                // Get the list of attachment names
                IList<string> attachmentNames = extractor.GetAttachNames();

                // Get the attachment streams (one stream per attachment)
                MemoryStream[] attachmentStreams = extractor.GetAttachment();

                // Write each attachment to the output directory
                for (int i = 0; i < attachmentStreams.Length; i++)
                {
                    string fileName = attachmentNames[i];
                    string outputPath = Path.Combine(outputDirectory, fileName);

                    // Reset stream position before reading
                    attachmentStreams[i].Position = 0;

                    using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                    {
                        attachmentStreams[i].CopyTo(fileStream);
                    }

                    Console.WriteLine($"Extracted: {outputPath}");
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