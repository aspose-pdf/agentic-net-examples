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
        const string outputPath = "accessible_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content interface
            ITaggedContent tagged = doc.TaggedContent;

            // Set document language and title (write‑only setters)
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root structure element (no cast required)
            StructureElement root = tagged.RootElement;

            // ----- Add a paragraph element -----
            ParagraphElement paragraph = tagged.CreateParagraphElement();
            paragraph.SetText("This PDF has been made accessible by adding a paragraph element.");
            root.AppendChild(paragraph); // AppendChild with a single argument

            // ----- Add a figure element (e.g., for an image) -----
            FigureElement figure = tagged.CreateFigureElement();
            figure.AlternativeText = "Sample figure showing a chart of quarterly revenue.";
            root.AppendChild(figure);

            // Save the modified PDF (lifecycle rule: use Save inside the using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Accessible PDF saved to '{outputPath}'.");
    }
}