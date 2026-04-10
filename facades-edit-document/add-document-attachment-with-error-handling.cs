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
        const string attachmentDesc    = "Description of the attachment file";

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
            // Create the facade and bind the source PDF
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(inputPdfPath);

            // Add attachment only if the file is present
            if (File.Exists(attachmentPath))
            {
                editor.AddDocumentAttachment(attachmentPath, attachmentDesc);
            }

            // Save the modified PDF
            editor.Save(outputPdfPath);
            Console.WriteLine($"PDF saved successfully to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}