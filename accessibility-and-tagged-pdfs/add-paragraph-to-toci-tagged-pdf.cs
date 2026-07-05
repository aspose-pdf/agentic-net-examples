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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Set language and title for the tagged PDF (optional)
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root structure element (no cast needed)
            StructureElement root = tagged.RootElement;

            // Create a TOCI (Table of Contents Item) element
            TOCIElement toci = tagged.CreateTOCIElement();

            // Create a Paragraph element to be placed under the TOCI
            ParagraphElement paragraph = tagged.CreateParagraphElement();

            // Set the actual text for accessibility
            paragraph.ActualText = "This is the descriptive paragraph for the TOCI entry.";

            // Append the paragraph as a child of the TOCI element
            toci.AppendChild(paragraph); // bool parameter has default value

            // Append the TOCI element to the document root
            root.AppendChild(toci); // bool parameter has default value

            // Save the modified PDF (no PreSave required)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}