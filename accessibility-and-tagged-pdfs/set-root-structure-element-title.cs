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
        const string rootTitle = "Accessible Document Title";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content (creates it if not present)
            ITaggedContent tagged = doc.TaggedContent;

            // Optional: set language and document-level title metadata
            tagged.SetLanguage("en-US");
            tagged.SetTitle(rootTitle);

            // Get the root structure element (no cast required)
            StructureElement root = tagged.RootElement;

            // Set the Title property on the root structure element
            root.Title = rootTitle;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with root title to '{outputPath}'.");
    }
}