using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string outputPath = "toc_dot_leader.pdf";

        using (Document doc = new Document())
        {
            // Add sample pages with headings (these will become TOC entries)
            Page page1 = doc.Pages.Add();
            TextFragment heading1 = new TextFragment("Chapter 1: Introduction");
            heading1.TextState.Font = FontRepository.FindFont("Helvetica");
            heading1.TextState.FontSize = 24;
            page1.Paragraphs.Add(heading1);

            Page page2 = doc.Pages.Add();
            TextFragment heading2 = new TextFragment("Chapter 2: Details");
            heading2.TextState.Font = FontRepository.FindFont("Helvetica");
            heading2.TextState.FontSize = 24;
            page2.Paragraphs.Add(heading2);

            // Access tagged content for accessibility and TOC creation
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Sample Document with TOC");

            // Create a TOC element and attach it to the root structure element
            TOCElement tocElement = tagged.CreateTOCElement();
            StructureElement root = tagged.RootElement;
            root.AppendChild(tocElement);

            // Configure TOC formatting to use dot leaders
            LevelFormat levelFormat = new LevelFormat
            {
                // Use dot leader between entry text and page number
                LineDash = TabLeaderType.Dot
            };

            // Build TocInfo – Title must be a TextFragment, not a string
            TocInfo tocInfo = new TocInfo
            {
                Title = new TextFragment("Table of Contents"),
                IsShowPageNumbers = true,
                CopyToOutlines = true,
                FormatArray = new LevelFormat[] { levelFormat },
                FormatArrayLength = 1
            };

            // The first page will host the generated TOC
            page1.TocInfo = tocInfo;

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with TOC saved to '{outputPath}'.");
    }
}
