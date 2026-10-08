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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load PDF and extract raw text preserving spaces and line breaks
        using (Document doc = new Document(inputPath))
        {
            TextAbsorber absorber = new TextAbsorber();
            absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);
            doc.Pages.Accept(absorber);
            string rawText = absorber.Text;

            // Split into paragraphs (empty line separation)
            string[] paragraphs = rawText.Split(
                new[] { "\r\n\r\n", "\n\n", "\r\r" },
                StringSplitOptions.RemoveEmptyEntries);

            // Write to markdown, using fenced code blocks to keep indentation
            using (StreamWriter writer = new StreamWriter(outputPath, false))
            {
                foreach (string para in paragraphs)
                {
                    writer.WriteLine("```");
                    writer.WriteLine(para.TrimEnd()); // keep leading spaces, remove trailing newline
                    writer.WriteLine("```");
                    writer.WriteLine(); // blank line between paragraphs
                }
            }
        }

        Console.WriteLine($"Markdown file created at '{outputPath}'.");
    }
}