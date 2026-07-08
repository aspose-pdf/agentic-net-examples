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
        const string outputPath = "output_with_title.pdf";
        const string docTitle   = "Accessible PDF Document";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content (creates it if missing)
            ITaggedContent tagged = doc.TaggedContent;

            // Set document title at the root structure element
            StructureElement root = tagged.RootElement; // no cast needed
            root.Title = docTitle; // set title metadata for the root element

            // Optionally set the PDF Info title as well (standard metadata)
            doc.Info.Title = docTitle;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved with title to '{outputPath}'.");
    }
}