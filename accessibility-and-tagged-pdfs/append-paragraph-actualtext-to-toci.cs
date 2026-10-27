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
        const string outputPath = "output_tagged.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content (creates structure if not present)
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Create a TOCI (Table of Contents Item) placeholder.
            // Aspose.Pdf does not expose a dedicated TOCIElement class; use a DivElement as a container.
            DivElement toci = tagged.CreateDivElement();
            toci.AlternativeText = "Table of Contents Item";

            // Append the TOCI element to the root
            root.AppendChild(toci);

            // Create a paragraph element to be placed under the TOCI element
            ParagraphElement paragraph = tagged.CreateParagraphElement();
            paragraph.SetText("This paragraph is part of the TOC entry.");
            // Set the ActualText for accessibility (used by assistive technologies)
            paragraph.ActualText = "Accessible paragraph text for TOC entry";

            // Append the paragraph under the TOCI element
            toci.AppendChild(paragraph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}