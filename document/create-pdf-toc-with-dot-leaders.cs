using System;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "toc_with_dots.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Access tagged‑content API
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Sample Document with TOC");

            // Root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // -----------------------------------------------------------------
            // Create the Table of Contents element and attach it to the root
            // -----------------------------------------------------------------
            TOCElement toc = tagged.CreateTOCElement();
            toc.AlternativeText = "Table of Contents";
            root.AppendChild(toc);

            // -----------------------------------------------------------------
            // Define a LevelFormat that uses a dot leader for the page numbers
            // -----------------------------------------------------------------
            LevelFormat levelFormat = new LevelFormat
            {
                // Use dot leader for the TOC entries
                LineDash = TabLeaderType.Dot,
                // TextState holds font and size (no TabStops here)
                TextState = new TextState
                {
                    Font = FontRepository.FindFont("Helvetica"),
                    FontSize = 12
                }
            };

            // -----------------------------------------------------------------
            // Configure TOCInfo on each page that will contain the TOC
            // -----------------------------------------------------------------
            Page tocPage = doc.Pages.Add();
            tocPage.TocInfo = new TocInfo
            {
                IsShowPageNumbers = true,          // show page numbers in the TOC
                CopyToOutlines = true,             // also create PDF outlines
                FormatArray = new LevelFormat[] { levelFormat }
            };

            // -----------------------------------------------------------------
            // Add sample headings (these will be listed in the TOC)
            // -----------------------------------------------------------------
            // Chapter 1
            HeaderElement chapter1 = tagged.CreateHeaderElement(1);
            chapter1.SetText("Chapter 1: Introduction");
            root.AppendChild(chapter1);

            ParagraphElement para1 = tagged.CreateParagraphElement();
            para1.SetText("This is the introduction text.");
            root.AppendChild(para1);

            // Section 1.1
            HeaderElement section11 = tagged.CreateHeaderElement(2);
            section11.SetText("Section 1.1: Overview");
            root.AppendChild(section11);

            ParagraphElement para2 = tagged.CreateParagraphElement();
            para2.SetText("Details about the overview.");
            root.AppendChild(para2);

            // Add a second page with more headings
            Page page2 = doc.Pages.Add();
            page2.TocInfo = new TocInfo
            {
                IsShowPageNumbers = true,
                CopyToOutlines = true,
                FormatArray = new LevelFormat[] { levelFormat }
            };

            // Chapter 2
            HeaderElement chapter2 = tagged.CreateHeaderElement(1);
            chapter2.SetText("Chapter 2: Methods");
            root.AppendChild(chapter2);

            ParagraphElement para3 = tagged.CreateParagraphElement();
            para3.SetText("Methodology description.");
            root.AppendChild(para3);

            // -----------------------------------------------------------------
            // Save the PDF (Document is disposed automatically by using)
            // -----------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with TOC saved to '{outputPath}'.");
    }
}
