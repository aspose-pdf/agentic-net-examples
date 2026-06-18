using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a Graph container that covers the whole page (use double literals as required)
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // ----- Shadow rectangle (slightly offset, semi‑transparent) -----
            // Position: 10 units right and 10 units up from the main rectangle
            var shadowRect = new Aspose.Pdf.Drawing.Rectangle(
                110f, // X (float)
                510f, // Y (float)
                200f, // Width (float)
                100f  // Height (float)
            );
            shadowRect.GraphInfo = new GraphInfo
            {
                // Light gray fill with 50% opacity (alpha = 128)
                FillColor = Aspose.Pdf.Color.FromArgb(128, 211, 211, 211), // 50% LightGray
                // No border color (transparent)
                Color = Aspose.Pdf.Color.Transparent,
                LineWidth = 0f
            };
            graph.Shapes.Add(shadowRect);

            // ----- Main rectangle (on top of the shadow) -----
            var mainRect = new Aspose.Pdf.Drawing.Rectangle(
                100f, // X
                500f, // Y
                200f, // Width
                100f  // Height
            );
            mainRect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.Blue,      // solid fill
                Color = Aspose.Pdf.Color.Black,         // black border
                LineWidth = 1f                           // border thickness
            };
            graph.Shapes.Add(mainRect);

            // Add the Graph (containing both rectangles) to the page
            page.Paragraphs.Add(graph);

            // Save the PDF – guard against missing libgdiplus on non‑Windows platforms
            string outputPath = "RectangleWithShadow.pdf";
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
                    Console.WriteLine("Warning: libgdiplus (GDI+) is not available on this platform. PDF was not saved.");
                }
            }
        }

        Console.WriteLine("PDF with rectangle and shadow created successfully.");
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
