using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths
        const string sourcePdfPath   = "source.pdf";          // PDF to which attachments will be added
        const string outputPdfPath   = "source_with_attachments.pdf";
        const string attachmentsDir  = "Attachments";        // Folder containing files to attach

        // Validate source PDF
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }

        // Validate attachments folder
        if (!Directory.Exists(attachmentsDir))
        {
            Console.Error.WriteLine($"Attachments folder not found: {attachmentsDir}");
            return;
        }

        // Gather all files to attach
        string[] attachmentFiles = Directory.GetFiles(attachmentsDir);
        if (attachmentFiles.Length == 0)
        {
            Console.WriteLine("No files found to attach.");
            return;
        }

        // Use PdfContentEditor facade to bind the PDF and add attachments
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF document
            editor.BindPdf(sourcePdfPath);

            // Attach each file
            foreach (string filePath in attachmentFiles)
            {
                // Description can be any string; here we use the file name
                string description = Path.GetFileName(filePath);
                editor.AddDocumentAttachment(filePath, description);
                Console.WriteLine($"Attached: {description}");
            }

            // Save the resulting PDF with all attachments
            editor.Save(outputPdfPath);
        }

        Console.WriteLine($"All attachments added. Output saved to '{outputPdfPath}'.");
    }
}