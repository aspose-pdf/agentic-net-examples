using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF to which attachments will be added
        const string inputPdfPath  = "input.pdf";
        // Output PDF containing the attachments
        const string outputPdfPath = "output_with_attachments.pdf";

        // Collection of file paths to attach
        List<string> attachmentPaths = new List<string>
        {
            "document1.txt",
            "image1.png",
            "data1.csv"
        };

        // Verify the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        // Verify each attachment file exists before processing
        foreach (string path in attachmentPaths)
        {
            if (!File.Exists(path))
            {
                Console.Error.WriteLine($"Attachment file not found: {path}");
                return;
            }
        }

        // Load the PDF, add attachments, and save
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            foreach (string path in attachmentPaths)
            {
                // Create a file specification for the attachment
                FileSpecification fileSpec = new FileSpecification(path);
                // Optional: set a description (shown in PDF viewers)
                fileSpec.Description = Path.GetFileName(path);

                // Add the file specification to the document's embedded files collection
                pdfDoc.EmbeddedFiles.Add(fileSpec);
            }

            // Save the modified PDF with the embedded attachments
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with attachments: {outputPdfPath}");
    }
}