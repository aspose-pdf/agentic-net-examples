using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output_with_attachment.pdf";
        const string attachmentPath = "attachment_file.pdf";
        const string attachmentDescription = "Description of the attachment";

        // Verify source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – '{inputPdfPath}'.");
            return;
        }

        // Verify attachment file exists before attempting to add it
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Error: Attachment file not found – '{attachmentPath}'.");
            return;
        }

        try
        {
            // Create and bind the PDF using PdfContentEditor (facade API)
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(inputPdfPath);

            // Add the attachment (no annotation) – both parameters are strings
            editor.AddDocumentAttachment(attachmentPath, attachmentDescription);

            // Save the modified PDF
            editor.Save(outputPdfPath);

            Console.WriteLine($"Attachment added successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            // General error handling – logs any unexpected issues
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}