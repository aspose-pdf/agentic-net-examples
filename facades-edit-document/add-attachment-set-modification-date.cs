using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath      = "input.pdf";          // existing PDF
        const string attachmentPath    = "audit_log.txt";      // file to attach
        const string outputPdfPath     = "output_with_attachment.pdf";

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Initialize the PdfContentEditor facade on the loaded document
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // Add the attachment without any visual annotation
            editor.AddDocumentAttachment(attachmentPath, "Audit log attachment");

            // Set the document's modification date to the current UTC timestamp
            // DocumentInfo.ModDate expects a DateTime, not a formatted string.
            doc.Info.ModDate = DateTime.UtcNow;

            // Save the updated PDF
            editor.Save(outputPdfPath);
        }

        Console.WriteLine($"Attachment added and modification date set. Saved to '{outputPdfPath}'.");
    }
}
