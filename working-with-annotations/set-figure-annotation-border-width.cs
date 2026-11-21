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
            // Work with the first page (adjust as needed)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (lower‑left x/y, upper‑right x/y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create a square annotation which serves as a visual "figure" annotation
            SquareAnnotation figureAnno = new SquareAnnotation(page, rect)
            {
                Title = "Figure",
                Contents = "Figure annotation with thicker border",
                // Border colour is set via the annotation's Color property
                Color = Aspose.Pdf.Color.Black
            };

            // Set the line thickness (border width) to 2 points
            // Border constructor requires the owning annotation as argument
            figureAnno.Border = new Border(figureAnno) { Width = 2 };

            // Add the annotation to the page
            page.Annotations.Add(figureAnno);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Figure annotation saved with 2 pt border to '{outputPath}'.");
    }
}
