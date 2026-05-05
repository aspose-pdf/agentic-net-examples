using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_with_attachment.pdf";
        const string attachmentPath = "newAttachment.pdf";
        const string description = "New attachment description";

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

        // Edit the PDF using PdfContentEditor (facade API)
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the existing PDF
            editor.BindPdf(inputPdf);

            // Add a new attachment while keeping existing ones
            editor.AddDocumentAttachment(attachmentPath, description);

            // Save the updated PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with new attachment: {outputPdf}");
    }
}