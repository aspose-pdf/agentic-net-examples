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

        // Load the PDF document.
        Document pdfDoc = new Document(inputPath);

        // Define rectangle coordinates (lower‑left x, lower‑left y, upper‑right x, upper‑right y).
        // Use the fully‑qualified Aspose.Pdf.Rectangle to avoid ambiguity with System.Drawing.Rectangle.
        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

        // Create a square (rectangle) annotation.
        SquareAnnotation square = new SquareAnnotation(pdfDoc.Pages[1], rect);
        // Light gray fill color – use the InteriorColor property (FillColor is not available).
        square.InteriorColor = Aspose.Pdf.Color.LightGray;
        // Border with solid style and 1 pt width – Border requires the parent annotation in its constructor.
        square.Border = new Border(square) { Width = 1, Style = BorderStyle.Solid };

        // Add the annotation to the first page.
        pdfDoc.Pages[1].Annotations.Add(square);

        // Save the modified PDF.
        pdfDoc.Save(outputPath);

        Console.WriteLine($"Rectangle annotation added and saved to '{outputPath}'.");
    }
}
