using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace provides MarkdownSaveOptions, HeadingRecognitionStrategy, HeadingLevels

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the target Markdown file
        const string inputPdfPath = "input.pdf";
        const string outputMdPath = "output.md";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Configure Markdown conversion options
            MarkdownSaveOptions mdOptions = new MarkdownSaveOptions
            {
                // Use a heuristic strategy for heading detection
                HeadingRecognitionStrategy = HeadingRecognitionStrategy.Heuristic,

                // Define custom heading level mapping.
                // The HeadingLevels class allows configuring font‑size based thresholds.
                // Here we create a default instance; you can adjust its properties as needed.
                HeadingLevels = new HeadingLevels()
            };

            // Save the document as Markdown using the configured options
            pdfDoc.Save(outputMdPath, mdOptions);
        }

        Console.WriteLine($"PDF successfully converted to Markdown: {outputMdPath}");
    }
}
