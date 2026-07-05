using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string attachmentPath = "attachment_file.pdf";
        const string description = "Sample attachment description";
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

        // Add the attachment using PdfContentEditor (facade API)
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPdf);                                 // Load the PDF
            editor.AddDocumentAttachment(attachmentPath, description); // Add attachment (no annotation)
            editor.Save(outputPdf);                                   // Save the modified PDF
        }

        Console.WriteLine($"Attachment added and saved to '{outputPdf}'.");
    }
}