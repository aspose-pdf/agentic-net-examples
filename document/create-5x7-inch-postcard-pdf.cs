using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // needed for TextFragment, Position, FontRepository

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

            // Set custom page size: 5 inches (width) x 7 inches (height)
            // 1 inch = 72 points, so 5*72 = 360, 7*72 = 504
            double width  = 5 * 72; // 360 points
            double height = 7 * 72; // 504 points
            page.SetPageSize(width, height);

            // OPTIONAL: add a sample text fragment to demonstrate the page
            TextFragment tf = new TextFragment("Hello, Postcard!");
            tf.TextState.FontSize = 24;
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.ForegroundColor = Color.Blue;
            // Position the text somewhere on the page
            tf.Position = new Position(100, 400);
            page.Paragraphs.Add(tf);

            // Save the PDF to the specified path
            doc.Save(outputPath);
        }

        Console.WriteLine($"Postcard PDF saved to '{outputPath}'.");
    }
}