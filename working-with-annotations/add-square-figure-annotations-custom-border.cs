using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_annotated.pdf";

        // Pages to receive the figure annotation (1‑based indexing)
        int[] pagesToAnnotate = { 1, 3, 5 };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Define custom appearance
            Aspose.Pdf.Color borderColor = Aspose.Pdf.Color.FromRgb(0.0, 0.5, 0.0); // dark green
            int lineWidth = 2; // points – Border.Width expects an int

            foreach (int pageNum in pagesToAnnotate)
            {
                // Ensure the page number is valid (Aspose.Pdf uses 1‑based indexing)
                if (pageNum < 1 || pageNum > doc.Pages.Count)
                    continue;

                Page page = doc.Pages[pageNum];

                // Define the annotation rectangle (lower‑left x/y, upper‑right x/y)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

                // Create a SquareAnnotation (a rectangular figure) on the specified page
                SquareAnnotation figAnno = new SquareAnnotation(page, rect);

                // Apply custom line width and color
                figAnno.Color = borderColor;
                // The Border property controls line width; ensure it is instantiated
                if (figAnno.Border == null)
                    figAnno.Border = new Border(figAnno);
                figAnno.Border.Width = lineWidth;

                // Provide alternative text for accessibility – use the Contents property
                figAnno.Contents = $"Figure on page {pageNum}";

                // Add the annotation to the page's annotation collection
                page.Annotations.Add(figAnno);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotated PDF saved to '{outputPath}'.");
    }
}
