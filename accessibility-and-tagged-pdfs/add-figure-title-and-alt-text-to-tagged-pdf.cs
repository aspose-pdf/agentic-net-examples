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

        // Load the PDF inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Optional: set document language and title metadata
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root of the structure tree (no cast needed)
            StructureElement root = tagged.RootElement;

            // Create a figure element to represent an image/graphic
            FigureElement figure = tagged.CreateFigureElement();

            // Set a concise description using the Title property
            figure.Title = "Figure 1: Quarterly Sales Overview";

            // Provide alternative text for screen readers (accessibility)
            figure.AlternativeText = "Bar chart showing sales for Q1–Q4";

            // Attach the figure to the document's structure tree
            root.AppendChild(figure);

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}