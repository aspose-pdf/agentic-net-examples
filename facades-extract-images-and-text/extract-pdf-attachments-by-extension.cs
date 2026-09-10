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

        // Base folder where attachments will be saved
        const string outputBaseFolder = "ExtractedAttachments";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the base output folder exists
        Directory.CreateDirectory(outputBaseFolder);

        // Use PdfExtractor (Facade) to extract attachments
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF
            extractor.BindPdf(inputPdfPath);

            // Perform the extraction operation
            extractor.ExtractAttachment();

            // Retrieve attachment names
            IList<string> attachmentNames = extractor.GetAttachNames();

            // Retrieve attachment streams (one stream per attachment)
            MemoryStream[] attachmentStreams = extractor.GetAttachment();

            // Iterate over each attachment
            for (int i = 0; i < attachmentNames.Count; i++)
            {
                string name = attachmentNames[i];
                string extension = Path.GetExtension(name).TrimStart('.').ToLowerInvariant();

                // Create a subfolder for the current file extension
                string extensionFolder = Path.Combine(outputBaseFolder, string.IsNullOrEmpty(extension) ? "no_ext" : extension);
                Directory.CreateDirectory(extensionFolder);

                // Full path for the extracted file
                string outputPath = Path.Combine(extensionFolder, name);

                // Write the stream to disk
                using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    MemoryStream srcStream = attachmentStreams[i];
                    srcStream.Position = 0; // Ensure we start from the beginning
                    srcStream.CopyTo(fileStream);
                }

                Console.WriteLine($"Extracted: {outputPath}");
            }
        }

        Console.WriteLine("All attachments have been extracted and organized.");
    }
}