using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";          // Existing PDF
        const string outputPdfPath = "output_with_attachments.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        // Define the new attachment files (ensure they exist)
        string[] attachmentFiles = new string[]
        {
            "attachment1.pdf",
            "attachment2.docx",
            "attachment3.png"
        };

        // Verify each attachment file exists before proceeding
        foreach (string attPath in attachmentFiles)
        {
            if (!File.Exists(attPath))
            {
                Console.Error.WriteLine($"Attachment file not found: {attPath}");
                return;
            }
        }

        // Use PdfContentEditor facade to manipulate attachments
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF document
            editor.BindPdf(inputPdfPath);

            // Remove all existing attachments
            editor.DeleteAttachments();

            // Add the fresh set of attachments
            foreach (string attPath in attachmentFiles)
            {
                string description = Path.GetFileNameWithoutExtension(attPath);
                editor.AddDocumentAttachment(attPath, description);
            }

            // Save the modified PDF
            editor.Save(outputPdfPath);
        }

        Console.WriteLine($"Attachments refreshed. Output saved to '{outputPdfPath}'.");
    }
}