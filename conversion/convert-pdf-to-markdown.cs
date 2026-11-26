using System;
using System.IO;
using Aspose.Pdf;               // All SaveOptions (including MarkdownSaveOptions) are in this namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.md";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        // Open the PDF and save it as Markdown using the default options
        using (Document doc = new Document(inputPath))
        {
            // Default MarkdownSaveOptions – no custom settings required
            MarkdownSaveOptions mdOptions = new MarkdownSaveOptions();

            // Save the document; the explicit options ensure Markdown output
            doc.Save(outputPath, mdOptions);
        }

        Console.WriteLine($"PDF successfully converted to Markdown: {outputPath}");
    }
}