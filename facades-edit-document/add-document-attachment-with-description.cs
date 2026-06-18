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

        // Load the PDF, add the attachment with a description, and save.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPdf);                                 // Load PDF
            editor.AddDocumentAttachment(attachmentPath, description); // Add attachment
            editor.Save(outputPdf);                                   // Save updated PDF
        }

        Console.WriteLine($"Attachment added and saved to '{outputPdf}'.");
    }
}