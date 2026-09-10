using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, the file to embed, and the resulting PDF
        const string inputPdfPath      = "input.pdf";
        const string attachmentFilePath = "attachment_file.pdf";
        const string outputPdfPath     = "output_with_attachment.pdf";

        // Verify that the required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(attachmentFilePath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentFilePath}");
            return;
        }

        // Create the PdfContentEditor facade (does NOT implement IDisposable)
        PdfContentEditor editor = new PdfContentEditor();

        // Load the existing PDF document into the editor
        editor.BindPdf(inputPdfPath);

        // Add the external file as an embedded attachment.
        // This creates an embedded file entry in the PDF structure without a visible annotation.
        editor.AddDocumentAttachment(attachmentFilePath, "Sample attachment description");

        // Persist the changes to a new PDF file
        editor.Save(outputPdfPath);

        Console.WriteLine($"Embedded attachment added successfully. Output saved to '{outputPdfPath}'.");
    }
}