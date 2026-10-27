using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "linked_output.pdf";

        // Ensure the output directory exists (if a directory part is present)
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // Create and work with the PDF document inside a using block (lifecycle rule)
        using (Document doc = new Document())
        {
            // Add two pages (page indexing is 1‑based)
            Page page1 = doc.Pages.Add(); // first page
            Page page2 = doc.Pages.Add(); // second page

            // -------------------------------------------------
            // Page 2: add some placeholder content
            // -------------------------------------------------
            TextFragment page2Text = new TextFragment("This is page 2.");
            page2Text.Position = new Position(100, 700);
            page2.Paragraphs.Add(page2Text);

            // -------------------------------------------------
            // Page 1: create a visible TextFragment that acts as a link
            // -------------------------------------------------
            TextFragment linkFragment = new TextFragment("Go to Page 2");
            // Position the text on the page
            linkFragment.Position = new Position(100, 700);
            // Optional styling (underline and blue color)
            linkFragment.TextState.Underline = true;
            // Use predefined Aspose.Pdf.Color to avoid out‑of‑range values
            linkFragment.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;
            page1.Paragraphs.Add(linkFragment);

            // -------------------------------------------------
            // Page 1: create a LinkAnnotation that points to page 2
            // -------------------------------------------------
            // Estimate a rectangle that covers the text. The rectangle is defined as
            // (llx, lly, urx, ury) – lower‑left and upper‑right corners.
            // Here we use a simple fixed width; for production code you could measure the text.
            double llx = 100;               // left X (same as text X)
            double lly = 690;               // lower Y (a bit below the baseline)
            double urx = 250;               // right X (approximate width of the string)
            double ury = 710;               // upper Y (a bit above the baseline)
            LinkAnnotation link = new LinkAnnotation(page1, new Aspose.Pdf.Rectangle(llx, lly, urx, ury))
            {
                Action = new GoToAction(page2)
            };
            page1.Annotations.Add(link);

            // Save the PDF (still inside the using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with GoTo link saved to '{outputPath}'.");
    }
}
