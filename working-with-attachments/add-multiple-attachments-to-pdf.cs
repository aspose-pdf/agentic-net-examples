using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF to which attachments will be added
        const string inputPdf = "input.pdf";
        // Output PDF containing the attachments
        const string outputPdf = "output_with_attachments.pdf";

        // Collection of file paths to attach
        List<string> attachmentPaths = new List<string>
        {
            "file1.txt",
            "image.png",
            "document.docx"
        };

        // Verify the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Verify each attachment file exists
        foreach (string path in attachmentPaths)
        {
            if (!File.Exists(path))
            {
                Console.Error.WriteLine($"Attachment not found: {path}");
                return;
            }
        }

        // Load the PDF, add attachments, and save
        using (Document doc = new Document(inputPdf))
        {
            foreach (string path in attachmentPaths)
            {
                // Create a file specification for the attachment
                FileSpecification fileSpec = new FileSpecification(path);
                // Add the specification to the document's embedded files collection
                doc.EmbeddedFiles.Add(fileSpec);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with attachments to '{outputPdf}'.");
    }
}