using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the PDF that will receive the attachments
        const string targetPdfPath = "base.pdf";

        // Output PDF with all attachments added
        const string outputPdfPath = "base_with_attachments.pdf";

        // Collection of files to attach
        string[] filesToAttach = new string[]
        {
            "attachment1.docx",
            "attachment2.xlsx",
            "attachment3.txt"
        };

        // Verify that the target PDF exists
        if (!File.Exists(targetPdfPath))
        {
            Console.Error.WriteLine($"Target PDF not found: {targetPdfPath}");
            return;
        }

        // Verify that each attachment file exists
        foreach (string attachPath in filesToAttach)
        {
            if (!File.Exists(attachPath))
            {
                Console.Error.WriteLine($"Attachment file not found: {attachPath}");
                return;
            }
        }

        try
        {
            // Initialize the PdfContentEditor facade and bind the target PDF
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(targetPdfPath);

            // Attach each file to the PDF without creating a visual annotation
            foreach (string attachPath in filesToAttach)
            {
                // The second parameter is a description for the attachment
                editor.AddDocumentAttachment(attachPath, $"Attached file: {Path.GetFileName(attachPath)}");
            }

            // Save the resulting PDF with all attachments
            editor.Save(outputPdfPath);

            Console.WriteLine($"Attachments added successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}