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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Create default Markdown save options
            MarkdownSaveOptions mdOptions = new MarkdownSaveOptions();

            // Save the document as Markdown using the options
            doc.Save(outputPath, mdOptions);
        }

        Console.WriteLine($"PDF successfully converted to Markdown: '{outputPath}'");
    }
}