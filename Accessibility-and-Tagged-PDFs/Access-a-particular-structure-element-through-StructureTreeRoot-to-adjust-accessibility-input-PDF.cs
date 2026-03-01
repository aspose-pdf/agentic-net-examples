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
        const string outputPath = "output_accessible.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content interface
            ITaggedContent tagged = doc.TaggedContent;

            // Retrieve the StructTreeRootElement of the logical structure
            StructTreeRootElement structRoot = tagged.StructTreeRootElement;

            // Find all FigureElement objects in the structure tree (recursive search)
            var figures = structRoot.FindElements<FigureElement>(true);
            if (figures.Count > 0)
            {
                // Adjust accessibility for the first figure element found
                FigureElement fig = figures[0];
                fig.AlternativeText = "Descriptive text for the figure.";
                fig.Language = "en-US";
            }
            else
            {
                Console.WriteLine("No FigureElement found in the structure tree.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}