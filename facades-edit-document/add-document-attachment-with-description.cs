using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string attachmentPath = "attachment.pdf";
        const string description = "Sample attachment description";
        const string outputPdf = "output.pdf";

        // Verify that the source PDF and attachment file exist
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

        // Add the attachment using PdfContentEditor (Facades API)
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF document
            editor.BindPdf(inputPdf);

            // Add the attachment with a descriptive label
            editor.AddDocumentAttachment(attachmentPath, description);

            // Save the updated PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Attachment added and saved to '{outputPdf}'.");
    }
}