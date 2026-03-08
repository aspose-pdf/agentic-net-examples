using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF containing attachments
        const string inputPdf = "input.pdf";
        // Directory where extracted attachments will be saved
        const string outputDir = "ExtractedAttachments";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Use PdfExtractor (Facade) to work with attachments
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(inputPdf);

            // Extract all attachments from the PDF
            extractor.ExtractAttachment();

            // Retrieve the list of attachment names (generic IList<string>)
            IList<string> attachmentNames = extractor.GetAttachNames();

            // Iterate over each attachment name and save it to a file
            foreach (string name in attachmentNames)
            {
                // Build full path for the extracted file
                string outputPath = Path.Combine(outputDir, name);

                // Save the attachment to the specified path
                extractor.GetAttachment(outputPath);

                Console.WriteLine($"Extracted attachment: {name} -> {outputPath}");
            }
        }

        Console.WriteLine("Attachment extraction completed.");
    }
}
