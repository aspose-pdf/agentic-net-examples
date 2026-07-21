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

        // Base folder where attachments will be organized by extension
        const string outputBaseFolder = "Attachments";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the base output folder exists
        Directory.CreateDirectory(outputBaseFolder);

        // Use PdfExtractor facade to extract attachments
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF
            extractor.BindPdf(inputPdfPath);

            // Perform the extraction operation
            extractor.ExtractAttachment();

            // Retrieve attachment names
            IList<string> attachmentNames = extractor.GetAttachNames();

            // Retrieve attachment streams (in the same order as names)
            MemoryStream[] attachmentStreams = extractor.GetAttachment();

            // Iterate over each attachment
            for (int i = 0; i < attachmentNames.Count; i++)
            {
                string name = attachmentNames[i];
                MemoryStream stream = attachmentStreams[i];

                // Determine file extension (including the dot), fallback to empty string
                string extension = Path.GetExtension(name);
                if (string.IsNullOrEmpty(extension))
                {
                    extension = "no_extension";
                }
                else
                {
                    // Remove leading dot and normalize
                    extension = extension.TrimStart('.').ToLowerInvariant();
                }

                // Create subfolder for this extension
                string extFolder = Path.Combine(outputBaseFolder, extension);
                Directory.CreateDirectory(extFolder);

                // Full path for the extracted file
                string outputPath = Path.Combine(extFolder, name);

                // Write the stream to disk
                stream.Position = 0; // Ensure we start from the beginning
                using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    stream.CopyTo(fileStream);
                }

                Console.WriteLine($"Extracted: {outputPath}");
            }
        }

        Console.WriteLine("Attachment extraction completed.");
    }
}