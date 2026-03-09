using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph.pdf";

        // Desired dimensions for the graph (in points)
        double graphWidth  = 400;
        double graphHeight = 200;

        // Document lifecycle must be wrapped in a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page to host the graph
            Page page = doc.Pages.Add();

            // On macOS the Graph class requires native GDI+ (libgdiplus).
            // If libgdiplus is not present the runtime throws a TypeInitializationException.
            // Provide a safe fallback: skip graph rendering and still save the PDF.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Console.WriteLine("libgdiplus is required for Graph rendering on macOS; saving PDF without graph.");
                doc.Save(outputPath);
                return;
            }

            // Instantiate the drawing object (Graph) with the given width and height
            Graph graph = new Graph(graphWidth, graphHeight);

            // Add the graph to the page's content
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with graph saved to '{outputPath}'.");
    }
}
