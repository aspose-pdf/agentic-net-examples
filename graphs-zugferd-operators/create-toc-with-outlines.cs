using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace AsposePdfExamples
{
    class CreateTocClickable
    {
        static void Main(string[] args)
        {
            // ------------------------------------------------------------
            // 1. Create a sample PDF with three pages, each containing a heading.
            // ------------------------------------------------------------
            using (Document doc = new Document())
            {
                // Page 1
                Page page1 = doc.Pages.Add();
                Heading heading1 = new Heading(1);
                heading1.Segments.Add(new TextSegment("Chapter 1"));
                heading1.IsAutoSequence = true;
                heading1.IsInList = true;
                page1.Paragraphs.Add(heading1);
                TextFragment tf1 = new TextFragment("Content of chapter 1.");
                page1.Paragraphs.Add(tf1);

                // Page 2
                Page page2 = doc.Pages.Add();
                Heading heading2 = new Heading(1);
                heading2.Segments.Add(new TextSegment("Chapter 2"));
                heading2.IsAutoSequence = true;
                heading2.IsInList = true;
                page2.Paragraphs.Add(heading2);
                TextFragment tf2 = new TextFragment("Content of chapter 2.");
                page2.Paragraphs.Add(tf2);

                // Page 3
                Page page3 = doc.Pages.Add();
                Heading heading3 = new Heading(1);
                heading3.Segments.Add(new TextSegment("Chapter 3"));
                heading3.IsAutoSequence = true;
                heading3.IsInList = true;
                page3.Paragraphs.Add(heading3);
                TextFragment tf3 = new TextFragment("Content of chapter 3.");
                page3.Paragraphs.Add(tf3);

                // Save the sample PDF.
                doc.Save("input.pdf");
            }

            // ------------------------------------------------------------
            // 2. Reopen the PDF and insert a Table of Contents page.
            // ------------------------------------------------------------
            using (Document doc = new Document("input.pdf"))
            {
                // Insert TOC page at the beginning (page index is 1‑based).
                Page tocPage = doc.Pages.Insert(1);
                TocInfo tocInfo = new TocInfo();

                // Create a title for the TOC.
                TextFragment tocTitle = new TextFragment("Table of Contents");
                tocTitle.TextState.FontSize = 16;
                tocTitle.TextState.FontStyle = FontStyles.Bold;
                tocInfo.Title = tocTitle;
                tocInfo.IsShowPageNumbers = true;
                tocInfo.CopyToOutlines = true;
                tocPage.TocInfo = tocInfo;

                // Add a TOC entry for each content page.
                for (int i = 2; i <= doc.Pages.Count; i++)
                {
                    Page sourcePage = doc.Pages[i];
                    Heading tocEntry = new Heading(1);
                    tocEntry.TocPage = tocPage;
                    tocEntry.DestinationPage = sourcePage;
                    // Position the entry at the top of the source page height.
                    tocEntry.Top = sourcePage.Rect.Height;
                    string entryText = "Chapter " + (i - 1);
                    tocEntry.Segments.Add(new TextSegment(entryText));
                    tocEntry.IsAutoSequence = true;
                    tocEntry.IsInList = true;
                    tocPage.Paragraphs.Add(tocEntry);
                }

                // Save the final document with TOC.
                doc.Save("output.pdf");
            }
        }
    }
}
