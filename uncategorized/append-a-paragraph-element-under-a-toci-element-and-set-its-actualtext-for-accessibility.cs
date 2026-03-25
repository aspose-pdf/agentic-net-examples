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

        using (Document doc = new Document(inputPath))
        {
            ITaggedContent tagged = doc.TaggedContent;
            StructureElement root = tagged.RootElement;

            // Create a TOCI element and attach it to the root
            TOCIElement toci = tagged.CreateTOCIElement();
            root.AppendChild(toci);

            // Create a paragraph element, set its ActualText, and attach it under the TOCI element
            ParagraphElement paragraph = tagged.CreateParagraphElement();
            paragraph.ActualText = "This is the actual text for the paragraph.";
            toci.AppendChild(paragraph);

            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}