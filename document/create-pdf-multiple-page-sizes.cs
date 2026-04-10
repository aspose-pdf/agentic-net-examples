using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "multi_section.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Section 1: A4 portrait
            Page page1 = doc.Pages.Add();
            page1.Resize(PageSize.A4); // A4 size (portrait)
            AddSectionTitle(page1, "Section 1 - A4 Portrait");

            // Section 2: Letter landscape
            Page page2 = doc.Pages.Add();
            page2.Resize(PageSize.PageLetter); // Letter size (portrait)
            // Switch to landscape by swapping width and height
            page2.SetPageSize(PageSize.PageLetter.Height, PageSize.PageLetter.Width);
            AddSectionTitle(page2, "Section 2 - Letter Landscape");

            // Section 3: Custom size 400x600 points (portrait)
            Page page3 = doc.Pages.Add();
            page3.SetPageSize(400, 600);
            AddSectionTitle(page3, "Section 3 - Custom 400x600");

            // Section 4: A5 portrait
            Page page4 = doc.Pages.Add();
            page4.Resize(PageSize.A5);
            AddSectionTitle(page4, "Section 4 - A5 Portrait");

            // Save the document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }

    // Helper method to add a title text to a page
    static void AddSectionTitle(Page page, string title)
    {
        // Create a text fragment with desired styling
        TextFragment tf = new TextFragment(title);
        tf.TextState.FontSize = 24;
        tf.TextState.Font = FontRepository.FindFont("Helvetica");
        // Position near the top-left corner (50 points margin)
        tf.Position = new Position(50, page.PageInfo.Height - 50);
        page.Paragraphs.Add(tf);
    }
}