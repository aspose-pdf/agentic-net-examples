using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API, includes AutoTaggingSettings, HeadingRecognitionStrategy, MarkdownSaveOptions
using Aspose.Pdf.Text;          // Required for text‑related options (MarkdownSaveOptions resides here in some versions)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // Path to the source PDF
        const string outputPath = "output.md";   // Path where the markdown will be saved

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Enable auto‑tagging so that Aspose.Pdf can recognise headings automatically
            AutoTaggingSettings.Default.EnableAutoTagging = true;

            // Use the automatic heading recognition strategy (can be adjusted if needed)
            AutoTaggingSettings.Default.HeadingRecognitionStrategy = HeadingRecognitionStrategy.Auto;

            // Prepare markdown save options – ensure heading detection is active
            MarkdownSaveOptions mdOptions = new MarkdownSaveOptions
            {
                // The same strategy can be set on the save options as a safety net
                HeadingRecognitionStrategy = HeadingRecognitionStrategy.Auto
                // Additional options (e.g., HeadingLevels) can be set here if required
            };

            // Save the document as markdown; headings will be represented using markdown syntax (e.g., #, ##)
            doc.Save(outputPath, mdOptions);
        }

        Console.WriteLine($"Markdown file with headings saved to '{outputPath}'.");
    }
}