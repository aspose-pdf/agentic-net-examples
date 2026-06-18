using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string attachmentPath = "invoice.pdf";
        const string attachmentDescription = "Invoice Document";

        // Verify that the source PDF and attachment file exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        // Initialize the PdfContentEditor facade
        PdfContentEditor editor = new PdfContentEditor();

        // Bind the existing PDF document
        editor.BindPdf(inputPdfPath);

        // Add the attachment (the MIME type is inferred from the file extension,
        // which for a .pdf file is application/pdf)
        editor.AddDocumentAttachment(attachmentPath, attachmentDescription);

        // Save the modified PDF
        editor.Save(outputPdfPath);

        // Release resources held by the facade
        editor.Close();

        Console.WriteLine($"Attachment added and saved to '{outputPdfPath}'.");
    }
}