using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "postcard.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Set the page size to 5 inches × 7 inches.
            // Aspose.Pdf uses points (1 inch = 72 points).
            double width  = 5 * 72; // 360 points
            double height = 7 * 72; // 504 points
            page.SetPageSize(width, height);

            // -------------------------------------------------
            // Optional: add sample content (e.g., a text fragment)
            // -------------------------------------------------
            // Uncomment the following lines if you want to place text on the postcard.
            // using Aspose.Pdf.Text;
            // TextFragment tf = new TextFragment("Postcard");
            // tf.Position = new Position(100, 400);
            // page.Paragraphs.Add(tf);
            // -------------------------------------------------

            // Save the PDF with the custom page size
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}