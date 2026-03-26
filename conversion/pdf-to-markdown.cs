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

        using (Document doc = new Document(inputPath))
        {
            MarkdownSaveOptions mdOptions = new MarkdownSaveOptions();
            doc.Save(outputPath, mdOptions);
        }

        Console.WriteLine($"PDF converted to Markdown: {outputPath}");
    }
}