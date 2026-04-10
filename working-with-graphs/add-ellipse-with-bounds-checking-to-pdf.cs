using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Determine if we are running on Windows (GDI+ is available)
            bool isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

            // Shape parameters – keep them outside the OS‑check so they are available for logging
            double left   = 100;   // X coordinate of the lower‑left corner
            double bottom = 200;   // Y coordinate of the lower‑left corner
            double width  = 300;   // Horizontal size
            double height = 150;   // Vertical size

            if (isWindows)
            {
                // Create a Graph container that matches the page size (double literals as required)
                Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

                // Create the ellipse shape (fully qualified to avoid ambiguity)
                Aspose.Pdf.Drawing.Ellipse ellipse = new Aspose.Pdf.Drawing.Ellipse(left, bottom, width, height);
                ellipse.GraphInfo = new GraphInfo
                {
                    FillColor = Aspose.Pdf.Color.LightGray,
                    Color     = Aspose.Pdf.Color.DarkBlue,
                    LineWidth = 2f // float literal – GraphInfo expects float
                };

                // Enable bounds checking that throws if the shape does not fit the page
                try
                {
                    // Configure the collection to use the ThrowExceptionIfDoesNotFit mode
                    graph.Shapes.UpdateBoundsCheckMode(
                        BoundsCheckMode.ThrowExceptionIfDoesNotFit,
                        page.PageInfo.Width,
                        page.PageInfo.Height);

                    // Attempt to add the ellipse; may throw BoundsOutOfRangeException
                    graph.Shapes.Add(ellipse);
                }
                catch (BoundsOutOfRangeException ex)
                {
                    // Log detailed shape coordinates for troubleshooting
                    Console.Error.WriteLine($"BoundsOutOfRangeException: {ex.Message}");
                    Console.Error.WriteLine($"Ellipse parameters -> Left: {left}, Bottom: {bottom}, Width: {width}, Height: {height}");
                    // In this example we simply skip adding the shape after logging
                }

                // Add the graph (with any successfully added shapes) to the page
                page.Paragraphs.Add(graph);
            }
            else
            {
                // On non‑Windows platforms libgdiplus (GDI+) is missing – skip Graph rendering
                Console.WriteLine("Running on a non‑Windows platform; Graph rendering is skipped because GDI+ is unavailable.");
            }

            // Save the PDF document – guard against missing GDI+ on any platform
            try
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF successfully saved to '{outputPath}'.");
            }
            catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
            {
                Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. The PDF could not be saved.");
                Console.WriteLine($"Exception details: {ex.Message}");
            }
        }
    }

    // Helper that walks the inner‑exception chain looking for a DllNotFoundException
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
