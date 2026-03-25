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
            // Access tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Create a Figure element and set its Title (concise description)
            FigureElement figure = tagged.CreateFigureElement();
            figure.Title = "Sample Figure Title"; // concise description

            // Attach the figure to the structure tree
            root.AppendChild(figure);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Figure title set and saved to '{outputPath}'.");
    }
}