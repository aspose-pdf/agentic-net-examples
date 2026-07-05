using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Collection of PDF files to process
        string[] pdfFiles = { "doc1.pdf", "doc2.pdf", "doc3.pdf" };

        // Attachment to add to each PDF
        const string attachmentPath = "attachment.pdf";
        const string description = "Sample attachment";

        foreach (string inputPath in pdfFiles)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            // Define output file name (original name with suffix)
            string outputPath = Path.Combine(
                Path.GetDirectoryName(inputPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(inputPath) + "_withAttachment.pdf");

            // Use PdfContentEditor facade to bind, attach, and save
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                editor.BindPdf(inputPath);                                 // Load the PDF
                editor.AddDocumentAttachment(attachmentPath, description); // Add the attachment
                editor.Save(outputPath);                                   // Save the modified PDF
            }

            Console.WriteLine($"Processed '{inputPath}' → '{outputPath}'");
        }
    }
}