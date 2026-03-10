using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputMd = "metadata.md";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load PDF metadata using the PdfFileInfo facade.
        // PdfFileInfo implements IDisposable, so wrap it in a using block.
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPdf))
        {
            // Retrieve the required metadata fields. Null‑coalesce to empty strings.
            string title    = pdfInfo.Title    ?? string.Empty;
            string author   = pdfInfo.Author   ?? string.Empty;
            string subject  = pdfInfo.Subject  ?? string.Empty;
            string keywords = pdfInfo.Keywords ?? string.Empty;

            // Build the Markdown content.
            string markdown = $"# PDF Metadata{Environment.NewLine}{Environment.NewLine}" +
                              $"**Title:** {title}{Environment.NewLine}{Environment.NewLine}" +
                              $"**Author:** {author}{Environment.NewLine}{Environment.NewLine}" +
                              $"**Subject:** {subject}{Environment.NewLine}{Environment.NewLine}" +
                              $"**Keywords:** {keywords}{Environment.NewLine}";

            // Write the Markdown to the output file.
            File.WriteAllText(outputMd, markdown);
            Console.WriteLine($"Metadata extracted and saved to '{outputMd}'.");
        }
    }
}