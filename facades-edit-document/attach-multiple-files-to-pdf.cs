using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF to which attachments will be added
        const string sourcePdfPath = "source.pdf";
        // Output PDF with all attachments
        const string outputPdfPath = "output_with_attachments.pdf";

        // Collection of files to attach
        string[] attachmentFiles = new string[]
        {
            "file1.docx",
            "image.png",
            "data.xlsx"
        };

        // Verify source PDF exists
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }

        // Verify each attachment exists before processing
        foreach (string att in attachmentFiles)
        {
            if (!File.Exists(att))
            {
                Console.Error.WriteLine($"Attachment not found: {att}");
                return;
            }
        }

        try
        {
            // Use PdfContentEditor to bind the source PDF
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                editor.BindPdf(sourcePdfPath);

                // Attach each file with a simple description
                foreach (string attPath in attachmentFiles)
                {
                    // Description can be any string; here we use the file name
                    string description = Path.GetFileName(attPath);
                    editor.AddDocumentAttachment(attPath, description);
                }

                // Save the resulting PDF
                editor.Save(outputPdfPath);
            }

            Console.WriteLine($"Attachments added successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}