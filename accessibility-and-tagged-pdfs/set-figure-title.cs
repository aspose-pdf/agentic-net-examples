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

        using (Document doc = new Document(inputPath))
        {
            ITaggedContent tagged = doc.TaggedContent;
            StructureElement root = tagged.RootElement;

            // Create a Figure element and set its Title (concise description)
            FigureElement figure = tagged.CreateFigureElement();
            figure.Title = "Sample Figure Title";

            // Attach the figure to the document's structure tree
            root.AppendChild(figure);

            doc.Save(outputPath);
        }

        Console.WriteLine($"Figure title set and saved to '{outputPath}'.");
    }
}