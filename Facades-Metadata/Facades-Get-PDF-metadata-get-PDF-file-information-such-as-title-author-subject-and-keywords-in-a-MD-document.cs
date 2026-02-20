using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF file path (change as needed)
        const string pdfPath = "input.pdf";

        // Output Markdown file path
        const string mdPath = "metadata.md";

        // Verify that the PDF file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        try
        {
            // Load PDF metadata using the Facades API
            PdfFileInfo pdfInfo = new PdfFileInfo(pdfPath);

            // Retrieve required metadata fields
            string title = pdfInfo.Title ?? "(none)";
            string author = pdfInfo.Author ?? "(none)";
            string subject = pdfInfo.Subject ?? "(none)";
            string keywords = pdfInfo.Keywords ?? "(none)";

            // Build Markdown content
            string markdown = $"# PDF Metadata{Environment.NewLine}" +
                              $"{Environment.NewLine}" +
                              $"- **Title:** {title}{Environment.NewLine}" +
                              $"- **Author:** {author}{Environment.NewLine}" +
                              $"- **Subject:** {subject}{Environment.NewLine}" +
                              $"- **Keywords:** {keywords}{Environment.NewLine}";

            // Write the Markdown file
            File.WriteAllText(mdPath, markdown);

            Console.WriteLine($"Metadata extracted and saved to '{mdPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}