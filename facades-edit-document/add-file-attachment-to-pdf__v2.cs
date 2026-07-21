using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";          // Existing PDF
        const string attachmentPath = "attachment_file.pdf"; // File to attach
        const string outputPdfPath = "output.pdf";         // Resulting PDF

        // Verify that both files exist before proceeding
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        // Use PdfContentEditor (a facade) to add the attachment.
        // The facade implements IDisposable, so wrap it in a using block.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF document.
            editor.BindPdf(inputPdfPath);

            // Add the file attachment without any visual annotation.
            editor.AddDocumentAttachment(attachmentPath, "Description of attachment_file");

            // Save the modified PDF to a new file.
            editor.Save(outputPdfPath);
        }

        Console.WriteLine($"Attachment added. Output saved to '{outputPdfPath}'.");
    }
}