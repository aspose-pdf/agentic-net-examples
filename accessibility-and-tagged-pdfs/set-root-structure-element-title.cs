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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            ITaggedContent taggedContent = doc.TaggedContent;

            // Get the root structure element (no cast required)
            StructureElement root = taggedContent.RootElement;

            // Set the title on the root structure element
            root.Title = rootTitle;

            // Optionally, also set the document title metadata
            doc.SetTitle(rootTitle);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved with root title to '{outputPath}'.");
    }
}