using System;
using System.IO;

using Aspose.Pdf.Facades;

class BatchAttachmentProcessor
{
    static void Main()
    {
        // Input PDF files (could be read from a directory or defined manually)
        string[] pdfFiles = {
            "doc1.pdf",
            "doc2.pdf",
            "doc3.pdf"
        };

        // Path to the file that will be attached to every PDF
        const string attachmentPath = "attachment.pdf";
        const string attachmentDescription = "Shared attachment";

        // Verify that the attachment exists before processing
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment not found: {attachmentPath}");
            return;
        }

        foreach (string inputPath in pdfFiles)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"Input PDF not found: {inputPath}");
                continue;
            }

            // Create an output file name (e.g., original name with "_attached" suffix)
            string outputPath = Path.Combine(
                Path.GetDirectoryName(inputPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(inputPath) + "_attached.pdf");

            // Use PdfContentEditor facade to bind, add attachment, and save
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                // Load the source PDF
                editor.BindPdf(inputPath);

                // Add the same attachment to the document (no annotation)
                editor.AddDocumentAttachment(attachmentPath, attachmentDescription);

                // Save the modified PDF
                editor.Save(outputPath);
            }

            Console.WriteLine($"Processed '{inputPath}' → '{outputPath}'");
        }
    }
}