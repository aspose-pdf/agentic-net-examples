using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_figures.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Add a square figure annotation
            // Fully qualified Rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle squareRect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);
            SquareAnnotation square = new SquareAnnotation(page, squareRect)
            {
                Color = Aspose.Pdf.Color.Blue,          // Border color
                InteriorColor = Aspose.Pdf.Color.LightGray, // Fill color
                Contents = "Square Figure"
            };
            page.Annotations.Add(square);

            // Add a circle figure annotation
            Aspose.Pdf.Rectangle circleRect = new Aspose.Pdf.Rectangle(250, 500, 350, 600);
            CircleAnnotation circle = new CircleAnnotation(page, circleRect)
            {
                Color = Aspose.Pdf.Color.Red,
                InteriorColor = Aspose.Pdf.Color.Yellow,
                Contents = "Circle Figure"
            };
            page.Annotations.Add(circle);

            // NOTE: Aspose.Pdf does NOT support saving to PostScript (PS) format.
            // The document must be saved as PDF (or another supported format) instead.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Figure annotations added and saved to '{outputPath}'.");
    }
}