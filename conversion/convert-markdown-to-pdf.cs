using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string markdownPath = "input.md";
        const string pdfPath      = "output.pdf";

        // Verify the source file exists
        if (!File.Exists(markdownPath))
        {
            Console.Error.WriteLine($"Markdown file not found: {markdownPath}");
            return;
        }

        // Initialize load options for Markdown
        MdLoadOptions mdLoadOptions = new MdLoadOptions();

        // Load the Markdown file and convert it to PDF
        using (Document pdfDocument = new Document(markdownPath, mdLoadOptions))
        {
            // Save the resulting PDF; code blocks are preserved by default
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"Markdown converted to PDF successfully: {pdfPath}");
    }
}