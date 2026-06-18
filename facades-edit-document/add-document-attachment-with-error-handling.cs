using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath      = "input.pdf";
        const string attachmentPath    = "attachment_file.pdf";
        const string outputPdfPath     = "output_with_attachment.pdf";
        const string attachmentDesc    = "Description of the attached file";

        // Verify source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – '{inputPdfPath}'.");
            return;
        }

        // Verify attachment file exists before attempting to add it
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Error: Attachment file not found – '{attachmentPath}'. Skipping attachment.");
        }

        try
        {
            // Create the facade editor and bind the source PDF
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(inputPdfPath);

            // Add the attachment only if the file is present
            if (File.Exists(attachmentPath))
            {
                editor.AddDocumentAttachment(attachmentPath, attachmentDesc);
            }

            // Save the modified PDF
            editor.Save(outputPdfPath);
            Console.WriteLine($"PDF saved with attachment (if provided) to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}