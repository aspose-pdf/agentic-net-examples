using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF to which the attachment will be added
        const string inputPdfPath = "input.pdf";
        // File to be attached (embedded) into the PDF
        const string attachmentPath = "attachment_file.pdf";
        // Description of the attachment (optional)
        const string attachmentDescription = "Embedded attachment file";

        // Output PDF containing the embedded file
        const string outputPdfPath = "output_with_attachment.pdf";

        // Verify that the source files exist
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

        // PdfContentEditor is a Facades class used to modify existing PDFs.
        // It does NOT implement IDisposable, so we do not wrap it in a using block.
        PdfContentEditor editor = new PdfContentEditor();

        // Bind the existing PDF document
        editor.BindPdf(inputPdfPath);

        // Add the file as an embedded attachment without any visible annotation.
        // This creates an entry in the PDF's EmbeddedFiles name tree.
        editor.AddDocumentAttachment(attachmentPath, attachmentDescription);

        // Save the modified PDF. The attachment is now part of the PDF structure.
        editor.Save(outputPdfPath);

        Console.WriteLine($"Attachment added. Output saved to '{outputPdfPath}'.");
    }
}