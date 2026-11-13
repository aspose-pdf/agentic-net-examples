using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Operators;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "star_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (pages are 1‑based)
            Page page = doc.Pages[1];

            // Star parameters
            double centerX = 300;      // X coordinate of star centre
            double centerY = 400;      // Y coordinate of star centre
            double outerRadius = 100; // Radius of outer points
            double innerRadius = 40;  // Radius of inner points
            int points = 5;           // Number of star points

            // Angle between successive points (outer‑inner‑outer‑…)
            double angleStep = Math.PI / points; // 36° for a 5‑point star

            // Save current graphics state
            page.Contents.Add(new GSave());

            // Set drawing attributes (stroke and fill colours, line width)
            page.Contents.Add(new SetLineWidth(2));
            page.Contents.Add(new SetRGBColorStroke(0, 0, 0));               // black stroke
            page.Contents.Add(new SetRGBColor(0.78, 0.78, 0.78));            // light‑gray fill (values 0‑1)

            // Move to the first outer point
            double startX = centerX + outerRadius * Math.Sin(0);
            double startY = centerY + outerRadius * Math.Cos(0);
            page.Contents.Add(new MoveTo(startX, startY));

            // Alternate between outer and inner points to build the star
            for (int i = 1; i <= points * 2; i++)
            {
                double radius = (i % 2 == 0) ? outerRadius : innerRadius;
                double angle = i * angleStep;
                double x = centerX + radius * Math.Sin(angle);
                double y = centerY + radius * Math.Cos(angle);
                page.Contents.Add(new LineTo(x, y));
            }

            // Close the path and fill+stroke it
            page.Contents.Add(new ClosePathFillStroke());

            // Restore the previous graphics state
            page.Contents.Add(new GRestore());

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Star shape added and saved to '{outputPath}'.");
    }
}
