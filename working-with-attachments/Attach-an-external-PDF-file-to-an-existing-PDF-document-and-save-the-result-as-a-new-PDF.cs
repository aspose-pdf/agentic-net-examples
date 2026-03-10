using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, the PDF to attach, and the output PDF
        const string sourcePdfPath = "source.pdf";
        const string attachmentPdfPath = "attachment.pdf";
        const string outputPdfPath = "output.pdf";

        // Verify that the required files exist
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(attachmentPdfPath))
        {
            Console.Error.WriteLine($"Attachment PDF not found: {attachmentPdfPath}");
            return;
        }

        // Initialize the PdfContentEditor facade
        PdfContentEditor editor = new PdfContentEditor();

        // Bind the existing PDF document that will receive the attachment
        editor.BindPdf(sourcePdfPath);

        // Define the rectangle (position and size) for the file attachment annotation.
        // System.Drawing.Rectangle is required by the CreateFileAttachment method.
        System.Drawing.Rectangle annotationRect = new System.Drawing.Rectangle(100, 100, 200, 200);

        // Create a file attachment annotation on page 1.
        // Parameters: rectangle, annotation contents, path to the file to attach,
        // page number (1‑based), and the icon name ("Graph", "PushPin", "Paperclip", "Tag").
        editor.CreateFileAttachment(
            annotationRect,
            "Attached PDF document",
            attachmentPdfPath,
            1,
            "Paperclip");

        // Save the modified PDF to the specified output path
        editor.Save(outputPdfPath);

        // Close the editor (optional but good practice)
        editor.Close();

        Console.WriteLine($"Attachment added and saved to '{outputPdfPath}'.");
    }
}