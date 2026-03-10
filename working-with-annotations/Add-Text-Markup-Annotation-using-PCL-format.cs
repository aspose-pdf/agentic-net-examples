using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPclPath  = "input.pcl";
        const string outputPdfPath = "annotated_output.pdf";

        if (!File.Exists(inputPclPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPclPath}");
            return;
        }

        // Load the PCL file (PCL is input‑only). Use PclLoadOptions.
        using (Document doc = new Document(inputPclPath, new PclLoadOptions()))
        {
            // Ensure the document has at least one page.
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The loaded document contains no pages.");
                return;
            }

            // Create a Highlight annotation on the first page.
            // Define a rectangle where the highlight will appear.
            // Rectangle constructor: (llx, lly, width, height)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 20);

            // HighlightAnnotation requires a page and a rectangle.
            HighlightAnnotation highlight = new HighlightAnnotation(doc.Pages[1], rect)
            {
                // Optional visual properties.
                Color = Aspose.Pdf.Color.Yellow,
                Contents = "Highlighted text"
            };

            // QuadPoints define the exact quadrilaterals under the annotation.
            // For a simple rectangular highlight, four points (two triangles) are enough.
            highlight.QuadPoints = new[]
            {
                new Aspose.Pdf.Point(rect.LLX, rect.URY), // top‑left
                new Aspose.Pdf.Point(rect.URX, rect.URY), // top‑right
                new Aspose.Pdf.Point(rect.LLX, rect.LLY), // bottom‑left
                new Aspose.Pdf.Point(rect.URX, rect.LLY)  // bottom‑right
            };

            // Add the annotation to the page.
            doc.Pages[1].Annotations.Add(highlight);

            // Save the result as PDF (PCL cannot be saved).
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Annotated PDF saved to '{outputPdfPath}'.");
    }
}