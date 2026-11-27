using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        // Paths of the three files to attach
        string[] filesToAttach = { "attachment1.txt", "attachment2.jpg", "attachment3.pdf" };

        // Verify the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Verify each attachment file exists
        foreach (string f in filesToAttach)
        {
            if (!File.Exists(f))
            {
                Console.Error.WriteLine($"Attachment file not found: {f}");
                return;
            }
        }

        // Load the PDF document
        Document pdfDoc = new Document(inputPdf);

        // -------------------------------------------------
        // Delete all existing embedded files (if any)
        // -------------------------------------------------
        if (pdfDoc.EmbeddedFiles != null && pdfDoc.EmbeddedFiles.Count > 0)
        {
            // EmbeddedFileCollection uses 1‑based indexing. Iterate backwards and delete by name.
            for (int i = pdfDoc.EmbeddedFiles.Count; i >= 1; i--)
            {
                FileSpecification spec = pdfDoc.EmbeddedFiles[i];
                if (spec != null && !string.IsNullOrEmpty(spec.Name))
                {
                    pdfDoc.EmbeddedFiles.Delete(spec.Name);
                }
            }
        }

        // -------------------------------------------------
        // Add the three new file attachments
        // -------------------------------------------------
        foreach (string filePath in filesToAttach)
        {
            // The constructor loads the file content and sets the name automatically.
            var fileSpec = new FileSpecification(filePath, Path.GetFileName(filePath))
            {
                Description = Path.GetFileName(filePath)
            };
            pdfDoc.EmbeddedFiles.Add(fileSpec);
        }

        // Save the modified PDF to the output path
        pdfDoc.Save(outputPdf);

        Console.WriteLine($"PDF saved with new attachments to '{outputPdf}'.");
    }
}
