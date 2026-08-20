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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Get the root of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Create a TOCI (Table of Contents Item) element
            TOCIElement toci = tagged.CreateTOCIElement();

            // Append the TOCI element to the root (single-argument AppendChild uses default bool)
            root.AppendChild(toci);

            // Create a paragraph element that will be a child of the TOCI element
            ParagraphElement paragraph = tagged.CreateParagraphElement();

            // Set the ActualText property for accessibility purposes
            paragraph.ActualText = "This paragraph is part of a TOCI entry for screen readers.";

            // Append the paragraph under the TOCI element
            toci.AppendChild(paragraph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}