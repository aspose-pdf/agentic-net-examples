using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF to which attachments will be added
        const string inputPdfPath = "source.pdf";
        // Output PDF with the added attachments
        const string outputPdfPath = "source_with_attachments.pdf";

        // Collection of file paths to be attached
        string[] attachmentFiles = new string[]
        {
            "doc1.txt",
            "image1.png",
            "report.pdf"
        };

        // Verify the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate over each file path and add it as an embedded file
            foreach (string filePath in attachmentFiles)
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"Attachment file not found, skipping: {filePath}");
                    continue;
                }

                // Create a FileSpecification with the display name
                var fileSpec = new FileSpecification(Path.GetFileName(filePath))
                {
                    Description = "Attached file",
                    // Set the file contents via a memory stream (required constructor overload)
                    Contents = new MemoryStream(File.ReadAllBytes(filePath))
                };

                // Add the file specification to the document's EmbeddedFiles collection
                pdfDoc.EmbeddedFiles.Add(fileSpec);
            }

            // Save the modified PDF (still PDF format)
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Attachments added and saved to '{outputPdfPath}'.");
    }
}
