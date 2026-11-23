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
        const string outputPath = "output_cleared.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content (logical structure)
            ITaggedContent taggedContent = doc.TaggedContent;

            // Get the root structure element
            StructureElement root = taggedContent.RootElement;

            // Remove all child elements from the root (or any specific tag)
            root.ClearChilds();

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with cleared structure: {outputPath}");
    }
}