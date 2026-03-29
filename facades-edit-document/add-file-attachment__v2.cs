using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string attachmentPath = "Terms.pdf";
        const string description = "Contract Terms";

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
        editor.AddDocumentAttachment(attachmentPath, description);
        editor.Save(outputPdf);
        editor.Close();

        Console.WriteLine($"Attachment added and saved to '{outputPdf}'.");
    }
}
