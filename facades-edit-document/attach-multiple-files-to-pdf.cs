using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the PDF that will receive the attachments
        const string targetPdfPath = "target.pdf";

        // Output PDF with all attachments added
        const string outputPdfPath = "target_with_attachments.pdf";

        // Collection of files to attach
        string[] filesToAttach = new string[]
        {
            "attachment1.pdf",
            "image.png",
            "document.docx"
        };

        // Verify that the target PDF exists
        if (!File.Exists(targetPdfPath))
        {
            Console.Error.WriteLine($"Target PDF not found: {targetPdfPath}");
            return;
        }

        // Verify that each attachment file exists before processing
        foreach (string file in filesToAttach)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Attachment file not found: {file}");
                return;
            }
        }

        // Create the PdfContentEditor facade (does NOT implement IDisposable)
        PdfContentEditor editor = new PdfContentEditor();

        // Bind the target PDF document
        editor.BindPdf(targetPdfPath);

        // Attach each file to the PDF
        foreach (string attachmentPath in filesToAttach)
        {
            // Use the file name as the description (you can customize as needed)
            string description = Path.GetFileName(attachmentPath);
            editor.AddDocumentAttachment(attachmentPath, description);
        }

        // Save the resulting PDF with all attachments
        editor.Save(outputPdfPath);

        Console.WriteLine($"Attachments added. Output saved to '{outputPdfPath}'.");
    }
}