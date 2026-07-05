using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the resulting PDF
        const string sourcePdfPath = "source.pdf";
        const string outputPdfPath = "output_with_attachment.pdf";

        // Verify source PDF exists
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }

        // Example data to embed as an attachment (could be any binary content)
        byte[] attachmentData = System.Text.Encoding.UTF8.GetBytes("This is the content of the attachment.");
        const string attachmentName = "example.txt";
        const string attachmentDescription = "Sample text attachment";

        // Create a memory stream for the attachment data
        using (MemoryStream attachmentStream = new MemoryStream(attachmentData))
        {
            // Initialize the PdfContentEditor facade
            PdfContentEditor editor = new PdfContentEditor();

            // Bind the source PDF for editing (using a file path)
            editor.BindPdf(sourcePdfPath);

            // Add the attachment from the memory stream (no annotation is created)
            editor.AddDocumentAttachment(attachmentStream, attachmentName, attachmentDescription);

            // Save the modified PDF to the output path
            editor.Save(outputPdfPath);

            // Close the editor (optional, as it does not implement IDisposable)
            editor.Close();

            Console.WriteLine($"Attachment added and PDF saved to '{outputPdfPath}'.");
        }
    }
}