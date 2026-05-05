using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string attachmentPath = "attachment_file.pdf";
        const string outputPdf = "output.pdf";
        const string description = "Sample attachment";

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

        // Initialize the PdfContentEditor facade and bind the source PDF
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdf);

        // Add the file as an embedded attachment (no visible annotation)
        editor.AddDocumentAttachment(attachmentPath, description);

        // Save the resulting PDF with the embedded file
        editor.Save(outputPdf);

        Console.WriteLine($"Attachment added and saved to '{outputPdf}'.");
    }
}