using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the resulting PDF
        const string sourcePdfPath = "input.pdf";
        const string outputPdfPath = "output_with_attachment.pdf";

        // Sample data to embed as an attachment (could be any binary content)
        byte[] attachmentData = System.Text.Encoding.UTF8.GetBytes("This is the content of the attachment.");

        // Name and description for the attachment
        const string attachmentName = "sample.txt";
        const string attachmentDescription = "Sample text attachment embedded from memory stream.";

        // Ensure the source PDF exists
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }

        // Use PdfContentEditor to bind the PDF, add the attachment, and save the result
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF file
            editor.BindPdf(sourcePdfPath);

            // Create a memory stream for the attachment data
            using (MemoryStream attachmentStream = new MemoryStream(attachmentData))
            {
                // Add the attachment from the memory stream
                editor.AddDocumentAttachment(attachmentStream, attachmentName, attachmentDescription);
            }

            // Save the modified PDF with the embedded attachment
            editor.Save(outputPdfPath);
        }

        Console.WriteLine($"Attachment added and PDF saved to '{outputPdfPath}'.");
    }
}