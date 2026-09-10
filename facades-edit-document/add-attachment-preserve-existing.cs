using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string attachmentPath = "newAttachment.pdf";
        const string description = "New attachment added";

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

        // Create a PdfContentEditor, bind the existing PDF, add the attachment, and save.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPdf);                                 // Load
            editor.AddDocumentAttachment(attachmentPath, description); // Add new attachment (preserves existing)
            editor.Save(outputPdf);                                   // Save
        }

        Console.WriteLine($"Attachment added and saved to '{outputPdf}'.");
    }
}