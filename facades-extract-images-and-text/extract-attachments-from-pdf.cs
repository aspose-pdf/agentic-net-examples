using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "Attachments";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Create and configure the PdfExtractor facade
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the source PDF document
                extractor.BindPdf(inputPdf);

                // Extract all attachments from the PDF
                extractor.ExtractAttachment();

                // Retrieve attachment names and corresponding streams
                IList<string> attachmentNames = extractor.GetAttachNames();
                MemoryStream[] attachmentStreams = extractor.GetAttachment();

                // Write each attachment to the output directory
                for (int i = 0; i < attachmentStreams.Length; i++)
                {
                    string fileName = attachmentNames[i];
                    string outputPath = Path.Combine(outputDir, fileName);

                    // Reset stream position before reading
                    attachmentStreams[i].Position = 0;

                    using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                    {
                        attachmentStreams[i].CopyTo(fileStream);
                    }

                    // Dispose the memory stream after use
                    attachmentStreams[i].Dispose();
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