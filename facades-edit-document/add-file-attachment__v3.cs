using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string attachmentPath = "attachment.pdf";
        const string outputPdf = "output.pdf";

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

        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdf);
        editor.AddDocumentAttachment(attachmentPath, "Sample attachment");
        editor.Save(outputPdf);

        Console.WriteLine($"Attachment added. Saved to '{outputPdf}'.");
    }
}