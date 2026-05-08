using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Operators;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPath  = "input.pdf";
        const string outputPath = "star_output.pdf";

        // Star parameters
        const float centerX = 300f;   // X coordinate of star centre
        const float centerY = 500f;   // Y coordinate of star centre
        const float outerRadius = 100f;
        const float innerRadius = 40f;
        const int   pointsCount = 5; // 5‑pointed star

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF (core API, no Facades)
        using (Document doc = new Document(inputPath))
        {
            // Ensure at least one page exists
            Page page = doc.Pages.Count > 0 ? doc.Pages[1] : doc.Pages.Add();

            // Prepare the operator collection for the page
            OperatorCollection ops = page.Contents;

            // Set fill color (light yellow) – values are 0‑1 range
            ops.Add(new SetRGBColor(1f, 1f, 0.8f));          // non‑stroking (fill) color
            // Set stroke color (dark orange)
            ops.Add(new SetRGBColorStroke(1f, 0.5f, 0f));    // stroking color
            // Set line width
            ops.Add(new SetLineWidth(2f));

            // Compute star vertices (alternating outer/inner points)
            float angleStep = (float)(Math.PI * 2 / (pointsCount * 2));
            float startAngle = -MathF.PI / 2; // start at top

            // Move to first vertex
            float x0 = centerX + outerRadius * MathF.Cos(startAngle);
            float y0 = centerY + outerRadius * MathF.Sin(startAngle);
            ops.Add(new MoveTo(x0, y0));

            // Add remaining vertices
            for (int i = 1; i < pointsCount * 2; i++)
            {
                float radius = (i % 2 == 0) ? outerRadius : innerRadius;
                float angle = startAngle + i * angleStep;
                float x = centerX + radius * MathF.Cos(angle);
                float y = centerY + radius * MathF.Sin(angle);
                ops.Add(new LineTo(x, y));
            }

            // Close the path and fill+stroke it
            ops.Add(new ClosePathFillStroke());

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Star shape added and saved to '{outputPath}'.");
    }
}