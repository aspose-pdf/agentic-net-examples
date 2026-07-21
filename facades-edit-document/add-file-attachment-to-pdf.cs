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
        const string attachmentPath = "Terms.pdf";
        const string description = "Contract Terms";

        // Verify that the source PDF and attachment exist.
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

        // PdfContentEditor does not implement IDisposable, so we instantiate it directly.
        PdfContentEditor editor = new PdfContentEditor();

        // Load the existing PDF document.
        editor.BindPdf(inputPdf);

        // Add the file attachment without creating an annotation.
        editor.AddDocumentAttachment(attachmentPath, description);

        // Save the updated PDF.
        editor.Save(outputPdf);

        Console.WriteLine($"Attachment added and saved to '{outputPdf}'.");
    }
}