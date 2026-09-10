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
        const string rootTitle  = "Accessible Document Title";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content (no casting needed)
            ITaggedContent tagged = doc.TaggedContent;

            // Get the root structure element and set its Title property
            StructureElement root = tagged.RootElement;
            root.Title = rootTitle;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with root title to '{outputPath}'.");
    }
}