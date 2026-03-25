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

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Access tagged content interface
                ITaggedContent tagged = doc.TaggedContent;
                tagged.SetLanguage("en-US");
                tagged.SetTitle("Document with Page Break");

                // Root of the logical structure tree
                StructureElement root = tagged.RootElement;

                // First paragraph (section content)
                ParagraphElement para1 = tagged.CreateParagraphElement();
                para1.SetText("First section content.");
                root.AppendChild(para1);

                // Insert a page‑break element using a Div with the "PageBreak" tag
                DivElement pageBreak = tagged.CreateDivElement();
                pageBreak.SetTag("PageBreak");
                root.AppendChild(pageBreak);

                // Second paragraph (content after the page break)
                ParagraphElement para2 = tagged.CreateParagraphElement();
                para2.SetText("Second section content after page break.");
                root.AppendChild(para2);

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Tagged PDF with page break saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}