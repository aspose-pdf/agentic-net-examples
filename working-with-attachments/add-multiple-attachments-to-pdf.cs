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

        // Validate input PDF existence
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Validate each attachment file exists
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
                // Add the specification to the document's embedded files collection
                pdfDoc.EmbeddedFiles.Add(fileSpec);
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with attachments to '{outputPdfPath}'.");
    }
}