using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    // Cubic Bézier helper – returns point on the curve for parameter t (0‑1)
    private static (double X, double Y) BezierPoint(double t, double[] p0, double[] p1, double[] p2, double[] p3)
    {
        double u = 1 - t;
        double x = u * u * u * p0[0] + 3 * u * u * t * p1[0] + 3 * u * t * t * p2[0] + t * t * t * p3[0];
        double y = u * u * u * p0[1] + 3 * u * u * t * p1[1] + 3 * u * t * t * p2[1] + t * t * t * p3[1];
        return (x, y);
    }

    // Derivative of the cubic Bézier – used to compute the tangent (angle) at t
    private static (double X, double Y) BezierDerivative(double t, double[] p0, double[] p1, double[] p2, double[] p3)
    {
        double u = 1 - t;
        double x = 3 * u * u * (p1[0] - p0[0]) + 6 * u * t * (p2[0] - p1[0]) + 3 * t * t * (p3[0] - p2[0]);
        double y = 3 * u * u * (p1[1] - p0[1]) + 6 * u * t * (p2[1] - p1[1]) + 3 * t * t * (p3[1] - p2[1]);
        return (x, y);
    }

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
            // Define the Bézier curve that the text will follow (page‑relative coordinates)
            double[] p0 = { 50, 100 };   // bottom‑left
            double[] p1 = { 200, 800 };  // first control
            double[] p2 = { 400, 800 };  // second control
            double[] p3 = { 550, 100 };  // top‑right

            int charCount = watermarkText.Length;

            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                for (int i = 0; i < charCount; i++)
                {
                    double t = (i + 0.5) / charCount; // 0‑1 range
                    var (x, y) = BezierPoint(t, p0, p1, p2, p3);
                    var (dx, dy) = BezierDerivative(t, p0, p1, p2, p3);

                    double angle = Math.Atan2(dy, dx) * 180.0 / Math.PI;

                    TextFragment tf = new TextFragment(watermarkText[i].ToString());
                    tf.TextState.FontSize = 48;
                    tf.TextState.Font = FontRepository.FindFont("Arial");
                    // 30% opacity → alpha ≈ 0.3 * 255 ≈ 77
                    tf.TextState.ForegroundColor = Color.FromArgb(77, 204, 0, 0); // semi‑red with transparency
                    tf.Position = new Position(x, y);
                    tf.TextState.Rotation = angle;

                    page.Paragraphs.Add(tf);
                }
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{Path.GetFullPath(outputPath)}'.");
    }
}
