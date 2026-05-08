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
        const string outputPath = "output_tagged.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and work with its tagged content
        using (Document doc = new Document(inputPath))
        {
            ITaggedContent tagged = doc.TaggedContent;

            // Optional: set language and title for the tagged PDF
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root of the structure tree (no cast needed)
            StructureElement root = tagged.RootElement;

            // Create a TOCI element (Table of Contents Item) and attach it to the root
            TOCIElement toci = tagged.CreateTOCIElement();
            root.AppendChild(toci); // AppendChild with one argument

            // Create a paragraph element, set its ActualText, and attach it under the TOCI element
            ParagraphElement para = tagged.CreateParagraphElement();
            para.ActualText = "This paragraph provides a description for the TOCI entry.";
            toci.AppendChild(para); // AppendChild with one argument

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}