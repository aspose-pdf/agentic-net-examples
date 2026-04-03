using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Facades; // Included as per requirement

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        string inputPath = "input.pdf";
        string outputPath = "output.pdf";

        // User setting: true = enforce bounds, false = ignore bounds
        bool enforceBounds = true;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Ensure the document has at least one page
            Aspose.Pdf.Page page = doc.Pages[1];

            // Create a Graph with specified width and height (points)
            Aspose.Pdf.Drawing.Graph graph = new Aspose.Pdf.Drawing.Graph(400.0, 200.0);

            // Toggle the BoundsCheckMode on the Shapes collection of the graph
            if (enforceBounds)
            {
                // Throw an exception if a shape does not fit within the graph bounds
                graph.Shapes.UpdateBoundsCheckMode(Aspose.Pdf.BoundsCheckMode.ThrowExceptionIfDoesNotFit);
            }
            else
            {
                // Default behavior: no bounds checking
                graph.Shapes.UpdateBoundsCheckMode(Aspose.Pdf.BoundsCheckMode.Default);
            }

            // Add a rectangle shape that fits inside the graph
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(0, 0, 100, 50);
            rect.GraphInfo = new Aspose.Pdf.GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 1f
            };
            graph.Shapes.Add(rect);

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the document.
            // On non‑Windows platforms, Graph rendering may require libgdiplus.
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
                    Console.WriteLine("GDI+ (libgdiplus) not available; saved PDF without graph rendering.");
                }
            }
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }

    // Helper to detect nested DllNotFoundException
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