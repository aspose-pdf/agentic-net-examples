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
        const string outputPath = "output_with_figure_title.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF and work with its tagged content
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Get the root element of the structure tree (no cast needed)
            StructureElement root = tagged.RootElement;

            // Create a new figure element
            FigureElement figure = tagged.CreateFigureElement();

            // Set a concise description using the Title property
            figure.Title = "Figure 1: Sales growth chart";

            // Optionally set alternative text for accessibility
            figure.AlternativeText = "Bar chart showing quarterly sales growth";

            // Append the figure to the document's structure tree
            root.AppendChild(figure);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with figure title to '{outputPath}'.");
    }
}