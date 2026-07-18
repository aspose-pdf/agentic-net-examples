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

        // Use PdfContentEditor (Facades API) to bind the PDF, add the attachment, and save
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPdf);                                 // Load the PDF
            editor.AddDocumentAttachment(attachmentPath, description); // Add attachment with description
            editor.Save(outputPdf);                                   // Save the updated PDF
        }

        Console.WriteLine($"Attachment added and saved to '{outputPdf}'.");
    }
}