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

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // ------------------------------------------------------------
            // Create a Graph (acts as a container for vector shapes)
            // Width = 400 points, Height = 200 points (use double ctor)
            // ------------------------------------------------------------
            Graph graph = new Graph(400.0, 200.0);

            // ----- Rectangle shape inside the graph (use Drawing.Rectangle) -----
            var rectShape = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 200f, 100f)
            {
                GraphInfo = new GraphInfo
                {
                    FillColor = Aspose.Pdf.Color.LightGray,
                    Color = Aspose.Pdf.Color.Black,
                    LineWidth = 1f // float literal as required
                }
            };
            graph.Shapes.Add(rectShape);

            // ----- Ellipse shape inside the graph -----
            var ellipse = new Aspose.Pdf.Drawing.Ellipse(250f, 0f, 150f, 100f)
            {
                GraphInfo = new GraphInfo
                {
                    FillColor = Aspose.Pdf.Color.Yellow,
                    Color = Aspose.Pdf.Color.Red,
                    LineWidth = 1f
                }
            };
            graph.Shapes.Add(ellipse);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // ------------------------------------------------------------
            // Add TextFragments with different fonts and sizes positioned
            // within the shapes
            // ------------------------------------------------------------

            // Text inside the rectangle
            TextFragment rectText = new TextFragment("Aspose.Pdf.Rectangle Text")
            {
                Position = new Position(50f, 50f)
            };
            rectText.TextState.Font = FontRepository.FindFont("TimesNewRoman");
            rectText.TextState.FontSize = 14;
            rectText.TextState.ForegroundColor = Aspose.Pdf.Color.DarkBlue;
            page.Paragraphs.Add(rectText);

            // Text inside the ellipse
            TextFragment ellipseText = new TextFragment("Ellipse Text")
            {
                Position = new Position(300f, 50f)
            };
            ellipseText.TextState.Font = FontRepository.FindFont("Courier");
            ellipseText.TextState.FontSize = 12;
            ellipseText.TextState.ForegroundColor = Aspose.Pdf.Color.Green;
            page.Paragraphs.Add(ellipseText);

            // Additional example: text with a different font and larger size
            TextFragment extraText = new TextFragment("Extra Text")
            {
                Position = new Position(200f, 150f)
            };
            extraText.TextState.Font = FontRepository.FindFont("Helvetica");
            extraText.TextState.FontSize = 18;
            extraText.TextState.ForegroundColor = Aspose.Pdf.Color.Red;
            page.Paragraphs.Add(extraText);

            // ------------------------------------------------------------
            // Save the PDF – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
            // ------------------------------------------------------------
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF saved without rendering Graph.");
                }
            }
        }

        Console.WriteLine("PDF creation process finished.");
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
