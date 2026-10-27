using System;
using Aspose.Pdf;
using Aspose.Pdf.Text; // optional, for TextFragment

class Program
{
    static void Main()
    {
        const string outputPath = "landscape.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (Pages are 1‑based)
            Page page = doc.Pages.Add();

            // Set the page size to landscape by assigning Width > Height.
            // Example: A4 landscape (842 x 595 points). Adjust as needed.
            page.PageInfo.Width = 842;   // points (1 point = 1/72 inch)
            page.PageInfo.Height = 595;

            // OPTIONAL: add a text fragment to visualize the orientation
            // TextFragment tf = new TextFragment("Landscape Page");
            // tf.TextState.FontSize = 24;
            // tf.TextState.HorizontalAlignment = HorizontalAlignment.Center;
            // page.Paragraphs.Add(tf);

            // Save the PDF (extension .pdf ensures PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
