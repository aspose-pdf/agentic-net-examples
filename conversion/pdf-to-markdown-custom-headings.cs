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
            // Configure Markdown conversion options
            MarkdownSaveOptions mdOptions = new MarkdownSaveOptions();

            // Custom heading level mapping based on font size.
            // Example: treat specific font sizes as H1, H2, H3, etc.
            // The HeadingLevels class can be populated accordingly.
            // Here we assign a new instance; replace with actual mapping as needed.
            mdOptions.HeadingLevels = new HeadingLevels();

            // Optional: choose a heading style (Atx, Setext, etc.)
            // mdOptions.HeadingStyle = HeadingStyle.Atx;

            // Save the PDF as Markdown using the configured options
            doc.Save(outputPath, mdOptions);
        }

        Console.WriteLine($"PDF converted to Markdown: {outputPath}");
    }
}