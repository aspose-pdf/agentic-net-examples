using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath      = "input.pdf";
        const string attachmentFilePath = "newAttachment.pdf";
        const string attachmentDesc    = "New attachment description";
        const string outputPdfPath     = "output.pdf";

        // Validate input files
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(attachmentFilePath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentFilePath}");
            return;
        }

        try
        {
            // PdfContentEditor is a facade that supports IDisposable
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                // Load the existing PDF
                editor.BindPdf(inputPdfPath);

                // Add a new attachment; existing attachments are preserved automatically
                editor.AddDocumentAttachment(attachmentFilePath, attachmentDesc);

                // Save the modified PDF
                editor.Save(outputPdfPath);
            }

            Console.WriteLine($"Attachment added successfully. Saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}