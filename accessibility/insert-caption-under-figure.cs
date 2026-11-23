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
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content (document must be tagged)
            ITaggedContent taggedContent = doc.TaggedContent;

            // Root of the structure tree
            StructureElement root = taggedContent.RootElement;

            // Find the first Figure element in the structure (recursive search)
            var figures = root.FindElements<FigureElement>(true);
            if (figures.Count == 0)
            {
                Console.WriteLine("No Figure elements found in the document.");
            }
            else
            {
                // Use the first figure for demonstration
                FigureElement figure = figures[0];

                // Create a caption as a Paragraph element
                ParagraphElement caption = taggedContent.CreateParagraphElement();
                caption.SetText("Figure caption: This is an automatically added description.");

                // Insert the caption as a child of the figure
                // AppendChild with a single argument is the correct API
                figure.AppendChild(caption);

                // Generate a simple report to the console
                Console.WriteLine("Caption inserted under Figure element.");
                Console.WriteLine($"Figure AlternativeText: {figure.AlternativeText ?? "(none)"}");
                Console.WriteLine($"Caption text: {caption.ActualText}");
            }

            // Save the modified PDF (no PreSave required)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}