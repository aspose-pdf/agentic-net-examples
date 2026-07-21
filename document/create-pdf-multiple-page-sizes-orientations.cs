using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // -------------------------------------------------
            // Section 1 – A4 size, portrait orientation
            // -------------------------------------------------
            Page page1 = doc.Pages.Add();                         // adds a new page
            page1.SetPageSize(PageSize.A4.Width, PageSize.A4.Height); // portrait
            TextFragment tf1 = new TextFragment("Section 1: A4 Portrait");
            page1.Paragraphs.Add(tf1);

            // -------------------------------------------------
            // Section 2 – A4 size, landscape orientation
            // -------------------------------------------------
            Page page2 = doc.Pages.Add();
            // Landscape: swap width and height
            page2.SetPageSize(PageSize.A4.Height, PageSize.A4.Width);
            TextFragment tf2 = new TextFragment("Section 2: A4 Landscape");
            page2.Paragraphs.Add(tf2);

            // -------------------------------------------------
            // Section 3 – Letter size, portrait orientation
            // -------------------------------------------------
            Page page3 = doc.Pages.Add();
            page3.SetPageSize(PageSize.PageLetter.Width, PageSize.PageLetter.Height);
            TextFragment tf3 = new TextFragment("Section 3: Letter Portrait");
            page3.Paragraphs.Add(tf3);

            // Save the document to a file (PDF format)
            doc.Save("multiple_sections.pdf");
        }

        Console.WriteLine("PDF with multiple sections created successfully.");
    }
}