using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Output Markdown file path
        const string outputMdPath = "pdf_info.md";

        // Verify that the PDF file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        // Use PdfFileInfo (a SaveableFacade) to read document metadata
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPdfPath))
        {
            // Retrieve required metadata; null values are replaced with empty strings
            string title    = pdfInfo.Title    ?? string.Empty;
            string author   = pdfInfo.Author   ?? string.Empty;
            string subject  = pdfInfo.Subject  ?? string.Empty;
            string keywords = pdfInfo.Keywords ?? string.Empty;

            // Build Markdown content
            string markdown = $"# PDF Metadata\n\n" +
                              $"**Title:** {title}\n\n" +
                              $"**Author:** {author}\n\n" +
                              $"**Subject:** {subject}\n\n" +
                              $"**Keywords:** {keywords}\n";

            // Write the Markdown document to disk
            File.WriteAllText(outputMdPath, markdown);
        }

        Console.WriteLine($"Metadata extracted and saved to '{outputMdPath}'.");
    }
}