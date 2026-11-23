using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 3 pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document does not contain a page 3.");
                return;
            }

            // Get page 3
            Page page = doc.Pages[3];

            // Define the rectangle for the figure annotation (coordinates in points)
            // Fully qualify Rectangle to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a SquareAnnotation (a rectangular figure) on the page
            SquareAnnotation figureAnno = new SquareAnnotation(page, rect)
            {
                // Border color
                Color = Aspose.Pdf.Color.LightGray,
                // Fill color – use InteriorColor (FillColor does not exist on SquareAnnotation)
                InteriorColor = Aspose.Pdf.Color.FromRgb(0.9, 0.9, 0.9)
            };

            // Set the border – Border requires the parent annotation in its constructor
            figureAnno.Border = new Border(figureAnno)
            {
                Width = 2,
                Style = BorderStyle.Solid
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(figureAnno);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Figure annotation added to page 3 and saved as '{outputPath}'.");
    }
}
