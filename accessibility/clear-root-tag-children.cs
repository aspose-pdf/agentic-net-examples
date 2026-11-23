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

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content of the document
            ITaggedContent tagged = doc.TaggedContent;

            // Obtain the root structure element (no cast required)
            StructureElement root = tagged.RootElement;

            // Clear all child elements from the root tag
            root.ClearChilds();

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with cleared structure to '{outputPath}'.");
    }
}