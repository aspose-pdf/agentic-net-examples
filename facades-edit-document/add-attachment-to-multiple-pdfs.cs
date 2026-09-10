using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // List of PDF files to process
        string[] pdfFiles = { "doc1.pdf", "doc2.pdf", "doc3.pdf" };

        // Path to the attachment file and its description
        string attachmentPath = "attachment.pdf";
        string attachmentDescription = "Sample attachment";

        foreach (string pdfPath in pdfFiles)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                continue;
            }

            // Initialize the facade
            PdfContentEditor editor = new PdfContentEditor();

            // Load the PDF document
            editor.BindPdf(pdfPath);

            // Add the attachment (no annotation)
            editor.AddDocumentAttachment(attachmentPath, attachmentDescription);

            // Define output file name (original name with suffix)
            string outputPath = Path.Combine(
                Path.GetDirectoryName(pdfPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(pdfPath) + "_attached.pdf");

            // Save the modified PDF
            editor.Save(outputPath);

            // Release resources
            editor.Close();

            Console.WriteLine($"Attachment added to: {outputPath}");
        }
    }
}