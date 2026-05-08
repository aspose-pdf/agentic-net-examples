using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    // Generates points on a quadratic Bezier curve.
    static void GetBezierPoints(double startX, double startY,
                                double controlX, double controlY,
                                double endX, double endY,
                                int steps,
                                out double[] xs,
                                out double[] ys)
    {
        xs = new double[steps];
        ys = new double[steps];
        for (int i = 0; i < steps; i++)
        {
            double t = (double)i / (steps - 1);
            double oneMinusT = 1 - t;
            xs[i] = oneMinusT * oneMinusT * startX
                    + 2 * oneMinusT * t * controlX
                    + t * t * endX;
            ys[i] = oneMinusT * oneMinusT * startY
                    + 2 * oneMinusT * t * controlY
                    + t * t * endY;
        }
    }

    // Calculates angle (in degrees) of the tangent at a point on the Bezier curve.
    static double GetTangentAngle(double startX, double startY,
                                  double controlX, double controlY,
                                  double endX, double endY,
                                  double t)
    {
        double dx = 2 * (1 - t) * (controlX - startX) + 2 * t * (endX - controlX);
        double dy = 2 * (1 - t) * (controlY - startY) + 2 * t * (endY - controlY);
        return Math.Atan2(dy, dx) * 180.0 / Math.PI;
    }

    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "watermarked.pdf";
        const string watermarkText = "CONFIDENTIAL";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document.
        using (Document doc = new Document(inputPdf))
        {
            // Define a simple quadratic Bezier curve.
            // Adjust these coordinates to fit your page size and desired path.
            double startX = 100, startY = 500;
            double controlX = 300, controlY = 700;
            double endX = 500, endY = 500;

            // Number of watermark fragments along the curve.
            int steps = 20;

            // Pre‑compute points on the curve.
            GetBezierPoints(startX, startY, controlX, controlY, endX, endY, steps,
                            out double[] xs, out double[] ys);

            // Load a font for the watermark text.
            Font font = FontRepository.FindFont("Helvetica");

            // Apply the curved watermark to each page.
            foreach (Page page in doc.Pages)
            {
                for (int i = 0; i < steps; i++)
                {
                    // Compute rotation so the text follows the curve direction.
                    double t = (double)i / (steps - 1);
                    double angle = GetTangentAngle(startX, startY, controlX, controlY, endX, endY, t);

                    // Create a watermark artifact for this fragment.
                    WatermarkArtifact artifact = new WatermarkArtifact();

                    // Set the text and its visual style.
                    artifact.Text = watermarkText;
                    artifact.TextState.Font = font;
                    artifact.TextState.FontSize = 24;
                    artifact.TextState.ForegroundColor = Color.FromRgb(1, 0, 0); // Red

                    // Position the artifact at the current point on the curve.
                    artifact.Position = new Point(xs[i], ys[i]);

                    // Rotate the artifact so it aligns with the curve.
                    artifact.Rotation = angle;

                    // Make the watermark semi‑transparent.
                    artifact.Opacity = 0.3;

                    // Add the artifact to the page.
                    page.Artifacts.Add(artifact);
                }
            }

            // Save the modified PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Curved watermark added and saved to '{outputPdf}'.");
    }
}