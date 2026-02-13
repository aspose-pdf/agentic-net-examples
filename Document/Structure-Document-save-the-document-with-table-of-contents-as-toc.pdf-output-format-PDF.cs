using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        try
        {
            // Create a new PDF document
            Document pdfDocument = new Document();

            // -----------------------------------------------------------------
            // Add content pages with headings that will be referenced from TOC
            // -----------------------------------------------------------------
            // Page 1 – first heading
            Page page1 = pdfDocument.Pages.Add();
            TextFragment heading1 = new TextFragment("Chapter 1: Introduction")
            {
                TextState = { FontSize = 20, FontStyle = FontStyles.Bold }
            };
            page1.Paragraphs.Add(heading1);

            // Page 2 – second heading
            Page page2 = pdfDocument.Pages.Add();
            TextFragment heading2 = new TextFragment("Chapter 2: Details")
            {
                TextState = { FontSize = 20, FontStyle = FontStyles.Bold }
            };
            page2.Paragraphs.Add(heading2);

            // Page 3 – third heading
            Page page3 = pdfDocument.Pages.Add();
            TextFragment heading3 = new TextFragment("Chapter 3: Conclusion")
            {
                TextState = { FontSize = 20, FontStyle = FontStyles.Bold }
            };
            page3.Paragraphs.Add(heading3);

            // ---------------------------------------------------------------
            // Create a Table of Contents page (insert at the beginning)
            // ---------------------------------------------------------------
            // Insert a blank page at position 1 (TOC page)
            Page tocPage = pdfDocument.Pages.Insert(1);

            // Helper to add a TOC entry with a link to the target page
            void AddTocEntry(string title, int targetPageNumber, float yPosition)
            {
                // Create a text fragment for the TOC entry
                TextFragment tocEntry = new TextFragment(title)
                {
                    TextState = { FontSize = 14 }
                };
                // Position the entry using a TextFragment with a specific rectangle
                // (optional: you can adjust the rectangle as needed)
                tocEntry.Position = new Position(0, yPosition);
                tocPage.Paragraphs.Add(tocEntry);

                // Define the clickable area for the link (approximate rectangle)
                double llx = 0;
                double lly = yPosition - 5; // a little below the baseline
                double urx = tocPage.PageInfo.Width;
                double ury = yPosition + 15; // above the baseline

                // Create a link annotation that jumps to the target page
                // NOTE: Use the fully‑qualified Aspose.Pdf.Rectangle to avoid ambiguity
                LinkAnnotation link = new LinkAnnotation(tocPage, new Aspose.Pdf.Rectangle(llx, lly, urx, ury))
                {
                    Action = new GoToAction(pdfDocument.Pages[targetPageNumber])
                };
                tocPage.Annotations.Add(link);
            }

            // Add TOC entries (Y positions are chosen arbitrarily for demonstration)
            AddTocEntry("Chapter 1: Introduction", 2, 700);
            AddTocEntry("Chapter 2: Details", 3, 650);
            AddTocEntry("Chapter 3: Conclusion", 4, 600);

            // ---------------------------------------------------------------
            // Save the document as "toc.pdf"
            // ---------------------------------------------------------------
            string outputPath = "toc.pdf";
            pdfDocument.Save(outputPath);
            Console.WriteLine($"PDF saved successfully to '{System.IO.Path.GetFullPath(outputPath)}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
