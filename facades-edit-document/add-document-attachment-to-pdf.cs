using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string attachmentPath = "invoice.pdf";
        const string outputPath = "output.pdf";

        // Verify source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {pdfPath}");
            return;
        }

        // Verify attachment file exists
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        // Create the facade, bind the PDF, add the attachment, and save the result
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(pdfPath);
        // The MIME type is inferred from the .pdf extension (application/pdf)
        editor.AddDocumentAttachment(attachmentPath, "Invoice Document");
        editor.Save(outputPath);

        Console.WriteLine($"Attachment added and saved to '{outputPath}'.");
    }
}