using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF and output PDF paths
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_attachments.pdf";

        // Collection of file paths to be attached
        List<string> attachmentPaths = new List<string>
        {
            "file1.txt",
            "image.png",
            "data.csv"
        };

        // Validate input PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Validate each attachment file exists
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