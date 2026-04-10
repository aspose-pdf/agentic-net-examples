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
        const string attachmentDescription = "Sample attachment";

        foreach (string pdfPath in pdfFiles)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                continue;
            }

            // Define output file name (original name with suffix)
            string outputPath = Path.Combine(
                Path.GetDirectoryName(pdfPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(pdfPath) + "_with_attachment.pdf");

            // Use PdfContentEditor to bind, add attachment, and save
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                editor.BindPdf(pdfPath);                                   // Load the PDF
                editor.AddDocumentAttachment(attachmentPath, attachmentDescription); // Add attachment
                editor.Save(outputPath);                                   // Save the modified PDF
            }

            Console.WriteLine($"Processed '{pdfPath}' -> '{outputPath}'");
        }
    }
}