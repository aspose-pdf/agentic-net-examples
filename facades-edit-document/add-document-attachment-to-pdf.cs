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

        // Use PdfContentEditor to bind the PDF, add the attachment, and save.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPdf);                                 // Load the PDF.
            editor.AddDocumentAttachment(attachmentPath, description); // Add Terms.pdf with description.
            editor.Save(outputPdf);                                   // Persist changes.
        }

        Console.WriteLine($"Attachment added and saved to '{outputPdf}'.");
    }
}