using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string attachmentPath = "newAttachment.docx";
        const string description = "New attachment added";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        // Use PdfContentEditor to edit the PDF.
        // No DeleteAttachments call – existing attachments remain intact.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the source PDF.
            editor.BindPdf(inputPdf);

            // Add a new attachment (no annotation).
            editor.AddDocumentAttachment(attachmentPath, description);

            // Save the modified PDF.
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Attachment added successfully. Output saved to '{outputPdf}'.");
    }
}