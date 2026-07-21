using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Output Markdown file path
        const string outputMd = "output.md";

        // Verify the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document and convert it to Markdown using explicit save options
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Create default Markdown save options
            MarkdownSaveOptions mdOptions = new MarkdownSaveOptions();

            // Save the document as Markdown
            pdfDoc.Save(outputMd, mdOptions);
        }

        Console.WriteLine($"PDF successfully converted to Markdown: '{outputMd}'");
    }
}