using System;
using System.IO;
using Aspose.Pdf;

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

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Configure Markdown save options; disable HTML <img> tags for images
                MarkdownSaveOptions mdOptions = new MarkdownSaveOptions
                {
                    UseImageHtmlTag = false
                };

                // Save the document as Markdown using the explicit options
                doc.Save(outputPath, mdOptions);
            }

            Console.WriteLine($"PDF successfully saved as Markdown to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}