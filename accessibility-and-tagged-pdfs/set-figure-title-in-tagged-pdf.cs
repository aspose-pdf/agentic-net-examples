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
        const string outputPath = "output.pdf";

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

            // Create a figure element in the logical structure
            FigureElement figure = tagged.CreateFigureElement();

            // Set a concise description using the Title property
            figure.Title = "Sample Figure: Revenue Chart";

            // Optional: provide alternative text for accessibility
            figure.AlternativeText = "Bar chart showing quarterly revenue";

            // Append the figure element to the root of the structure tree
            StructureElement root = tagged.RootElement;
            root.AppendChild(figure);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with figure title to '{outputPath}'.");
    }
}