using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // Existing PDF to modify
        const string attachmentPath = "attachment.pdf";    // File to embed
        const string outputPdfPath = "output.pdf";        // Resulting PDF

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create a FileSpecification for the attachment (use only the file name)
            var fileSpec = new FileSpecification(Path.GetFileName(attachmentPath))
            {
                Description = "Sample PDF attachment",
                MIMEType = "application/pdf",
                Contents = new MemoryStream(File.ReadAllBytes(attachmentPath))
            };

            // Add the file specification to the document's EmbeddedFiles collection
            // The EmbeddedFiles collection is read‑only; it is instantiated by the Document
            // and can be used directly without re‑assigning.
            pdfDoc.EmbeddedFiles.Add(fileSpec);

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"File attachment added and saved to '{outputPdfPath}'.");
    }
}
