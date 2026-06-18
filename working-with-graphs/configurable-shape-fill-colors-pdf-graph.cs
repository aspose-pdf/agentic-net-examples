using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    // Helper to convert a color name (e.g., "LightGray") to Aspose.Pdf.Color
    static Color GetColorByName(string name)
    {
        // Try to get a static property with the given name from Aspose.Pdf.Color
        var prop = typeof(Color).GetProperty(name);
        if (prop != null && prop.PropertyType == typeof(Color))
        {
            // The property value is never null for the built‑in colors
            return (Color)prop.GetValue(null)!;
        }

        // Fallback to Black if the name is not recognized
        return Color.Black;
    }

    static void Main()
    {
        const string configPath = "shapesConfig.json";
        const string outputPdf = "output.pdf";

        // Load shape fill color configuration (JSON format: {"Rectangle":"LightGray","Ellipse":"Yellow"})
        Dictionary<string, string> fillConfig = File.Exists(configPath)
            ? JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(configPath)) ?? new Dictionary<string, string>()
            : new Dictionary<string, string>();

        // Create and process the PDF document
        using (Document doc = new Document())
        {
            // Add a page
            Page page = doc.Pages.Add();

            // Create a Graph container (width: 400, height: 200) – use double literals as required by the non‑obsolete constructor
            Graph graph = new Graph(400.0, 200.0);

            // ----- Rectangle shape -----
            // Determine fill color from config; default to LightGray if not specified
            Color rectFill = fillConfig.TryGetValue("Rectangle", out string rectColorName)
                ? GetColorByName(rectColorName)
                : Color.LightGray;

            // Create a rectangle shape (left:0, bottom:0, width:200, height:100) – rectangle expects float parameters
            Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 200f, 100f);
            rectShape.GraphInfo = new GraphInfo
            {
                FillColor = rectFill,
                Color = Color.Black,      // stroke color
                LineWidth = 1f
            };
            graph.Shapes.Add(rectShape);

            // ----- Ellipse shape -----
            // Determine fill color from config; default to Yellow if not specified
            Color ellipseFill = fillConfig.TryGetValue("Ellipse", out string ellipseColorName)
                ? GetColorByName(ellipseColorName)
                : Color.Yellow;

            // Create an ellipse shape (left:220, bottom:0, width:150, height:100)
            Ellipse ellipseShape = new Ellipse(220f, 0f, 150f, 100f);
            ellipseShape.GraphInfo = new GraphInfo
            {
                FillColor = ellipseFill,
                Color = Color.Black,      // stroke color
                LineWidth = 1f
            };
            graph.Shapes.Add(ellipseShape);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Save the PDF – guard against missing GDI+ on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPdf);
            }
            else
            {
                try
                {
                    doc.Save(outputPdf);
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF saved may be incomplete.");
                }
            }
        }

        Console.WriteLine($"PDF saved to '{outputPdf}'.");
    }

    // Helper to detect a nested DllNotFoundException (e.g., missing libgdiplus on Linux/macOS)
    private static bool ContainsDllNotFound(Exception ex)
    {
        Exception? current = ex;
        while (current != null)
        {
            if (current is DllNotFoundException)
                return true;
            current = current.InnerException;
        }
        return false;
    }
}
