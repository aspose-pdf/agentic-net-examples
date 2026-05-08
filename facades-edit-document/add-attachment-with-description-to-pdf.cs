using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf       = "input.pdf";
        const string attachmentPath = "attachment_file.pdf";
        const string description    = "Description of attachment_file";
        const string outputPdf      = "output_with_attachment.pdf";

        // Verify that source PDF and attachment exist
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

        // Use PdfContentEditor (Facades API) to add the attachment
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the PDF document
            editor.BindPdf(inputPdf);

            // Add the attachment with a descriptive label
            editor.AddDocumentAttachment(attachmentPath, description);

            // Save the updated PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Attachment added successfully. Saved to '{outputPdf}'.");
    }
}