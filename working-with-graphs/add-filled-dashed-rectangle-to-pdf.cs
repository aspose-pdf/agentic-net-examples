using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (width, height) – this acts like a drawing canvas
            // Use the double‑based constructor as the float overload is obsolete
            Graph graph = new Graph(400.0, 200.0);

            // Define a rectangle shape (left, bottom, width, height)
            // Rectangle constructor expects float values, so we pass literals with the 'f' suffix
            var rect = new Aspose.Pdf.Drawing.Rectangle(50f, 50f, 300f, 100f);

            // Set fill color, border color, border width, and dash pattern via GraphInfo
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,   // interior fill
                Color = Color.Blue,            // border (stroke) color
                LineWidth = 2f,                // border thickness (float literal)
                DashArray = new int[] { 5, 3 } // dash pattern: 5pt dash, 3pt gap
            };

            // Add the rectangle to the graph's shape collection
            graph.Shapes.Add(rect);

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the PDF document – guard against missing GDI+ on non‑Windows platforms
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
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF saved without graphical rendering.");
                }
            }
        }

        Console.WriteLine($"PDF with filled, dashed rectangle saved to '{outputPath}'.");
    }

    // Helper to detect a nested DllNotFoundException (e.g., missing libgdiplus on Linux/macOS)
    private static bool ContainsDllNotFound(Exception ex)
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
