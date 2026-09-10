using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "Tagged_TOC.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // -------------------------------------------------
            // 1. Prepare tagged content (language, title, etc.)
            // -------------------------------------------------
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Document with Tagged Table of Contents");

            // -------------------------------------------------
            // 2. Add a content page with headings
            // -------------------------------------------------
            Page contentPage = doc.Pages.Add();
            // Visible title for the page
            TextFragment title = new TextFragment("Chapter 1: Introduction");
            title.Position = new Position(50, 750);
            title.TextState.FontSize = 20;
            contentPage.Paragraphs.Add(title);

            // Create a heading structure element (H1) and attach it to the page
            HeaderElement heading = tagged.CreateHeaderElement(1);
            heading.SetText("Chapter 1: Introduction");
            // Append the heading to the document root (visual association is implicit)
            tagged.RootElement.AppendChild(heading);

            // -------------------------------------------------
            // 3. Add a dedicated TOC page
            // -------------------------------------------------
            Page tocPage = doc.Pages.Add();

            // Configure TOC page info – this tells Aspose.Pdf that this page holds a TOC
            tocPage.TocInfo = new TocInfo
            {
                Title = new TextFragment("Table of Contents"),
                IsShowPageNumbers = true,
                CopyToOutlines = true
            };

            // Visible heading for the TOC page
            TextFragment tocTitle = new TextFragment("Table of Contents");
            tocTitle.Position = new Position(50, 750);
            tocTitle.TextState.FontSize = 24;
            tocPage.Paragraphs.Add(tocTitle);

            // -------------------------------------------------
            // 4. Build the logical TOC structure
            // -------------------------------------------------
            // Create the TOC element (root of the TOC hierarchy)
            TOCElement tocElement = tagged.CreateTOCElement();
            tocElement.Title = "Table of Contents"; // set label for the TOC element
            // Append TOC element to the document root
            tagged.RootElement.AppendChild(tocElement);

            // Create a TOCI (TOC item) for the heading we added earlier
            TOCIElement tocItem = tagged.CreateTOCIElement();
            // The label (e.g., chapter title)
            tocItem.ActualText = "Chapter 1: Introduction";
            // The reference (page number) – create a Reference element that points to the target page
            ReferenceElement reference = tagged.CreateReferenceElement();
            reference.ActualText = "1"; // page number where the heading resides
            // Append the reference to the TOCI item
            tocItem.AppendChild(reference);
            // Append the TOCI item to the TOC element
            tocElement.AppendChild(tocItem);

            // -------------------------------------------------
            // 5. Save the document
            // -------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF with TOC saved to '{outputPath}'.");
    }
}
