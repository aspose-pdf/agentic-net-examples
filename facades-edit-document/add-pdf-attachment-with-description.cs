using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, the file to attach, and the output PDF
        const string sourcePdfPath = "source.pdf";
        const string attachmentPath = "invoice.pdf";
        const string outputPdfPath = "output_with_attachment.pdf";

        // Verify that the source PDF and attachment exist
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(sourcePdfPath))
        {
            // Create a PdfContentEditor facade and bind it to the loaded document
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // Add the attachment with a description.
            // The MIME type is inferred from the file extension (application/pdf for .pdf files).
            editor.AddDocumentAttachment(attachmentPath, "Invoice Document");

            // Save the modified PDF to the output path
            editor.Save(outputPdfPath);
        }

        Console.WriteLine($"Attachment added and saved to '{outputPdfPath}'.");
    }
}