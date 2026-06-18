using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf       = "input.pdf";
        const string attachmentPath = "attachment_file.pdf";
        const string outputPdf      = "output_with_attachment.pdf";

        // Verify source files exist.
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

        // Bind the existing PDF, add an embedded file attachment (no visible annotation),
        // and save the result.
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdf);
        editor.AddDocumentAttachment(attachmentPath, "Sample attachment description");
        editor.Save(outputPdf);

        Console.WriteLine($"Attachment added. Output saved to '{outputPdf}'.");
    }
}