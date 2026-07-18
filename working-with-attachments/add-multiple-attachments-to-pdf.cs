using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output_with_attachments.pdf";

        // Create a minimal source PDF so the example can run in an empty sandbox
        using (Document seed = new Document())
        {
            seed.Pages.Add(); // add a blank page
            seed.Save(inputPdfPath);
        }

        // Collection of file paths to be attached
        List<string> attachmentPaths = new List<string>
        {
            "doc1.txt",
            "image.png",
            "report.pdf"
        };

        // Ensure the sample attachment files exist for the demo (create dummy files if needed)
        foreach (var path in attachmentPaths)
        {
            if (!File.Exists(path))
            {
                File.WriteAllText(path, $"Sample content for {Path.GetFileName(path)}");
            }
        }

        // Load the PDF document (wrapped in using for proper disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate over each file path and add it as an embedded file
            foreach (string filePath in attachmentPaths)
            {
                if (!File.Exists(filePath))
                {
                    Console.Error.WriteLine($"Attachment not found: {filePath}");
                    continue;
                }

                // Create a file specification and embed the file contents
                var fileSpec = new FileSpecification(filePath, $"Attachment: {Path.GetFileName(filePath)}")
                {
                    // Assign the file bytes to the Contents stream
                    Contents = new MemoryStream(File.ReadAllBytes(filePath))
                };

                // Add the file specification to the document's embedded files collection
                pdfDoc.EmbeddedFiles.Add(fileSpec);
            }

            // Save the modified PDF with the attachments
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with attachments to '{outputPdfPath}'.");
    }
}
