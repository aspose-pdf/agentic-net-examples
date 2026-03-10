using System;
using System.IO;
using System.Collections.Generic; // Added for IList<string>
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the source PDF that contains embedded attachments
        const string inputPdfPath = "input.pdf";

        // Directory where extracted attachments will be saved
        const string outputDirectory = "ExtractedAttachments";

        // Validate input file
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        try
        {
            // PdfExtractor is a Facade that implements IDisposable – use a using block
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the PDF file to the extractor
                extractor.BindPdf(inputPdfPath);

                // Extract all attachments from the document
                extractor.ExtractAttachment();

                // Get the list of attachment names (generic IList<string>)
                IList<string> attachmentNames = extractor.GetAttachNames();

                // Get the attachment data as an array of MemoryStream objects
                MemoryStream[] attachmentStreams = extractor.GetAttachment();

                // Iterate over the attachments and write each to a file
                for (int i = 0; i < attachmentStreams.Length; i++)
                {
                    // Each name object is already a string
                    string attachmentName = attachmentNames[i];

                    // Build the full output path for the attachment
                    string outputPath = Path.Combine(outputDirectory, attachmentName);

                    // Reset stream position before copying
                    attachmentStreams[i].Position = 0;

                    // Write the attachment stream to the file system
                    using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                    {
                        attachmentStreams[i].CopyTo(fileStream);
                    }

                    Console.WriteLine($"Extracted: {attachmentName} → {outputPath}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }
}
