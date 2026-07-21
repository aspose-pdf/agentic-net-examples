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
        const string outputPath = "TaggedDocumentWithTOC.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Set document language and title (write‑only setters)
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Sample Document with TOC");

            // Root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // -----------------------------------------------------------------
            // Add some content pages with headings (to be referenced in TOC)
            // -----------------------------------------------------------------
            // Page 1 – first heading
            Page page1 = doc.Pages.Add();
            HeaderElement h1 = tagged.CreateHeaderElement(1);
            h1.SetText("Chapter 1 – Introduction");
            root.AppendChild(h1);               // attach heading to the structure
            // (optional visual content)
            TextFragment tf1 = new TextFragment("Content of Chapter 1...");
            tf1.Position = new Position(50, 750);
            page1.Paragraphs.Add(tf1);

            // Page 2 – second heading
            Page page2 = doc.Pages.Add();
            HeaderElement h2 = tagged.CreateHeaderElement(2);
            h2.SetText("Chapter 2 – Details");
            root.AppendChild(h2);
            TextFragment tf2 = new TextFragment("Content of Chapter 2...");
            tf2.Position = new Position(50, 750);
            page2.Paragraphs.Add(tf2);

            // -----------------------------------------------------------------
            // Create a dedicated TOC page
            // -----------------------------------------------------------------
            Page tocPage = doc.Pages.Add();

            // Configure TOC visual information (title, page numbers, etc.)
            TocInfo tocInfo = new TocInfo
            {
                Title = new TextFragment("Table of Contents"), // Title expects a TextFragment
                IsShowPageNumbers = true,
                CopyToOutlines = true
            };
            tocPage.TocInfo = tocInfo;

            // Create the TOC structure element and attach it to the root
            TOCElement tocElement = tagged.CreateTOCElement();
            tocElement.Title = "Table of Contents"; // optional visible title for the TOC element
            root.AppendChild(tocElement);

            // -----------------------------------------------------------------
            // Add TOC entries (TOCI elements) for each heading
            // -----------------------------------------------------------------
            // Entry for Chapter 1
            TOCIElement entry1 = tagged.CreateTOCIElement();
            entry1.ActualText = "Chapter 1 – Introduction"; // use ActualText instead of SetText
            tocElement.AppendChild(entry1);

            // Entry for Chapter 2
            TOCIElement entry2 = tagged.CreateTOCIElement();
            entry2.ActualText = "Chapter 2 – Details";
            tocElement.AppendChild(entry2);

            // -----------------------------------------------------------------
            // Save the tagged PDF
            // -----------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF with TOC saved to '{outputPath}'.");
    }
}
