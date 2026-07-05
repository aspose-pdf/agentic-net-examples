using System;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using Aspose.Pdf.Text; // needed for TextFragment

class Program
{
    static void Main()
    {
        const string outputPath = "toc_document.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Access the tagged‑content API
            ITaggedContent tagged = doc.TaggedContent;

            // Set language and title for accessibility
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Document with Table of Contents");

            // Create the TOC structure element and attach it to the root
            TOCElement tocElement = tagged.CreateTOCElement();
            // The visible title of the TOC (shown in the PDF outline)
            tocElement.Title = "Table of Contents";
            StructureElement root = tagged.RootElement;
            root.AppendChild(tocElement);

            // Add a page that will contain the TOC
            Page tocPage = doc.Pages.Add();
            tocPage.TocInfo = new TocInfo
            {
                // TocInfo.Title expects a TextFragment, not a plain string
                Title = new TextFragment("Table of Contents"),
                IsShowPageNumbers = true,
                CopyToOutlines = false
            };

            // Create three headings on separate pages and corresponding TOC entries
            for (int i = 1; i <= 3; i++)
            {
                // Add a new page for the heading
                Page headingPage = doc.Pages.Add();

                // Add visible text for the heading (optional, but makes the PDF readable)
                TextFragment tf = new TextFragment($"Chapter {i}");
                tf.Position = new Position(50, 800);
                headingPage.Paragraphs.Add(tf);

                // Create a heading (H1) structure element
                HeaderElement heading = tagged.CreateHeaderElement(1);
                // Set the heading's title – this is the text used by assistive technologies
                heading.Title = $"Chapter {i}";
                // Append the heading to the document structure
                root.AppendChild(heading);

                // Create a TOC item (TOCI) element
                TOCIElement tocItem = tagged.CreateTOCIElement();
                // Set the visible text of the TOC entry
                tocItem.ActualText = $"Chapter {i}";
                // Append the TOC item to the TOC element
                tocElement.AppendChild(tocItem);
            }

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with TOC saved to '{outputPath}'.");
    }
}
