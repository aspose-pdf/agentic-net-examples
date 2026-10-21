using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "annotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Define the annotation rectangle (example coordinates)
        double llx = 100; // lower‑left X
        double lly = 500; // lower‑left Y
        double urx = 300; // upper‑right X
        double ury = 550; // upper‑right Y

        // Bright red border color (RGB 255,0,0) – Aspose.Pdf.Color uses 0‑1 range
        Color redBorder = Color.FromRgb(1, 0, 0);
        // Transparent fill (no background) – default is transparent, so no property needed

        // Load the PDF document
        Document pdfDoc = new Document(inputPath);
        // Get the first page (1‑based index)
        Page page = pdfDoc.Pages[1];

        // Create a square annotation with the specified rectangle
        Rectangle rect = new Rectangle(llx, lly, urx, ury);
        SquareAnnotation square = new SquareAnnotation(page, rect)
        {
            Color = redBorder // border color
            // No FillColor property in this version; omission leaves the interior transparent
        };

        // Add the annotation to the page
        page.Annotations.Add(square);

        // Save the modified PDF
        pdfDoc.Save(outputPath);

        Console.WriteLine($"Annotation added. Saved to '{outputPath}'.");
    }
}
