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

        using (Document doc = new Document(inputPath))
        {
            // Access tagged content and set basic properties
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root of the logical structure
            StructureElement root = tagged.RootElement;

            // Create a new section element – acts as a logical page break
            SectElement section = tagged.CreateSectElement();
            section.AlternativeText = "Section break (page break)";
            root.AppendChild(section);

            // Add a paragraph inside the new section
            ParagraphElement paragraph = tagged.CreateParagraphElement();
            paragraph.SetText("This paragraph starts a new section, effectively separating content with a page‑break element in the logical tree.");
            section.AppendChild(paragraph);

            // Save the modified PDF (PDF output, no extra SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF with section break saved to '{outputPath}'.");
    }
}