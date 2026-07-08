using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "multi_section.pdf";

        // Create a new PDF document inside a using block for proper disposal
        using (Document doc = new Document())
        {
            // ---------- Section 1: A4 portrait ----------
            Page page1 = doc.Pages.Add();                     // add a new page
            PageSize a4 = PageSize.A4;                        // A4 size (297 x 210 mm)
            page1.PageInfo.Width = a4.Width;                  // set width
            page1.PageInfo.Height = a4.Height;                // set height
            page1.PageInfo.IsLandscape = false;              // portrait orientation
            page1.Paragraphs.Add(new TextFragment("Section 1: A4 Portrait"));

            // ---------- Section 2: Letter landscape ----------
            Page page2 = doc.Pages.Add();
            PageSize letter = PageSize.PageLetter;            // Letter size (279 x 216 mm)
            // Swap width/height for landscape
            page2.PageInfo.Width = letter.Height;
            page2.PageInfo.Height = letter.Width;
            page2.PageInfo.IsLandscape = true;               // landscape orientation
            page2.Paragraphs.Add(new TextFragment("Section 2: Letter Landscape"));

            // ---------- Section 3: A5 portrait ----------
            Page page3 = doc.Pages.Add();
            PageSize a5 = PageSize.A5;                        // A5 size (210 x 148 mm)
            page3.PageInfo.Width = a5.Width;
            page3.PageInfo.Height = a5.Height;
            page3.PageInfo.IsLandscape = false;              // portrait orientation
            page3.Paragraphs.Add(new TextFragment("Section 3: A5 Portrait"));

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}