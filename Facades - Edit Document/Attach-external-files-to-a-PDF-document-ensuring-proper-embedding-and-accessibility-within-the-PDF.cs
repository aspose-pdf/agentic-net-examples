using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing; // System.Drawing.Rectangle is required for PdfContentEditor.CreateFileAttachment

class AttachFilesExample
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath   = "input.pdf";
        const string attachmentPath = "attachment_file.pdf";
        const string outputPdfPath  = "output_with_attachments.pdf";

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

        // Load the source PDF
        using (Document doc = new Document(inputPdfPath))
        {
            // Initialize the content editor facade
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // Embed the file as a document attachment (no visible annotation)
            editor.AddDocumentAttachment(attachmentPath, "Embedded attachment without annotation");

            // Create a visible file‑attachment annotation on page 1
            // PdfContentEditor expects a System.Drawing.Rectangle for the annotation bounds
            System.Drawing.Rectangle annotRect = new System.Drawing.Rectangle(100, 500, 20, 20);
            editor.CreateFileAttachment(
                annotRect,
                "Attachment annotation tooltip",
                attachmentPath,
                1,               // page number (1‑based)
                "Graph");        // icon name (Graph, PushPin, Paperclip, Tag)

            // Save the modified PDF
            editor.Save(outputPdfPath);
            editor.Close(); // optional, releases resources
        }

        Console.WriteLine($"PDF saved with attachments: {outputPdfPath}");
    }
}
