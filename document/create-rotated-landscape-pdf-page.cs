using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "rotated_landscape.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Set the page size to Letter (you can choose any size)
            page.SetPageSize(PageSize.PageLetter.Width, PageSize.PageLetter.Height);

            // Rotate the page 90 degrees clockwise to achieve landscape orientation
            // Note: Aspose.Pdf uses the "on" prefix for rotation enum values.
            page.Rotate = Rotation.on90;

            // Optionally, indicate that the page is landscape (affects some viewers)
            page.PageInfo.IsLandscape = true;

            // Add some sample content to visualize the orientation
            TextFragment tf = new TextFragment("Landscape page with 90° rotation");
            tf.Position = new Position(100, 500); // position after rotation
            page.Paragraphs.Add(tf);

            // Save the PDF to the specified path
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF created with rotated landscape page: {outputPath}");
    }
}
