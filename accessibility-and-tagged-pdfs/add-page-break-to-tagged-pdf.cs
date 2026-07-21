using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;               // ITaggedContent, DivElement, ParagraphElement
using Aspose.Pdf.LogicalStructure;    // StructureElement, ParagraphElement

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "tagged_with_pagebreak.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Optional: set language and title for the document
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root of the logical structure tree
            StructureElement root = tagged.RootElement;

            // ------------------------------------------------------------
            // Section 1 – add some content (example paragraph)
            // ------------------------------------------------------------
            ParagraphElement section1 = tagged.CreateParagraphElement();
            section1.SetText("First section content.");
            root.AppendChild(section1);

            // ------------------------------------------------------------
            // Page break element – forces the next element to start on a new page
            // ------------------------------------------------------------
            // In Aspose.PDF the page‑break is represented by a DivElement with the tag "PageBreak".
            DivElement pageBreak = tagged.CreateDivElement();
            pageBreak.SetTag("PageBreak");
            root.AppendChild(pageBreak);

            // ------------------------------------------------------------
            // Section 2 – content that will appear on the next page
            // ------------------------------------------------------------
            ParagraphElement section2 = tagged.CreateParagraphElement();
            section2.SetText("Second section content starts on a new page.");
            root.AppendChild(section2);

            // Save the modified PDF (no PreSave call required)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF with page break saved to '{outputPath}'.");
    }
}
