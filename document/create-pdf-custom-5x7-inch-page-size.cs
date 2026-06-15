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

            // Set the page size to 5 inches (width) x 7 inches (height)
            // 1 inch = 72 points, so 5*72 = 360, 7*72 = 504
            double width  = 5 * 72; // 360 points
            double height = 7 * 72; // 504 points
            page.SetPageSize(width, height);

            // Save the PDF with the custom page size
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}