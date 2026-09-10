using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string attachmentPath = "attachment.pdf";
        const string description = "Sample attachment description";
        const string outputPath = "output.pdf";

        // Verify the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – '{pdfPath}'.");
            return;
        }

        // Verify the attachment file exists before invoking AddDocumentAttachment
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Error: Attachment file not found – '{attachmentPath}'.");
            return;
        }

        try
        {
            // Initialize the facade, bind the PDF, add the attachment, and save the result
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(pdfPath);
            editor.AddDocumentAttachment(attachmentPath, description);
            editor.Save(outputPath);

            Console.WriteLine($"Attachment added successfully. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Catch any unexpected errors (e.g., I/O, permission issues)
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}