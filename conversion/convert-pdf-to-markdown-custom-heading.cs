using System;
using System.IO;
using Aspose.Pdf; // Document, MarkdownSaveOptions, HeadingRecognitionStrategy, HeadingLevels

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputMd = "output.md";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Configure Markdown conversion options with custom heading mapping
            var mdOptions = new MarkdownSaveOptions
            {
                // Use heuristic heading recognition (based on font size)
                HeadingRecognitionStrategy = HeadingRecognitionStrategy.Heuristic,
                // Define custom heading levels; the threshold determines how close
                // font sizes are considered the same heading level.
                HeadingLevels = new HeadingLevels(0.05) // example threshold
            };

            // Save the document as Markdown using the configured options
            doc.Save(outputMd, mdOptions);
        }

        Console.WriteLine($"Markdown file saved to '{outputMd}'.");
    }
}
