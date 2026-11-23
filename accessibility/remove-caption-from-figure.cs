using System;
using System.IO;
using System.Linq;
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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content of the document
            ITaggedContent taggedContent = doc.TaggedContent;

            // Get the root of the logical structure tree
            StructureElement root = taggedContent.RootElement;

            // Find the first Figure element in the structure (recursive search)
            FigureElement figure = root.FindElements<FigureElement>(true).FirstOrDefault();

            if (figure != null)
            {
                // Locate a Caption child within the Figure (if any)
                CaptionElement caption = figure.ChildElements
                                               .OfType<CaptionElement>()
                                               .FirstOrDefault();

                if (caption != null)
                {
                    // Remove the caption from the figure. RemoveChild expects an index, so use the element's Remove() method.
                    caption.Remove();
                    Console.WriteLine("Caption removed from figure.");
                }
                else
                {
                    Console.WriteLine("No caption child found in the figure.");
                }
            }
            else
            {
                Console.WriteLine("No figure element found in the document.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
