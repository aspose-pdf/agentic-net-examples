using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.md";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document.
        using (Document doc = new Document(inputPath))
        {
            // Process paragraphs to retain layout information such as indents.
            doc.ProcessParagraphs();

            // Configure markdown save options (default settings preserve indentation).
            MarkdownSaveOptions mdOptions = new MarkdownSaveOptions();

            // Save the document as a formatted markdown file.
            doc.Save(outputPath, mdOptions);
        }

        Console.WriteLine($"Markdown file saved to '{outputPath}'.");
    }
}