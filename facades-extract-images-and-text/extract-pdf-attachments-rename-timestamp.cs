using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source PDF – you can also pass it as a command‑line argument.
        const string inputPdfPath = "input.pdf"; // Ensure this file exists in the working directory.
        const string outputDirectory = "ExtractedAttachments"; // Folder to store renamed files.

        // Verify that the source PDF exists before attempting extraction.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: The PDF file '{inputPdfPath}' was not found. Please provide a valid path.");
            return;
        }

        // Ensure the output folder exists.
        Directory.CreateDirectory(outputDirectory);

        try
        {
            // Use PdfExtractor (Facade) to extract attachments.
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the source PDF.
                extractor.BindPdf(inputPdfPath);

                // Extract all attachments from the document.
                extractor.ExtractAttachment();

                // Retrieve attachment names.
                IList<string> attachmentNames = extractor.GetAttachNames();

                // Retrieve attachment streams (one stream per attachment).
                MemoryStream[] attachmentStreams = extractor.GetAttachment();

                // Guard against a mismatch between names and streams.
                int count = Math.Min(attachmentNames.Count, attachmentStreams.Length);

                for (int i = 0; i < count; i++)
                {
                    // Original attachment file name.
                    string originalName = attachmentNames[i];

                    // Build a timestamp prefix (yyyyMMddHHmmssfff) and ensure uniqueness with the loop index.
                    string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    string newFileName = $"{timestamp}_{i}_{originalName}";

                    // Full path for the renamed attachment.
                    string outputPath = Path.Combine(outputDirectory, newFileName);

                    // Write the stream to disk.
                    using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                    {
                        // Reset stream position to the beginning.
                        attachmentStreams[i].Position = 0;
                        attachmentStreams[i].CopyTo(fileStream);
                    }

                    Console.WriteLine($"Saved attachment as: {outputPath}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred while extracting attachments: {ex.Message}");
        }
    }
}
