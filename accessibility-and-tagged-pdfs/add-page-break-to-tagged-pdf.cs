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
        const string outputPath = "tagged_with_pagebreak.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged‑content API
            ITaggedContent tagged = doc.TaggedContent;

            // Optional: set language and title for the tagged PDF
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root of the logical structure tree
            StructureElement root = tagged.RootElement;

            // First section (example content)
            ParagraphElement firstPara = tagged.CreateParagraphElement();
            firstPara.SetText("First section content.");
            root.AppendChild(firstPara);

            // Page‑break element: use a DivElement with the "PageBreak" tag
            DivElement pageBreak = tagged.CreateDivElement();
            pageBreak.SetTag("PageBreak");
            root.AppendChild(pageBreak);

            // Second section (example content)
            ParagraphElement secondPara = tagged.CreateParagraphElement();
            secondPara.SetText("Second section content starts on a new page.");
            root.AppendChild(secondPara);

            // Save the modified PDF (no PreSave call required)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF with page break saved to '{outputPath}'.");
    }
}