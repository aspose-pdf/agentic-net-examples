using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_with_text.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Create a Graph that will hold vector shapes (double literals as required)
            double graphWidth = 400.0;
            double graphHeight = 300.0;
            Graph graph = new Graph(graphWidth, graphHeight)
            {
                // Position the graph on the page (coordinates are from the bottom‑left corner)
                Left = 50,
                Top = 500
            };

            // ----- Rectangle shape -----
            var rectShape = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 150f, 100f);
            rectShape.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 1f
            };
            graph.Shapes.Add(rectShape);

            // ----- Ellipse shape -----
            var ellipseShape = new Aspose.Pdf.Drawing.Ellipse(200f, 0f, 350f, 100f);
            ellipseShape.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightBlue,
                Color = Aspose.Pdf.Color.DarkBlue,
                LineWidth = 1f
            };
            graph.Shapes.Add(ellipseShape);

            // ----- Line shape -----
            float[] linePoints = { 0f, 150f, 350f, 150f };
            var lineShape = new Aspose.Pdf.Drawing.Line(linePoints);
            lineShape.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Red,
                LineWidth = 2f
            };
            graph.Shapes.Add(lineShape);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // ----- Text inside the rectangle -----
            TextFragment tfRect = new TextFragment("Rect Text");
            tfRect.Position = new Position(70, 560); // Adjusted to lie within the rectangle
            tfRect.TextState.Font = Aspose.Pdf.Text.FontRepository.FindFont("Helvetica");
            tfRect.TextState.FontSize = 12;
            tfRect.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // ----- Text inside the ellipse -----
            TextFragment tfEllipse = new TextFragment("Ellipse Text");
            tfEllipse.Position = new Position(260, 560); // Centered in the ellipse
            tfEllipse.TextState.Font = Aspose.Pdf.Text.FontRepository.FindFont("TimesNewRoman");
            tfEllipse.TextState.FontSize = 14;
            tfEllipse.TextState.ForegroundColor = Aspose.Pdf.Color.Maroon;

            // ----- Text on the line -----
            TextFragment tfLine = new TextFragment("Line Text");
            tfLine.Position = new Position(180, 470); // Near the line
            tfLine.TextState.Font = Aspose.Pdf.Text.FontRepository.FindFont("Courier");
            tfLine.TextState.FontSize = 10;
            tfLine.TextState.ForegroundColor = Aspose.Pdf.Color.Green;

            // Append all text fragments to the page
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(tfRect);
            builder.AppendText(tfEllipse);
            builder.AppendText(tfLine);

            // Save the PDF – guard against missing libgdiplus on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF saved to '{outputPath}'. (non‑Windows platform, libgdiplus may be required)");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: libgdiplus (GDI+) is not available on this platform. PDF was not saved.");
                }
            }
        }
    }

    // Helper to detect a nested DllNotFoundException (e.g., missing libgdiplus)
    private static bool ContainsDllNotFound(Exception? ex)
    {
        while (ex != null)
        {
            if (ex is DllNotFoundException)
                return true;
            ex = ex.InnerException;
        }
        return false;
    }
}
