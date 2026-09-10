using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePdf = "source.pdf";          // PDF to which the attachment will be added
        const string attachmentPdf = "invoice.pdf";     // PDF file to attach (MIME type inferred as application/pdf)
        const string outputPdf = "output.pdf";          // Resulting PDF with the attachment

        // Verify that both files exist before proceeding
        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdf}");
            return;
        }
        if (!File.Exists(attachmentPdf))
        {
            Console.Error.WriteLine($"Attachment PDF not found: {attachmentPdf}");
            return;
        }

        // Initialize the facade and bind the source PDF
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(sourcePdf);

        // Add the attachment with the required description.
        // The attached file is a PDF, so its MIME type is automatically application/pdf.
        editor.AddDocumentAttachment(attachmentPdf, "Invoice Document");

        // Save the modified document
        editor.Save(outputPdf);

        Console.WriteLine($"Attachment added and saved to '{outputPdf}'.");
    }
}