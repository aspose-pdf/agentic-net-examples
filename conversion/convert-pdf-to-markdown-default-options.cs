using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.md";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF document.
        using (Document pdfDoc = new Document(inputPath))
        {
            // Create default Markdown save options.
            MarkdownSaveOptions mdOptions = new MarkdownSaveOptions();

            // Save the document as Markdown using the options.
            pdfDoc.Save(outputPath, mdOptions);
        }

        Console.WriteLine($"PDF successfully converted to Markdown: '{outputPath}'");
    }
}