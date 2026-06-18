using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string attachmentPath = "attachment.txt";
        const string description = "Audit attachment";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        // Use PdfContentEditor to edit the PDF
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF
            editor.BindPdf(inputPdf);

            // Add the attachment without any annotation
            editor.AddDocumentAttachment(attachmentPath, description);

            // Set the document's modification date to the current UTC timestamp
            // DocumentInfo.ModDate expects a DateTime value
            editor.Document.Info.ModDate = DateTime.UtcNow;

            // Save the modified PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Attachment added and ModDate updated. Saved to '{outputPdf}'.");
    }
}
