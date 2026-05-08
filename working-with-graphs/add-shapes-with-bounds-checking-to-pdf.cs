using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page (default size A4)
            Page page = doc.Pages.Add();

            // Define the container dimensions (page size) for bounds checking
            double pageWidth = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;

            // Create a Graph container that will hold vector shapes
            // Width and height of the Graph are set to the page dimensions
            Graph graph = new Graph(pageWidth, pageHeight);

            // Example shapes – some within bounds, some deliberately out of bounds
            // Shape 1: fits inside the page
            var rectInside = new Aspose.Pdf.Drawing.Rectangle(
                (float)50,               // Left
                (float)700,              // Bottom
                (float)200,              // Width
                (float)100);             // Height
            rectInside.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.Black,
                LineWidth = 1f
            };
            TryAddShape(graph, rectInside, pageWidth, pageHeight);

            // Shape 2: exceeds the right edge of the page
            var rectOutRight = new Aspose.Pdf.Drawing.Rectangle(
                (float)(pageWidth - 50), // Left
                (float)500,              // Bottom
                (float)200,              // Width
                (float)100);             // Height
            rectOutRight.GraphInfo = new GraphInfo
            {
                FillColor = Color.Yellow,
                Color = Color.Red,
                LineWidth = 2f
            };
            TryAddShape(graph, rectOutRight, pageWidth, pageHeight);

            // Shape 3: exceeds the top edge of the page
            var rectOutTop = new Aspose.Pdf.Drawing.Rectangle(
                (float)100,               // Left
                (float)(pageHeight - 30), // Bottom
                (float)150,               // Width
                (float)100);              // Height
            rectOutTop.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightBlue,
                Color = Color.DarkBlue,
                LineWidth = 1.5f
            };
            TryAddShape(graph, rectOutTop, pageWidth, pageHeight);

            // Add the Graph (with any successfully added shapes) to the page
            page.Paragraphs.Add(graph);

            // Save the PDF document – guard against missing libgdiplus on non‑Windows platforms
            string outputPath = "output.pdf";
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
                    Console.WriteLine($"PDF saved to '{outputPath}'. (non‑Windows platform)" );
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
                }
            }
        }
    }

    // Attempts to add a shape to the graph and catches BoundsOutOfRangeException.
    // Logs the shape's coordinates when the exception occurs.
    static void TryAddShape(Graph graph, Shape shape, double containerWidth, double containerHeight)
    {
        try
        {
            // Pre‑validate bounds; if it fails, log and skip adding.
            if (!shape.CheckBounds(containerWidth, containerHeight))
            {
                LogOutOfBounds(shape, "Pre‑check failed");
                return;
            }

            // Attempt to add the shape; if it does not fit, an exception will be thrown.
            graph.Shapes.Add(shape);
        }
        catch (Aspose.Pdf.BoundsOutOfRangeException ex)
        {
            // Log the exception message and the shape's coordinates.
            LogOutOfBounds(shape, ex.Message);
        }
    }

    // Logs the coordinates of a shape that is out of bounds.
    static void LogOutOfBounds(Shape shape, string reason)
    {
        // All concrete Shape types in Aspose.Pdf.Drawing expose the same constructor
        // parameters (left, bottom, width, height) via their public properties.
        // We use reflection to retrieve them safely for any shape type.
        double left = 0, bottom = 0, width = 0, height = 0;

        var type = shape.GetType();
        var leftProp = type.GetProperty("Left");
        var bottomProp = type.GetProperty("Bottom");
        var widthProp = type.GetProperty("Width");
        var heightProp = type.GetProperty("Height");

        if (leftProp != null && leftProp.GetValue(shape) is float leftF) left = leftF;
        if (bottomProp != null && bottomProp.GetValue(shape) is float bottomF) bottom = bottomF;
        if (widthProp != null && widthProp.GetValue(shape) is float widthF) width = widthF;
        if (heightProp != null && heightProp.GetValue(shape) is float heightF) height = heightF;

        Console.WriteLine($"Shape out of bounds ({reason}): Type={type.Name}, Left={left}, Bottom={bottom}, Width={width}, Height={height}");
    }

    // Helper to detect missing libgdiplus/DLL on non‑Windows platforms.
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
