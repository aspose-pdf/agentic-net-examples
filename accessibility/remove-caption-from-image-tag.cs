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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure the PDF is tagged (check TaggedContent instead of non‑existent IsTagged)
            if (doc.TaggedContent == null)
            {
                Console.WriteLine("The document is not a tagged PDF.");
                return;
            }

            // Access the tagged content interface
            ITaggedContent tagged = doc.TaggedContent;

            // Get the root structure element
            StructureElement root = tagged.RootElement;

            // Find the first FigureElement (image tag) in the structure tree
            var figures = root.FindElements<FigureElement>(true);
            if (figures != null && figures.Count > 0)
            {
                FigureElement figure = figures[0];

                // If the figure has child elements (e.g., a caption), remove the first child
                if (figure.ChildElements != null && figure.ChildElements.Count > 0)
                {
                    // RemoveChild expects an index; remove the caption at index 0
                    figure.RemoveChild(0);
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}
