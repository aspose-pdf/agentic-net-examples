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
            // 1 inch = 72 points, so convert inches to points
            double widthInPoints  = 5 * 72; // 360 points
            double heightInPoints = 7 * 72; // 504 points
            page.SetPageSize(widthInPoints, heightInPoints);

            // Example content (optional): add a simple text fragment
            // TextFragment tf = new TextFragment("Hello, Postcard!");
            // page.Paragraphs.Add(tf);

            // Save the PDF to the specified file
            doc.Save(outputPath);
        }

        Console.WriteLine($"Postcard PDF created at '{outputPath}'.");
    }
}