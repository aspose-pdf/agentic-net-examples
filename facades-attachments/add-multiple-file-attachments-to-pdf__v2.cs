using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_attachments.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // List of files to attach to the PDF
        string[] attachmentFiles = { "file1.txt", "image.png", "data.csv" };

        // Ensure each attachment file exists
        foreach (string file in attachmentFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Attachment file not found: {file}");
                return;
            }
        }

        // Load the PDF using the high‑level Document API
        Document pdfDocument = new Document(inputPdf);

        // Add each file as an embedded attachment
        foreach (string file in attachmentFiles)
        {
            // Create a file specification for the attachment
            var fileSpec = new FileSpecification(Path.GetFileName(file))
            {
                Description = Path.GetFileName(file),
                // The file content is read into a memory stream
                Contents = new MemoryStream(File.ReadAllBytes(file))
            };

            // Add the specification to the document's EmbeddedFiles collection
            pdfDocument.EmbeddedFiles.Add(fileSpec);
        }

        // Save the resulting PDF with the attachments
        pdfDocument.Save(outputPdf);

        Console.WriteLine($"PDF with attachments saved to '{outputPdf}'.");
    }
}
