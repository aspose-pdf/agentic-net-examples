using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string attachmentPath = "attachment_file.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdf);

        if (File.Exists(attachmentPath))
        {
            editor.AddDocumentAttachment(attachmentPath, "Attachment description");
        }
        else
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
        }

        editor.Save(outputPdf);
        Console.WriteLine($"Saved PDF with attachment (if file existed) to '{outputPdf}'.");
    }
}