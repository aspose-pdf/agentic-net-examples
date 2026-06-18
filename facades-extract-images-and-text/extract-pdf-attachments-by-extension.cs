using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the source PDF containing attachments
        const string inputPdfPath = "input.pdf";

        // Folder where extracted attachments will be placed,
        // organized into subfolders by their original file extension
        const string outputRootFolder = "ExtractedAttachments";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the root output folder exists
        Directory.CreateDirectory(outputRootFolder);

        // Use PdfExtractor (a Facade) to work with the PDF
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(inputPdfPath);

            // Extract attachment information from the document
            extractor.ExtractAttachment();

            // Retrieve attachment names (must be called after ExtractAttachment)
            IList<string> attachmentNames = extractor.GetAttachNames();

            // Retrieve attachment data as memory streams
            MemoryStream[] attachmentStreams = extractor.GetAttachment();

            // Verify that names and streams count match
            if (attachmentNames.Count != attachmentStreams.Length)
            {
                Console.Error.WriteLine("Mismatch between attachment names and streams.");
                return;
            }

            // Process each attachment
            for (int i = 0; i < attachmentNames.Count; i++)
            {
                string attachmentName = attachmentNames[i];
                string extension = Path.GetExtension(attachmentName);

                // Use "no_ext" folder for files without an extension
                string extFolder = string.IsNullOrEmpty(extension)
                    ? "no_ext"
                    : extension.TrimStart('.').ToLowerInvariant();

                // Create subfolder for this extension
                string targetFolder = Path.Combine(outputRootFolder, extFolder);
                Directory.CreateDirectory(targetFolder);

                // Full path for the extracted file
                string targetPath = Path.Combine(targetFolder, attachmentName);

                // Write the memory stream to disk
                using (MemoryStream ms = attachmentStreams[i])
                using (FileStream fs = new FileStream(targetPath, FileMode.Create, FileAccess.Write))
                {
                    ms.Position = 0;
                    ms.CopyTo(fs);
                }

                Console.WriteLine($"Extracted: {attachmentName} → {targetPath}");
            }
        }

        Console.WriteLine("Attachment extraction completed.");
    }
}