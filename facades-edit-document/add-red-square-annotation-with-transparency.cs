using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least five pages (1‑based indexing)
            if (doc.Pages.Count < 5)
            {
                Console.Error.WriteLine("The document does not contain a page 5.");
                return;
            }

            // Get page five
            Page page = doc.Pages[5];

            // Define the rectangle for the square annotation (llx, lly, urx, ury)
            // Adjust the coordinates as needed; here we place a 100x100 square at (100,500)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);

            // Create the square annotation on the specified page
            SquareAnnotation square = new SquareAnnotation(page, rect)
            {
                // Red border
                Color = Aspose.Pdf.Color.Red,

                // Semi‑transparent fill (same red color, opacity controls transparency)
                InteriorColor = Aspose.Pdf.Color.Red,
                Opacity = 0.5, // 0 = fully transparent, 1 = fully opaque

                // Optional: set a title or contents for the annotation
                Title = "Sample Square",
                Contents = "Square annotation with red border and semi‑transparent fill."
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(square);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Square annotation added and saved to '{outputPath}'.");
    }
}