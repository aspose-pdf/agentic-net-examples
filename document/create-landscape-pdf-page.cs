using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "landscape.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a new page to the document
            Page page = doc.Pages.Add();

            // Optionally set a specific page size (e.g., A4)
            page.SetPageSize(PageSize.A4.Width, PageSize.A4.Height);

            // Indicate that the page should be treated as landscape
            page.PageInfo.IsLandscape = true;

            // Rotate the page 90 degrees clockwise to achieve landscape orientation
            page.Rotate = Rotation.on90; // correct enum value

            // Add a sample text fragment to visualize the orientation
            TextFragment tf = new TextFragment("Landscape Page");
            tf.Position = new Position(100, 500);
            page.Paragraphs.Add(tf);

            // Save the PDF to the specified path
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}