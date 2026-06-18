using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for source PDF, attachment file, and output PDF
        const string sourcePdfPath = "source.pdf";
        const string attachmentPath = "large_file.bin";
        const string outputPdfPath = "output_with_attachment.pdf";

        // Verify files exist
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        // Initialize the facade for editing PDF content
        PdfContentEditor editor = new PdfContentEditor();

        // Bind the existing PDF document (load operation)
        editor.BindPdf(sourcePdfPath);

        // Open the attachment as a FileStream.
        // FileStream reads data on demand and uses an internal buffer,
        // avoiding loading the entire file into memory.
        using (FileStream attachmentStream = File.OpenRead(attachmentPath))
        {
            // Add the attachment to the PDF.
            // The stream is passed directly; Aspose.Pdf reads it in chunks internally.
            editor.AddDocumentAttachment(
                attachmentStream,
                Path.GetFileName(attachmentPath), // attachment name inside PDF
                "Large file attachment");          // description
        }

        // Save the modified PDF (save operation)
        editor.Save(outputPdfPath);

        Console.WriteLine($"Attachment added and PDF saved to '{outputPdfPath}'.");
    }
}