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

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the default auto‑tagging heading levels configuration
            HeadingLevels headingLevels = AutoTaggingSettings.Default.HeadingLevels;
            Console.WriteLine("AutoTagging heading levels configuration:");
            Console.WriteLine(headingLevels?.GetType().FullName ?? "null");

            // Check if document is tagged using TaggedContent property existence
            // In Aspose.Pdf, IsTagged is not a property of Document; instead, check TaggedContent != null
            if (doc.TaggedContent != null)
            {
                ITaggedContent tagged = doc.TaggedContent;
                StructureElement root = tagged.RootElement;

                // Find all header elements (H1‑H6) in the structure tree
                var headers = root.FindElements<HeaderElement>(true);
                foreach (HeaderElement header in headers)
                {
                    // HeaderElement may have a Level property; if not, just output the text
                    string levelInfo = header.GetType().Name; // fallback if Level not available
                    Console.WriteLine($"{levelInfo}: {header.ActualText}");
                }
            }
            else
            {
                Console.WriteLine("Document is not tagged; heading structure is unavailable.");
            }
        }
    }
}
