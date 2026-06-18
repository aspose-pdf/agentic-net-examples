using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Path to the input Markdown file
        const string markdownPath = "input.md";

        // Path where the resulting PDF will be saved
        const string pdfPath = "output.pdf";

        // Verify that the Markdown file exists
        if (!File.Exists(markdownPath))
        {
            Console.Error.WriteLine($"File not found: {markdownPath}");
            return;
        }

        // Initialize load options for Markdown format
        MdLoadOptions mdLoadOptions = new MdLoadOptions();

        // Load the Markdown file and convert it to a PDF document
        using (Document pdfDocument = new Document(markdownPath, mdLoadOptions))
        {
            // Save the document as PDF; code blocks are preserved by default
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"Markdown file successfully converted to PDF: {pdfPath}");
    }
}