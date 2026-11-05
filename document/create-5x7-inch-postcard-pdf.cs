using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input and output file paths
        const string outputPath = "postcard.pdf";

        // 1 inch = 72 points. 5x7 inches = 360 x 504 points.
        const double widthPoints  = 5 * 72; // 360
        const double heightPoints = 7 * 72; // 504

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a new page to the document
            Page page = doc.Pages.Add();

            // Set the custom page size (width, height) in points
            page.SetPageSize(widthPoints, heightPoints);

            // OPTIONAL: add a sample text fragment to visualize the page
            // (requires Aspose.Pdf.Text namespace)
            Aspose.Pdf.Text.TextFragment text = new Aspose.Pdf.Text.TextFragment("Postcard 5x7 inches")
            {
                // Position the text near the top-left corner
                Position = new Aspose.Pdf.Text.Position(20, heightPoints - 40)
            };
            page.Paragraphs.Add(text);

            // Save the document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Postcard PDF created at '{Path.GetFullPath(outputPath)}'");
    }
}