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
        const string altText = "Descriptive alternative text for image";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify the document is tagged; otherwise there is no structure tree to traverse
            if (doc.TaggedContent == null)
            {
                Console.WriteLine("Document is not tagged. No structure elements to process.");
                doc.Save(outputPath);
                return;
            }

            // Access the tagged content interface
            ITaggedContent taggedContent = doc.TaggedContent;

            // Get the root of the logical structure tree (no cast required)
            StructureElement root = taggedContent.RootElement;

            // Recursively find all FigureElement objects (image elements) in the structure
            var figureElements = root.FindElements<FigureElement>(true);

            // Set the AlternativeText for each found image element
            foreach (FigureElement fig in figureElements)
            {
                fig.AlternativeText = altText;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
