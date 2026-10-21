using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_dashed_rect.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page where the annotation will be placed
            Page page = doc.Pages[1];

            // Define rectangle coordinates (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

            // Use SquareAnnotation (the correct class for rectangular annotations)
            SquareAnnotation square = new SquareAnnotation(page, rect)
            {
                Title = "Note",
                Contents = "Dashed border example",
                // Border color is set via the annotation's Color property
                Color = Aspose.Pdf.Color.Blue
            };

            // Configure a dashed border. Border requires the parent annotation in its constructor.
            square.Border = new Border(square)
            {
                Style = BorderStyle.Dashed,
                Width = 2
            };

            // Attach the annotation to the page
            page.Annotations.Add(square);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with dashed rectangle annotation to '{outputPath}'.");
    }
}