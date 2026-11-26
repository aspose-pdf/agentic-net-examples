using System;
using System.IO;
using Aspose.Pdf;               // All SaveOptions subclasses are in this namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.md";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and save it as Markdown with the required options
        using (Document doc = new Document(inputPath))
        {
            // MarkdownSaveOptions is a non‑PDF SaveOptions subclass; it must be passed explicitly
            MarkdownSaveOptions mdOptions = new MarkdownSaveOptions
            {
                UseImageHtmlTag        = true,      // Emit <img> tags for images
                ResourcesDirectoryName = "images"   // Store extracted images in the "images" folder
            };

            doc.Save(outputPath, mdOptions);
        }

        Console.WriteLine($"Markdown file saved to '{outputPath}'.");
    }
}