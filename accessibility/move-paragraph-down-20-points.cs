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
        const string outputPath = "output_moved.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content (logical structure) of the document
            ITaggedContent taggedContent = doc.TaggedContent;

            // Find the first ParagraphElement in the structure tree (recursive search)
            var paragraphs = taggedContent.RootElement.FindElements<ParagraphElement>(true);
            if (paragraphs.Count > 0)
            {
                ParagraphElement paragraph = paragraphs[0];

                // Move the paragraph 20 points down using AdjustPosition with a top margin
                paragraph.AdjustPosition(new PositionSettings
                {
                    Margin = new MarginInfo { Top = 20 }
                });
            }
            else
            {
                Console.WriteLine("No ParagraphElement found in the document.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with moved paragraph: {outputPath}");
    }
}
