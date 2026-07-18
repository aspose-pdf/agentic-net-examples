using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf      = "input.pdf";          // Existing PDF
        const string outputPdf     = "output.pdf";         // PDF with new attachment
        const string attachment    = "newAttachment.pdf";  // File to attach
        const string description   = "New attachment added";

        // Verify files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(attachment))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachment}");
            return;
        }

        // PdfContentEditor edits PDF content, including attachments
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the PDF to be edited
            editor.BindPdf(inputPdf);

            // Add a new attachment; existing attachments are left untouched
            editor.AddDocumentAttachment(attachment, description);

            // Persist changes
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Attachment added successfully. Saved as '{outputPdf}'.");
    }
}