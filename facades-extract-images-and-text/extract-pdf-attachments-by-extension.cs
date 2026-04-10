using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
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

        // Use PdfExtractor (Facade) to work with attachments
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF
            extractor.BindPdf(inputPdfPath);

            // Extract attachment information from the PDF
            extractor.ExtractAttachment();

            // Get the list of attachment file names
            IList<string> attachmentNames = extractor.GetAttachNames();

            // Get the attachment data as memory streams (parallel array)
            MemoryStream[] attachmentStreams = extractor.GetAttachment();

            // Safety check: the two collections should have the same count
            if (attachmentNames.Count != attachmentStreams.Length)
            {
                Console.Error.WriteLine("Mismatch between attachment names and streams.");
                return;
            }

            // Process each attachment
            for (int i = 0; i < attachmentNames.Count; i++)
            {
                string fileName = attachmentNames[i];

                // Determine the original file extension (without the leading dot)
                string extension = Path.GetExtension(fileName);
                if (string.IsNullOrEmpty(extension))
                {
                    extension = "no_extension";
                }
                else
                {
                    extension = extension.TrimStart('.'); // e.g., "pdf", "png"
                }

                // Create a subfolder for this extension
                string targetFolder = Path.Combine(outputRootFolder, extension);
                Directory.CreateDirectory(targetFolder);

                // Full path for the extracted file
                string outputPath = Path.Combine(targetFolder, fileName);

                // Write the memory stream to disk
                using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    MemoryStream attachmentStream = attachmentStreams[i];
                    attachmentStream.Position = 0; // reset to start
                    attachmentStream.CopyTo(fileStream);
                }

                Console.WriteLine($"Extracted: {outputPath}");
            }
        }

        Console.WriteLine("Attachment extraction completed.");
    }
}