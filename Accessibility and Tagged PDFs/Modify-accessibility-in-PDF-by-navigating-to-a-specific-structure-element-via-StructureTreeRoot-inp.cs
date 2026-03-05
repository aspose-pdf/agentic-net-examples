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

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content interface
            ITaggedContent tagged = doc.TaggedContent;

            // Optionally set document language and title via tagged content
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Navigate to the StructTreeRoot element
            StructTreeRootElement structRoot = tagged.StructTreeRootElement;

            // Example: locate the first FigureElement in the structure tree
            var figures = structRoot.FindElements<FigureElement>(true);
            if (figures.Count > 0)
            {
                FigureElement figure = figures[0];
                // Modify the alternative text of the figure
                figure.AlternativeText = "Updated description for the figure.";
            }

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}