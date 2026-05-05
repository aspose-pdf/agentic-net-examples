using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string attachmentPath = "invoice.pdf";
        const string description = "Invoice Document";
        const string mimeType = "application/pdf";

        // Verify source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdf}");
            return;
        }

        // Verify attachment file exists
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        // Use the Facades API with proper resource disposal
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the PDF
            editor.BindPdf(inputPdf);

            // Open the attachment as a stream and add it with explicit MIME type and description
            using (FileStream attachmentStream = File.OpenRead(attachmentPath))
            {
                editor.AddDocumentAttachment(attachmentStream, description, mimeType);
            }

            // Save the updated PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Attachment added and saved to '{outputPdf}'.");
    }
}
