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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Set document language and title (optional)
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root structure element (no cast needed)
            StructureElement root = tagged.RootElement;

            // Create a figure element and set its title (concise description)
            FigureElement figure = tagged.CreateFigureElement();
            figure.Title = "Sample Figure: Quarterly Revenue Chart";

            // Append the figure to the document's structure tree
            root.AppendChild(figure);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with figure title to '{outputPath}'.");
    }
}