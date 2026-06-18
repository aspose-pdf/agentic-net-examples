using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "rectangle_opacity.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a rectangle shape (left, bottom, width, height)
            Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(100, 500, 200, 100);

            // Define a semi‑transparent red fill color using ARGB (alpha = 128 → 50% opacity)
            Aspose.Pdf.Color fillColor = Aspose.Pdf.Color.FromArgb(128, 255, 0, 0);

            // Apply visual properties via GraphInfo
            rectShape.GraphInfo = new GraphInfo
            {
                FillColor = fillColor,               // 50 % opaque fill
                Color = Aspose.Pdf.Color.Black,      // stroke color
                LineWidth = 1
            };

            // Add the rectangle to a Graph container and then to the page
            Aspose.Pdf.Drawing.Graph graph = new Aspose.Pdf.Drawing.Graph(400, 200);
            graph.Shapes.Add(rectShape);
            page.Paragraphs.Add(graph);

            // Verify the fill color (its ARGB representation)
            Console.WriteLine($"Rectangle fill color (ARGB): {fillColor}");

            // Save the document – guard against missing GDI+ on non‑Windows platforms
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
                    Console.WriteLine($"PDF saved to '{outputPath}'.");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("GDI+ (libgdiplus) is not available on this platform; PDF not saved.");
                }
            }
        }
    }

    // Helper to detect a nested DllNotFoundException (e.g., missing libgdiplus)
    static bool ContainsDllNotFound(Exception ex)
    {
        while (ex != null)
        {
            if (ex is DllNotFoundException) return true;
            ex = ex.InnerException;
        }
        return false;
    }
}