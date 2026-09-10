using System;
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
            // ---------- Section 1: A4 portrait ----------
            Page page1 = doc.Pages.Add(); // adds a new page
            // Set size to A4 (portrait)
            page1.SetPageSize(PageSize.A4.Width, PageSize.A4.Height);
            // Add identifying text
            page1.Paragraphs.Add(new TextFragment("Section 1: A4 Portrait"));

            // ---------- Section 2: Letter landscape ----------
            Page page2 = doc.Pages.Add();
            // Letter size dimensions
            double letterWidth  = PageSize.PageLetter.Width;
            double letterHeight = PageSize.PageLetter.Height;
            // Swap width/height for landscape orientation
            page2.SetPageSize(letterHeight, letterWidth);
            page2.Paragraphs.Add(new TextFragment("Section 2: Letter Landscape"));

            // ---------- Section 3: Custom size (500 x 300 points) portrait ----------
            Page page3 = doc.Pages.Add();
            page3.SetPageSize(500, 300); // custom width and height
            page3.Paragraphs.Add(new TextFragment("Section 3: Custom 500x300 Portrait"));

            // Save the document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}