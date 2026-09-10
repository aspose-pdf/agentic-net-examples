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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Page indexing in Aspose.Pdf is 1‑based; page 5 is the target page
            Page page = doc.Pages[5];

            // Define the rectangle for the square annotation (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

            // Create the square annotation on the specified page
            SquareAnnotation square = new SquareAnnotation(page, rect)
            {
                // Red border color
                Color = Aspose.Pdf.Color.Red,

                // Fill color (e.g., light gray)
                InteriorColor = Aspose.Pdf.Color.LightGray,

                // Semi‑transparent fill (0.0 = fully transparent, 1.0 = opaque)
                Opacity = 0.5
            };

            // Set the border width (Border requires the parent annotation in its constructor)
            square.Border = new Border(square) { Width = 2 };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(square);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Square annotation added and saved to '{outputPath}'.");
    }
}