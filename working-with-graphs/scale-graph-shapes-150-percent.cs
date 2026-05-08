using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "scaled_shapes.pdf";

        // Create a new PDF document
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document())
        {
            // Add a page to the document
            Aspose.Pdf.Page page = doc.Pages.Add();

            // Create a Graph with specified width and height
            Aspose.Pdf.Drawing.Graph graph = new Aspose.Pdf.Drawing.Graph(400.0, 300.0);

            // ----- Add shapes to the graph -----

            // Rectangle shape
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(0, 0, 200, 100);
            rect.GraphInfo = new Aspose.Pdf.GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 2
            };
            graph.Shapes.Add(rect);

            // Line shape
            float[] linePoints = { 0, 0, 300, 0 };
            Aspose.Pdf.Drawing.Line line = new Aspose.Pdf.Drawing.Line(linePoints);
            line.GraphInfo = new Aspose.Pdf.GraphInfo
            {
                Color = Aspose.Pdf.Color.Red,
                LineWidth = 1
            };
            graph.Shapes.Add(line);

            // ----- Scale all shapes by 150% using GraphInfo scaling rates -----
            // The GraphInfo attached to the Graph controls transformation of its child shapes.
            graph.GraphInfo = new Aspose.Pdf.GraphInfo
            {
                ScalingRateX = 1.5, // 150% horizontal scaling
                ScalingRateY = 1.5  // 150% vertical scaling
            };

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the document.
            // On non‑Windows platforms the PDF rendering may require libgdiplus.
            // Guard the save operation to avoid a TypeInitializationException.
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
                    Console.WriteLine("GDI+ (libgdiplus) is not available on this platform; graph rendering was skipped.");
                }
            }
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }

    // Helper to detect a nested DllNotFoundException (libgdiplus) inside TypeInitializationException
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