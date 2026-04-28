using System;
using System.IO;
using Aspose.Pdf;                                 // Core PDF API
using Aspose.Pdf.Tagged;                         // ITaggedContent interface
using Aspose.Pdf.LogicalStructure;               // Structure element types

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";      // Existing PDF (can be empty for new doc)
        const string outputPath = "tagged_with_pagebreak.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block to ensure proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged‑content API
            ITaggedContent taggedContent = doc.TaggedContent;

            // Optional: set document language and title for accessibility
            taggedContent.SetLanguage("en-US");
            taggedContent.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root element of the logical structure tree
            StructureElement root = taggedContent.RootElement;

            // ----- First section -------------------------------------------------
            // Create a section element and add a paragraph with some text
            SectElement firstSect = taggedContent.CreateSectElement();
            firstSect.SetTag("Section1");                     // optional custom tag
            root.AppendChild(firstSect);                      // attach to root

            ParagraphElement firstPara = taggedContent.CreateParagraphElement();
            firstPara.SetText("This is the content of the first section.");
            firstSect.AppendChild(firstPara);                 // attach paragraph to section

            // ----- Page break element -------------------------------------------
            // Represent a page break by inserting a Div element with the tag "PageBreak"
            DivElement pageBreak = taggedContent.CreateDivElement();
            pageBreak.SetTag("PageBreak");                    // tag recognized as a page break
            root.AppendChild(pageBreak);                      // insert into the logical tree

            // ----- Second section ------------------------------------------------
            SectElement secondSect = taggedContent.CreateSectElement();
            secondSect.SetTag("Section2");
            root.AppendChild(secondSect);

            ParagraphElement secondPara = taggedContent.CreateParagraphElement();
            secondPara.SetText("This is the content of the second section, which starts on a new page.");
            secondSect.AppendChild(secondPara);

            // Save the modified PDF. No PreSave() call is required.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF with page break saved to '{outputPath}'.");
    }
}