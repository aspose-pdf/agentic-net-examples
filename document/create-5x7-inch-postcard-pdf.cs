using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "postcard.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Set custom page size: 5 inches × 7 inches.
            // 1 inch = 72 points, so width = 360, height = 504.
            double width  = 5 * 72; // 360 points
            double height = 7 * 72; // 504 points
            page.SetPageSize(width, height);

            // OPTIONAL: add a sample text fragment to verify the size
            TextFragment tf = new TextFragment("Hello, postcard!");
            tf.Position = new Position(50, height - 50); // 50 points from left/top
            page.Paragraphs.Add(tf);

            // Save the PDF to the specified file
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}