using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_with_attachments.pdf";
        const string attachmentPath = "attachment_file.pdf";
        const string attachmentDescription = "Sample attachment for accessibility";

        if (!File.Exists(inputPdf) || !File.Exists(attachmentPath))
        {
            Console.Error.WriteLine("Required files not found.");
            return;
        }

        // Bind the source PDF and add attachments
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPdf);

            // Add a hidden document attachment (no visible annotation)
            editor.AddDocumentAttachment(attachmentPath, attachmentDescription);

            // Add a visible file attachment annotation on page 1
            // The API expects System.Drawing.Rectangle for the annotation rectangle
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 100, 200, 200);
            editor.CreateFileAttachment(rect, "Attachment", attachmentPath, 1, "Paperclip");

            // Save the modified PDF with the embedded attachments
            editor.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with attachments: {outputPdf}");
    }
}