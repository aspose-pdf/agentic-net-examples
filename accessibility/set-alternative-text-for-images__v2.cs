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
        const string outputPath = "output_tagged.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content interface
            ITaggedContent taggedContent = doc.TaggedContent;

            // Ensure the document has a tagged structure
            if (taggedContent == null)
            {
                Console.Error.WriteLine("Document does not contain tagged content.");
                return;
            }

            // Get the root element of the structure tree (no cast needed)
            StructureElement root = taggedContent.RootElement;

            // Find all FigureElement nodes recursively
            var figures = root.FindElements<FigureElement>(true);

            // Set a generic alternative text for each image figure
            foreach (FigureElement fig in figures)
            {
                fig.AlternativeText = "Image description";
            }

            // Save the modified PDF (no PreSave required)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}