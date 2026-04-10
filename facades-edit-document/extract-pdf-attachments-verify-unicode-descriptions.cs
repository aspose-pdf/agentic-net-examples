using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "Attachments";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Use PdfExtractor (facade) to extract attachments
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file
            extractor.BindPdf(inputPdf);

            // Extract attachment objects from the PDF
            extractor.ExtractAttachment();

            // Retrieve the list of attachment names (optional, for verification)
            IList<string> attachmentNames = extractor.GetAttachNames();

            // Save all attachments to the output directory
            extractor.GetAttachment(outputDir);

            // Load the same PDF with Document to read attachment metadata (descriptions)
            using (Document doc = new Document(inputPdf))
            {
                foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
                {
                    // Description may contain Unicode characters; output directly
                    string description = fileSpec.Description ?? string.Empty;
                    Console.WriteLine($"Attachment Name: {fileSpec.Name}");
                    Console.WriteLine($"Description: {description}");
                }
            }
        }

        Console.WriteLine("Attachment extraction and description verification completed.");
    }
}