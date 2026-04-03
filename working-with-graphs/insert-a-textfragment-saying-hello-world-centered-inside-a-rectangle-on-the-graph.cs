using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths
        const string outputPath = "output.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page
            Page page = doc.Pages.Add();

            // Define the rectangle area (left, bottom, width, height)
            // Example: rectangle at (100, 400) with size 200x100
            double rectLeft = 100;
            double rectBottom = 400;
            double rectWidth = 200;
            double rectHeight = 100;

            // Create a Graph container (size can be larger than the rectangle)
            // Use double literals for the Graph constructor as required by Aspose.Pdf
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Create the rectangle shape (Aspose.Pdf.Drawing.Rectangle expects float values)
            var shapeRect = new Aspose.Pdf.Drawing.Rectangle(
                (float)rectLeft,
                (float)rectBottom,
                (float)rectWidth,
                (float)rectHeight);

            // Set visual properties via GraphInfo
            shapeRect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 1f // float literal
            };

            // Add the rectangle to the graph
            graph.Shapes.Add(shapeRect);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Create a TextFragment with the desired text
            TextFragment tf = new TextFragment("Hello World");

            // Set font and size (optional styling)
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.FontSize = 12;
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Center the text horizontally within the rectangle
            tf.HorizontalAlignment = HorizontalAlignment.Center;

            // Position the baseline roughly at the vertical center of the rectangle
            tf.Position = new Position(
                rectLeft + rectWidth / 2,          // X coordinate (center)
                rectBottom + rectHeight / 2);      // Y coordinate (center)

            // Append the text fragment to the page (it will appear over the graph)
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(tf);

            // Save the document – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
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
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF could not be saved with graphical content.");
                }
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
