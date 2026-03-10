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
        const string outputPath = "output_with_headings.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            ITaggedContent taggedContent = doc.TaggedContent;

            // Optionally set language and title for the tagged PDF
            taggedContent.SetLanguage("en-US");
            taggedContent.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root structure element (no cast required)
            StructureElement root = taggedContent.RootElement;

            // Create a level‑1 heading and add it to the structure tree
            HeaderElement heading1 = taggedContent.CreateHeaderElement(1);
            heading1.SetText("Document Title");
            root.AppendChild(heading1);

            // Create a level‑2 heading and add it to the structure tree
            HeaderElement heading2 = taggedContent.CreateHeaderElement(2);
            heading2.SetText("Section 1: Introduction");
            root.AppendChild(heading2);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with headings saved to '{outputPath}'.");
    }
}