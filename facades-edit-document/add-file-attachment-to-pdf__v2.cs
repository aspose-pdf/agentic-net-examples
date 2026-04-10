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

        // Verify that the source PDF and the attachment file exist.
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Error: Input PDF not found at '{inputPdf}'.");
            return;
        }

        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Error: Attachment file not found at '{attachmentPath}'.");
            return;
        }

        // Use the PdfContentEditor facade to bind the PDF, add the attachment, and save.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPdf);                                 // Load the existing PDF.
            editor.AddDocumentAttachment(attachmentPath, description); // Attach the file (no annotation).
            editor.Save(outputPdf);                                   // Save the modified PDF.
        }

        Console.WriteLine($"Attachment added successfully. Output saved to '{outputPdf}'.");
    }
}