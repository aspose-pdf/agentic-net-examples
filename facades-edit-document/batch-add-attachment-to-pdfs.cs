using System;
using System.IO;
using Aspose.Pdf.Facades;

class BatchAttachmentProcessor
{
    static void Main()
    {
        // Input PDF files to process
        string[] pdfFiles = {
            "doc1.pdf",
            "doc2.pdf",
            "doc3.pdf"
        };

        // Attachment to add to each PDF
        const string attachmentPath = "attachment.pdf";
        const string attachmentDescription = "Sample attachment";

        // Verify attachment exists once
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment not found: {attachmentPath}");
            return;
        }

        foreach (string inputPath in pdfFiles)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"PDF not found: {inputPath}");
                continue;
            }

            // Create output file name (e.g., doc1_attached.pdf)
            string directory = Path.GetDirectoryName(inputPath);
            string filenameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(directory, $"{filenameWithoutExt}_attached.pdf");

            // Use PdfContentEditor to add the attachment
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(inputPath);                                 // Load the PDF
            editor.AddDocumentAttachment(attachmentPath, attachmentDescription); // Add attachment
            editor.Save(outputPath);                                   // Save the modified PDF
            editor.Close();                                            // Release resources

            Console.WriteLine($"Processed '{inputPath}' -> '{outputPath}'");
        }
    }
}