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
            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Create a TOCI (Table of Contents Item) element
            TOCIElement toci = tagged.CreateTOCIElement();
            toci.Title = "Table of Contents Item";

            // Append the TOCI element to the root of the structure tree
            StructureElement root = tagged.RootElement;
            root.AppendChild(toci); // bool parameter omitted (default)

            // Create a paragraph element
            ParagraphElement paragraph = tagged.CreateParagraphElement();

            // Set the ActualText for accessibility
            paragraph.ActualText = "This is a descriptive paragraph under TOCI.";

            // Append the paragraph as a child of the TOCI element
            toci.AppendChild(paragraph); // bool parameter omitted (default)

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}