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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content interface
            ITaggedContent taggedContent = doc.TaggedContent;

            // Get the root element of the logical structure tree
            var root = taggedContent.RootElement;

            // Create a page‑break element. In the current Aspose.Pdf API a page break is
            // represented by a <Div> element whose tag is set to "PageBreak".
            DivElement pageBreak = taggedContent.CreateDivElement();
            pageBreak.SetTag("PageBreak");
            root.AppendChild(pageBreak);

            // Optionally add a paragraph after the page break to illustrate the new section.
            ParagraphElement para = taggedContent.CreateParagraphElement();
            para.SetText("Content after the page break.");
            root.AppendChild(para);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF with page break saved to '{outputPath}'.");
    }
}
