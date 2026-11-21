using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "landscape.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Set the page size so that width > height → landscape orientation.
            // Example uses A4 dimensions in points (1 point = 1/72 inch).
            page.PageInfo.Width = 842;   // Landscape width
            page.PageInfo.Height = 595;  // Landscape height

            // Optional: add some sample text to verify the orientation
            TextFragment tf = new TextFragment("Landscape page");
            tf.TextState.FontSize = 24;
            page.Paragraphs.Add(tf);

            // Save the document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}