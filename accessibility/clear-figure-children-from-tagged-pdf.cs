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
        const string outputPath = "output_cleared.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Check whether the document contains a tagged structure
            if (doc.TaggedContent == null)
            {
                Console.WriteLine("Document is not tagged. No logical structure to modify.");
                doc.Save(outputPath);
                return;
            }

            // Access the tagged content interface
            ITaggedContent taggedContent = doc.TaggedContent;

            // Get the root of the logical structure tree
            StructureElement root = taggedContent.RootElement;

            // Find all Figure elements in the structure (recursive search)
            var figures = root.FindElements<FigureElement>(true);

            // Clear child elements from each Figure tag using ClearChilds()
            foreach (FigureElement figure in figures)
            {
                figure.ClearChilds();
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
