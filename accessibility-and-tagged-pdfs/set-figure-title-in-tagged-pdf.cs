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
        const string figureTitle = "Concise description of the figure";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content (if any)
            ITaggedContent taggedContent = doc.TaggedContent;

            // Find the first FigureElement in the structure tree
            FigureElement figure = null;
            var figures = taggedContent.RootElement.FindElements<FigureElement>(true);
            if (figures.Count > 0)
                figure = figures[0];

            // If no figure exists, create one and attach it to the root
            if (figure == null)
            {
                figure = taggedContent.CreateFigureElement();
                // Attach the new figure to the root element
                taggedContent.RootElement.AppendChild(figure);
            }

            // Set the concise description using the AlternativeText property
            figure.AlternativeText = figureTitle;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Figure title set and saved to '{outputPath}'.");
    }
}