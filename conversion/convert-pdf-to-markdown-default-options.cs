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
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Create MarkdownSaveOptions with default settings
                MarkdownSaveOptions mdOptions = new MarkdownSaveOptions();

                // Save the document as Markdown
                doc.Save(outputPath, mdOptions);
            }

            Console.WriteLine($"PDF successfully converted to Markdown: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}