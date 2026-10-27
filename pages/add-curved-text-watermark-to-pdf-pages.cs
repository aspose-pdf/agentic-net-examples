using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "watermarked.pdf";
        const string watermarkText = "CONFIDENTIAL";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Define a quadratic Bezier curve for the watermark path
            var start   = new Aspose.Pdf.Point(50, 400);
            var control = new Aspose.Pdf.Point(300, 800);
            var end     = new Aspose.Pdf.Point(550, 400);
            var curvePoints = GetQuadraticBezierPoints(start, control, end, 20);

            foreach (Page page in doc.Pages)
            {
                // Place a small text stamp at each point to simulate a curved watermark
                foreach (var pt in curvePoints)
                {
                    TextStamp stamp = new TextStamp(watermarkText);
                    stamp.TextState.Font = FontRepository.FindFont("Arial");
                    stamp.TextState.FontSize = 24;
                    stamp.TextState.FontStyle = FontStyles.Bold;
                    stamp.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(0.8, 0.0, 0.0);
                    stamp.Opacity = 0.3;
                    stamp.XIndent = pt.X;
                    stamp.YIndent = pt.Y;
                    // Simple rotation can be added if desired
                    stamp.RotateAngle = 0;
                    page.AddStamp(stamp);
                }
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }

    // Generates points on a quadratic Bezier curve
    static List<Aspose.Pdf.Point> GetQuadraticBezierPoints(Aspose.Pdf.Point p0, Aspose.Pdf.Point p1, Aspose.Pdf.Point p2, int segments)
    {
        var points = new List<Aspose.Pdf.Point>();
        for (int i = 0; i <= segments; i++)
        {
            double t = (double)i / segments;
            double oneMinusT = 1 - t;
            double x = oneMinusT * oneMinusT * p0.X + 2 * oneMinusT * t * p1.X + t * t * p2.X;
            double y = oneMinusT * oneMinusT * p0.Y + 2 * oneMinusT * t * p1.Y + t * t * p2.Y;
            points.Add(new Aspose.Pdf.Point(x, y));
        }
        return points;
    }
}