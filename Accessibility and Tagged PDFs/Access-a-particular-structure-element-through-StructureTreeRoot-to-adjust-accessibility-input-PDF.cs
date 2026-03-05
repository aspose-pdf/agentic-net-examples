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
        const string outputPath = "output_accessible.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content interface
            ITaggedContent taggedContent = doc.TaggedContent;

            // Get the StructTreeRootElement (the root of the logical structure tree)
            StructTreeRootElement structRoot = taggedContent.StructTreeRootElement;

            // Example: locate all FigureElements (e.g., images) in the structure tree
            var figures = structRoot.FindElements<FigureElement>(true);

            foreach (FigureElement figure in figures)
            {
                // Adjust accessibility properties, e.g., set alternative text
                figure.AlternativeText = "Descriptive text for the figure";
                // Optionally set language for this element
                figure.Language = "en-US";
            }

            // Save the modified PDF (no PreSave call needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Accessible PDF saved to '{outputPath}'.");
    }
}