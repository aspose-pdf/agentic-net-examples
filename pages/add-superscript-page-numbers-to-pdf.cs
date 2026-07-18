using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // ---------------------------------------------------------------------
        // Create a self‑contained input PDF so the example works in an empty sandbox
        // ---------------------------------------------------------------------
        using (Document seed = new Document())
        {
            // Add a few pages with placeholder text (the exact content is not important)
            for (int p = 1; p <= 3; p++)
            {
                Page page = seed.Pages.Add();
                TextFragment tf = new TextFragment($"Sample content for page {p}");
                tf.TextState.FontSize = 12;
                page.Paragraphs.Add(tf);
            }
            seed.Save(inputPath);
        }

        // ---------------------------------------------------------------------
        // Load the PDF document and insert page numbers with superscript styling
        // ---------------------------------------------------------------------
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a page‑number stamp; default format is "#"
                PageNumberStamp stamp = new PageNumberStamp();

                // Position the stamp at the bottom‑right corner
                stamp.HorizontalAlignment = HorizontalAlignment.Right;
                stamp.VerticalAlignment   = VerticalAlignment.Bottom;
                stamp.BottomMargin        = 20;   // distance from the bottom edge

                // Superscript‑like formatting:
                //   • Reduce the font size
                //   • Raise the baseline using YIndent (TextState.Rise is not exposed)
                stamp.TextState.FontSize = 8;    // smaller than normal body text
                stamp.YIndent = 4;               // move the stamp up a few points

                // Optional: set a specific font (Helvetica is a common PDF base font)
                stamp.TextState.Font = FontRepository.FindFont("Helvetica");

                // Apply the stamp to the current page
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers with superscript formatting saved to '{outputPath}'.");
    }
}
