using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

namespace AsposePdfExamples
{
    class CreateTaggedToc
    {
        static void Main(string[] args)
        {
            // Step 1: Create a sample PDF (self‑contained) and save it as input.pdf
            using (Document sampleDoc = new Document())
            {
                // Add an empty page – required because a PDF must contain at least one page
                sampleDoc.Pages.Add();
                sampleDoc.Save("input.pdf");
            }

            // Step 2: Open the sample PDF for further processing
            using (Document doc = new Document("input.pdf"))
            {
                // Add two content pages that will contain headings
                doc.Pages.Add(); // Page 2
                doc.Pages.Add(); // Page 3

                // Get the tagged‑content helper
                ITaggedContent tagged = doc.TaggedContent;

                // Create a root structure element (the document already has one)
                StructureElement root = tagged.RootElement;

                // ------------------------------------------------------------
                // Add visible headings on the content pages and tag them
                // ------------------------------------------------------------
                // Page 2 – Heading "Chapter 1"
                Page page2 = doc.Pages[2];
                TextFragment tf1 = new TextFragment("Chapter 1");
                tf1.TextState.FontSize = 24;
                tf1.TextState.FontStyle = FontStyles.Bold;
                page2.Paragraphs.Add(tf1);
                HeaderElement header1 = tagged.CreateHeaderElement(1);
                header1.SetText("Chapter 1");
                root.AppendChild(header1);

                // Page 3 – Heading "Chapter 2"
                Page page3 = doc.Pages[3];
                TextFragment tf2 = new TextFragment("Chapter 2");
                tf2.TextState.FontSize = 24;
                tf2.TextState.FontStyle = FontStyles.Bold;
                page3.Paragraphs.Add(tf2);
                HeaderElement header2 = tagged.CreateHeaderElement(1);
                header2.SetText("Chapter 2");
                root.AppendChild(header2);

                // ------------------------------------------------------------
                // Insert a Table of Contents page at the beginning (page index 1)
                // ------------------------------------------------------------
                doc.Pages.Insert(1);
                Page tocPage = doc.Pages[1];

                // Configure TOC visual information
                TocInfo tocInfo = new TocInfo();
                tocInfo.Title = new TextFragment("Table of Contents");
                tocInfo.IsShowPageNumbers = true;
                tocPage.TocInfo = tocInfo;

                // Create the logical TOC structure element
                TOCElement tocElement = tagged.CreateTOCElement();
                tocElement.Title = "Table of Contents";
                root.AppendChild(tocElement);

                // Create TOC items (TOCI) for each heading
                TOCIElement tocItem1 = tagged.CreateTOCIElement();
                tocItem1.ActualText = "Chapter 1";
                tocElement.AppendChild(tocItem1);

                TOCIElement tocItem2 = tagged.CreateTOCIElement();
                tocItem2.ActualText = "Chapter 2";
                tocElement.AppendChild(tocItem2);

                // Save the final PDF
                doc.Save("output.pdf");
            }
        }
    }
}
