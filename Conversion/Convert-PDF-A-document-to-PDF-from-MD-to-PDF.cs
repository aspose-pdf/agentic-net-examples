using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace (contains MdLoadOptions)

class Program
{
    static void Main()
    {
        // Input Markdown file (PDF/A source is not applicable here; we convert MD to a regular PDF)
        const string markdownPath = "input.md";
        // Desired output PDF file
        const string pdfPath = "output.pdf";

        if (!File.Exists(markdownPath))
        {
            Console.Error.WriteLine($"Markdown file not found: {markdownPath}");
            return;
        }

        // Initialize load options for Markdown format
        MdLoadOptions mdLoadOptions = new MdLoadOptions();

        // Load the Markdown file and convert it to a PDF document
        using (Document pdfDocument = new Document(markdownPath, mdLoadOptions))
        {
            // Save as a regular PDF (no special SaveOptions required)
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"Markdown converted to PDF successfully: {pdfPath}");
    }
}