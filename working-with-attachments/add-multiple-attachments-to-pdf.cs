using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF to which attachments will be added
        const string sourcePdf = "source.pdf";

        // Output PDF with the embedded attachments
        const string outputPdf = "source_with_attachments.pdf";

        // Collection of file paths to be attached
        List<string> attachmentPaths = new List<string>
        {
            "attachment1.docx",
            "attachment2.xlsx",
            "attachment3.txt"
        };

        // Verify source PDF exists
        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdf}");
            return;
        }

        // Verify each attachment exists before processing
        foreach (string path in attachmentPaths)
        {
            if (!File.Exists(path))
            {
                Console.Error.WriteLine($"Attachment not found: {path}");
                return;
            }
        }

        try
        {
            // Load the source PDF (using block ensures proper disposal)
            using (Document doc = new Document(sourcePdf))
            {
                // Iterate over the attachment file paths and embed each one
                foreach (string filePath in attachmentPaths)
                {
                    // Create a FileSpecification for the attachment
                    FileSpecification fileSpec = new FileSpecification(filePath);

                    // Add the file specification to the document's embedded files collection
                    doc.EmbeddedFiles.Add(fileSpec);
                }

                // Save the modified PDF with attachments
                doc.Save(outputPdf);
            }

            Console.WriteLine($"PDF saved with attachments: {outputPdf}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}