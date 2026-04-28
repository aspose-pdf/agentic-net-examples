using System;
using System.IO;
using Aspose.Pdf; // Aspose.Pdf namespace contains Document, MarkdownSaveOptions

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Output Markdown file path
        const string outputMdPath = "output.md";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document, process, and save as Markdown
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Create default Markdown save options
            MarkdownSaveOptions mdOptions = new MarkdownSaveOptions();

            // Save the document as Markdown using the options
            pdfDocument.Save(outputMdPath, mdOptions);
        }

        Console.WriteLine($"PDF successfully converted to Markdown: '{outputMdPath}'");
    }
}