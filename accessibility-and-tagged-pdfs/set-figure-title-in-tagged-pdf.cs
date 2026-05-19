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

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Optional: set document language and title for accessibility
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Accessible PDF with Figure Title");

            // Get the root structure element of the tagged PDF
            StructureElement root = tagged.RootElement;

            // Create a figure element and assign a concise title
            FigureElement figure = tagged.CreateFigureElement();
            figure.Title = "Figure 1: Sales Overview";

            // Optional: provide alternative text for the figure
            figure.AlternativeText = "Bar chart showing quarterly sales.";

            // Append the figure element to the document's structure tree
            root.AppendChild(figure);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}