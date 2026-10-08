using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "multi_section.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // ---------- Section 1 ----------
            // A4 size, portrait orientation (595 x 842 points)
            Page page1 = doc.Pages.Add();
            page1.SetPageSize(595, 842);
            page1.Paragraphs.Add(new TextFragment("Section 1: A4 Portrait"));

            // ---------- Section 2 ----------
            // Letter size, landscape orientation (842 x 595 points)
            Page page2 = doc.Pages.Add();
            page2.SetPageSize(842, 595); // width > height => landscape
            page2.Paragraphs.Add(new TextFragment("Section 2: Letter Landscape"));

            // ---------- Section 3 ----------
            // Custom size (5" x 7"), portrait orientation (5*72 x 7*72 points)
            Page page3 = doc.Pages.Add();
            page3.SetPageSize(5 * 72, 7 * 72);
            page3.Paragraphs.Add(new TextFragment("Section 3: 5\" x 7\" Portrait"));

            // Save the PDF (output format is PDF by default)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF created at '{outputPath}'.");
    }
}
