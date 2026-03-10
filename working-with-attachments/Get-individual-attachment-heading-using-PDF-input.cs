using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Enable auto‑tagging and configure heading detection
            AutoTaggingSettings.Default.EnableAutoTagging = true;
            // Use the correct enum: HeadingRecognitionStrategy is in Aspose.Pdf namespace, not LogicalStructure
            AutoTaggingSettings.Default.HeadingRecognitionStrategy = HeadingRecognitionStrategy.Heuristic;

            // Access the tagged content of the document
            ITaggedContent tagged = doc.TaggedContent;

            // Get the root structure element
            StructureElement root = tagged.RootElement;

            // Retrieve all heading elements (HeaderElement) recursively
            var headings = root.FindElements<HeaderElement>(true);

            Console.WriteLine($"Found {headings.Count} heading(s):");
            foreach (HeaderElement heading in headings)
            {
                // The visible text of a heading is stored in ActualText
                string text = heading.ActualText ?? string.Empty;
                Console.WriteLine($"- {text}");
            }
        }
    }
}