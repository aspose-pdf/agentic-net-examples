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

        // Verify that the source PDF and the attachment exist.
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

        // Bind the PDF, add the attachment (no visual annotation), and save.
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdf);
        editor.AddDocumentAttachment(attachmentPath, description);
        editor.Save(outputPdf);

        Console.WriteLine($"Attachment '{attachmentPath}' added with description '{description}'.");
        Console.WriteLine($"Result saved to '{outputPdf}'.");
    }
}