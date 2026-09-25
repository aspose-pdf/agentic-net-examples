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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content interface
            ITaggedContent tagged = doc.TaggedContent;

            // Optional: set language and title for the tagged PDF
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Insert a page‑break element by creating a DivElement with the "PageBreak" tag
            DivElement pageBreak = tagged.CreateDivElement();
            pageBreak.SetTag("PageBreak");
            root.AppendChild(pageBreak);

            // Example: add a paragraph after the page break
            ParagraphElement para = tagged.CreateParagraphElement();
            para.SetText("Content that follows the page break.");
            root.AppendChild(para);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF with page break saved to '{outputPath}'.");
    }
}
