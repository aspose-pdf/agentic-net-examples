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

        // Open the PDF with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Get the root structure element (no cast needed)
            StructureElement root = tagged.RootElement;

            // Create a TOCI (Table of Contents Item) element
            TOCIElement toci = tagged.CreateTOCIElement();

            // Create a paragraph element to be placed under the TOCI
            ParagraphElement paragraph = tagged.CreateParagraphElement();

            // Set the actual text for accessibility (screen readers will read this)
            paragraph.ActualText = "Descriptive paragraph for the TOC entry.";

            // Append the paragraph to the TOCI element
            toci.AppendChild(paragraph); // bool parameter omitted (defaults to true)

            // Append the TOCI element to the document root
            root.AppendChild(toci); // bool parameter omitted

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}