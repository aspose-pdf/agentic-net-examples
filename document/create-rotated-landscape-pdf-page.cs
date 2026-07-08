using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "rotated_landscape.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Set the page size to a rotated Letter format (11" x 8.5")
            // Letter size is 612 x 792 points (8.5" x 11").
            // For landscape we swap width and height (792 x 612).
            page.MediaBox = new Rectangle(0, 0, 792, 612);

            // Rotate the page 90 degrees clockwise to achieve landscape orientation
            page.Rotate = Rotation.on90;

            // Add a sample text fragment to visualize the orientation
            TextFragment tf = new TextFragment("Landscape Page");
            tf.Position = new Position(100, 500);
            page.Paragraphs.Add(tf);

            // Save the PDF file
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
