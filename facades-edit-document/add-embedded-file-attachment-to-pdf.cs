using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, the file to attach, and the resulting PDF
        const string sourcePdfPath      = "input.pdf";
        const string attachmentFilePath = "attachment_file.pdf";
        const string outputPdfPath      = "output.pdf";

        // Description for the attachment (appears in the attachment list)
        const string attachmentDescription = "Sample attachment embedded in the PDF";

        // Ensure the source PDF and attachment exist
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }

        if (!File.Exists(attachmentFilePath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentFilePath}");
            return;
        }

        // Use PdfContentEditor (Facades API) to add the attachment.
        // PdfContentEditor does not implement IDisposable, so a plain instance is sufficient.
        PdfContentEditor editor = new PdfContentEditor();

        // Bind the existing PDF document.
        editor.BindPdf(sourcePdfPath);

        // Add the attachment without creating a visible annotation.
        // This embeds the file in the PDF's EmbeddedFiles name tree.
        editor.AddDocumentAttachment(attachmentFilePath, attachmentDescription);

        // Save the modified PDF.
        editor.Save(outputPdfPath);

        Console.WriteLine($"Attachment added and PDF saved to '{outputPdfPath}'.");
    }
}