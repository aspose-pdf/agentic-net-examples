using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API and SaveOptions subclasses

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.md";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Configure Markdown export options
            MarkdownSaveOptions mdOptions = new MarkdownSaveOptions {
                UseImageHtmlTag        = true,          // Emit <img> tags for images
                ResourcesDirectoryName = "md_images"    // Folder where extracted images will be placed
            };

            // Save as Markdown; must pass the SaveOptions instance
            doc.Save(outputPath, mdOptions);
        }

        Console.WriteLine($"PDF successfully converted to Markdown: {outputPath}");
    }
}