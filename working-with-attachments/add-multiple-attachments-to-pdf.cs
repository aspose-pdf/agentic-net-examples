using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_with_attachments.pdf";

        // Collection of file paths to be attached
        List<string> attachmentPaths = new List<string>
        {
            "doc1.txt",
            "image1.jpg",
            "report.pdf"
        };

        // Verify the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
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

        // Load the PDF, add attachments, and save – all within a using block for proper disposal
        using (Document doc = new Document(inputPdf))
        {
            foreach (string path in attachmentPaths)
            {
                // Create a FileSpecification for the file and add it to the EmbeddedFiles collection
                var fileSpec = new FileSpecification(Path.GetFileName(path));
                fileSpec.Contents = new MemoryStream(File.ReadAllBytes(path));
                fileSpec.Description = $"Embedded file: {Path.GetFileName(path)}";
                doc.EmbeddedFiles.Add(fileSpec);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Attachments added and saved to '{outputPdf}'.");
    }
}
