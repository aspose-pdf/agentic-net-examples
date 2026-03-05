using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_tagged.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block (ensures disposal)
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content interface
            ITaggedContent tagged = doc.TaggedContent;

            // Set language and title for the PDF (writes to the PDF/UA metadata)
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Example 1: Update all existing Figure elements (images, graphics) with new alt text
            var figures = tagged.RootElement.FindElements<FigureElement>(true);
            foreach (FigureElement fig in figures)
            {
                fig.AlternativeText = "Updated alternate text for figure";
            }

            // Example 2: Update all Paragraph elements – here we convert their text to uppercase
            var paragraphs = tagged.RootElement.FindElements<ParagraphElement>(true);
            foreach (ParagraphElement para in paragraphs)
            {
                // Retrieve current text, transform, and set it back
                string current = para.ActualText ?? string.Empty;
                para.SetText(current.ToUpperInvariant());
            }

            // Example 3: Ensure every structure element has its Language property set
            var allElements = tagged.RootElement.FindElements<StructureElement>(true);
            foreach (StructureElement el in allElements)
            {
                el.Language = "en-US";
            }

            // Persist changes to the tagged structure
            tagged.Save();

            // Save the modified PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}