using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string markdownPath = "metadata.md";
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";

        // Verify input files exist
        if (!File.Exists(markdownPath))
        {
            Console.Error.WriteLine($"Markdown file not found: {markdownPath}");
            return;
        }

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {inputPdfPath}");
            return;
        }

        // Simple parsing of Markdown for metadata key/value pairs
        string title = null, author = null, subject = null, keywords = null;

        foreach (var line in File.ReadAllLines(markdownPath))
        {
            var trimmed = line.Trim();

            if (trimmed.StartsWith("Title:", StringComparison.OrdinalIgnoreCase))
                title = trimmed.Substring("Title:".Length).Trim();
            else if (trimmed.StartsWith("Author:", StringComparison.OrdinalIgnoreCase))
                author = trimmed.Substring("Author:".Length).Trim();
            else if (trimmed.StartsWith("Subject:", StringComparison.OrdinalIgnoreCase))
                subject = trimmed.Substring("Subject:".Length).Trim();
            else if (trimmed.StartsWith("Keywords:", StringComparison.OrdinalIgnoreCase))
                keywords = trimmed.Substring("Keywords:".Length).Trim();
        }

        // Use PdfFileInfo facade to bind the PDF and set metadata
        using (PdfFileInfo pdfInfo = new PdfFileInfo())
        {
            // Load the PDF document
            pdfInfo.BindPdf(inputPdfPath);

            // Apply metadata if values were found
            if (!string.IsNullOrEmpty(title))    pdfInfo.Title    = title;
            if (!string.IsNullOrEmpty(author))   pdfInfo.Author   = author;
            if (!string.IsNullOrEmpty(subject))  pdfInfo.Subject  = subject;
            if (!string.IsNullOrEmpty(keywords)) pdfInfo.Keywords = keywords;

            // Save the updated PDF with new metadata
            pdfInfo.SaveNewInfo(outputPdfPath);
        }

        Console.WriteLine($"Metadata applied and saved to '{outputPdfPath}'.");
    }
}