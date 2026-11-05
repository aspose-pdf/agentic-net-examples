using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "multi_section.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Section 1: A4 size, portrait orientation (width < height)
            Page page1 = doc.Pages.Add();
            page1.SetPageSize(PageSize.A4.Width, PageSize.A4.Height);
            page1.Paragraphs.Add(new TextFragment("Section 1: A4 Portrait"));

            // Section 2: Letter size, landscape orientation (swap width/height)
            // Letter size in points: 612 (width) x 792 (height). Landscape => 792 x 612.
            Page page2 = doc.Pages.Add();
            page2.SetPageSize(792, 612); // landscape Letter
            page2.Paragraphs.Add(new TextFragment("Section 2: Letter Landscape"));

            // Section 3: Custom size (200 x 300 points), portrait orientation
            Page page3 = doc.Pages.Add();
            page3.SetPageSize(200, 300);
            page3.Paragraphs.Add(new TextFragment("Section 3: Custom Size 200x300 Portrait"));

            // Section 4: A5 size, landscape orientation (swap width/height)
            Page page4 = doc.Pages.Add();
            page4.SetPageSize(PageSize.A5.Height, PageSize.A5.Width); // landscape
            page4.Paragraphs.Add(new TextFragment("Section 4: A5 Landscape"));

            // Save the document as a PDF file
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF created: {outputPath}");
    }
}
