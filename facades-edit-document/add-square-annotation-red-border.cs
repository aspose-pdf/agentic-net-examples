using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "annotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 5 pages
            if (doc.Pages.Count < 5)
            {
                Console.Error.WriteLine("The document does not contain a page 5.");
                return;
            }

            // Get page 5 (Aspose.Pdf pages are 1‑based)
            Page page5 = doc.Pages[5];

            // Define the rectangle for the square annotation on page 5
            // Coordinates are in points; lower‑left (llx,lly) and upper‑right (urx,ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create the square annotation – the constructor expects a Page object, not an int
            SquareAnnotation square = new SquareAnnotation(page5, rect);

            // Set a red border (use the Color property for the border colour)
            square.Color = Aspose.Pdf.Color.FromRgb(1, 0, 0); // Red border

            // Optionally set border width
            square.Border = new Border(square) { Width = 2 };

            // Set a semi‑transparent red fill (50% opacity) using InteriorColor
            square.InteriorColor = Aspose.Pdf.Color.FromRgb(1, 0, 0); // Red fill
            square.Opacity = 0.5; // 0 = fully transparent, 1 = opaque

            // Add the annotation directly to the page's annotation collection
            page5.Annotations.Add(square);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Square annotation added and saved to '{outputPath}'.");
    }
}
