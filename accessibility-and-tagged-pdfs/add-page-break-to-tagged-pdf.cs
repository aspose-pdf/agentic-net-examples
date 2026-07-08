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

        // Load the PDF (no special load options needed for PDF)
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Set language and title (optional)
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root of the logical structure tree
            StructureElement root = tagged.RootElement;

            // ------------------------------------------------------------
            // Section 1
            // ------------------------------------------------------------
            ParagraphElement section1 = tagged.CreateParagraphElement();
            section1.SetText("Section 1: Introduction");
            // Append the first section to the root
            root.AppendChild(section1);

            // ------------------------------------------------------------
            // Page break element
            // ------------------------------------------------------------
            // Insert a page‑break by creating a DivElement with the "PageBreak" tag.
            DivElement pageBreak = tagged.CreateDivElement();
            pageBreak.SetTag("PageBreak");
            root.AppendChild(pageBreak);

            // ------------------------------------------------------------
            // Section 2
            // ------------------------------------------------------------
            ParagraphElement section2 = tagged.CreateParagraphElement();
            section2.SetText("Section 2: Details");
            root.AppendChild(section2);

            // Save the modified PDF (no PreSave call required)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF with page break saved to '{outputPath}'.");
    }
}
