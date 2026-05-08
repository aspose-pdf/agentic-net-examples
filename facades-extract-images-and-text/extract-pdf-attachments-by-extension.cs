using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF containing attachments
        const string inputPdfPath = "input.pdf";

        // Root folder where attachments will be saved
        const string outputRootFolder = "ExtractedAttachments";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the root output folder exists
        Directory.CreateDirectory(outputRootFolder);

        // Use PdfExtractor facade to work with attachments
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF
            extractor.BindPdf(inputPdfPath);

            // Extract attachment information from the PDF
            extractor.ExtractAttachment();

            // Get the list of attachment file names
            IList<string> attachmentNames = extractor.GetAttachNames();

            // Retrieve all attachment streams
            MemoryStream[] attachmentStreams = extractor.GetAttachment();

            // Iterate over each attachment and save it to the appropriate subfolder
            for (int i = 0; i < attachmentStreams.Length; i++)
            {
                string name = attachmentNames[i];
                MemoryStream stream = attachmentStreams[i];

                // Determine the file extension (e.g., "pdf", "png")
                string extension = Path.GetExtension(name);
                if (string.IsNullOrEmpty(extension))
                {
                    // If no extension, place it in a folder named "no_extension"
                    extension = "no_extension";
                }
                else
                {
                    // Remove the leading dot
                    extension = extension.TrimStart('.').ToLowerInvariant();
                }

                // Create subfolder for this extension
                string extensionFolder = Path.Combine(outputRootFolder, extension);
                Directory.CreateDirectory(extensionFolder);

                // Full path for the extracted file
                string outputPath = Path.Combine(extensionFolder, name);

                // Write the stream to disk
                using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    // Reset stream position to the beginning
                    stream.Position = 0;
                    stream.CopyTo(fileStream);
                }

                Console.WriteLine($"Extracted: {outputPath}");
            }
        }

        Console.WriteLine("Attachment extraction completed.");
    }
}