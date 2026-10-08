using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "postcard.pdf";

        // Create a new PDF document and ensure it is disposed properly
        using (Document doc = new Document())
        {
            // Add a new page to the document
            Page page = doc.Pages.Add();

            // Define conversion factor from inches to points (1 inch = 72 points)
            const double inchesToPoints = 72.0;

            // Set the custom page size to 5 x 7 inches
            page.PageInfo.Width  = 5 * inchesToPoints; // 360 points
            page.PageInfo.Height = 7 * inchesToPoints; // 504 points

            // Save the document as a PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}