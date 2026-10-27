using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    // Simple overlap check for two rectangles
    static bool Overlaps(Rectangle r1, Rectangle r2)
    {
        return r1.LLX < r2.URX && r1.URX > r2.LLX &&
               r1.LLY < r2.URY && r1.URY > r2.LLY;
    }

    static void Main()
    {
        const string outputPath = "graph_rectangles.pdf";

        // Ensure output directory exists (handle case where Path.GetDirectoryName returns null)
        string outDir = Path.GetDirectoryName(Path.GetFullPath(outputPath)) ?? Directory.GetCurrentDirectory();
        if (!Directory.Exists(outDir))
            Directory.CreateDirectory(outDir);

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page (first page is at index 1)
            Page page = doc.Pages.Add();

            // List to keep track of placed rectangle bounds
            List<Rectangle> placedRects = new List<Rectangle>();

            // Define rectangle sizes (width, height) and desired positions (llx, lly)
            var rectDefinitions = new (double llx, double lly, double width, double height)[]
            {
                (50, 700, 100, 50),
                (200, 700, 120, 80),
                (350, 700, 80, 120),
                (50, 550, 150, 70),
                (250, 550, 100, 100),
                (400, 550, 90, 60),
                (100, 400, 130, 90),
                (300, 400, 110, 110)
            };

            foreach (var def in rectDefinitions)
            {
                // Compute rectangle coordinates
                double llx = def.llx;
                double lly = def.lly;
                double urx = llx + def.width;
                double ury = lly + def.height;
                Rectangle newRect = new Rectangle(llx, lly, urx, ury);

                // Check against all previously placed rectangles
                bool hasOverlap = false;
                foreach (var existing in placedRects)
                {
                    if (Overlaps(newRect, existing))
                    {
                        hasOverlap = true;
                        break;
                    }
                }

                if (hasOverlap)
                {
                    Console.WriteLine($"Skipped rectangle at ({llx},{lly}) size ({def.width}x{def.height}) due to overlap.");
                    continue;
                }

                // No overlap – add as a square annotation (the API name for a rectangular annotation)
                SquareAnnotation squareAnnot = new SquareAnnotation(page, newRect)
                {
                    // FillColor property is not available in the current Aspose.Pdf version; omit it.
                    Title = "Rect",
                    Contents = $"Rect {placedRects.Count + 1}"
                };
                // Set border color and width (border color is controlled by the annotation's Color property)
                squareAnnot.Color = Color.Black;
                squareAnnot.Border = new Border(squareAnnot) { Width = 2 };

                page.Annotations.Add(squareAnnot);

                // Store the bounds for future checks
                placedRects.Add(newRect);
                Console.WriteLine($"Added rectangle {placedRects.Count} at ({llx},{lly}) size ({def.width}x{def.height}).");
            }

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with rectangles saved to '{outputPath}'.");
    }
}
