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
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (width: 400pt, height: 300pt)
            Graph graph = new Graph(400.0, 300.0);
            page.Paragraphs.Add(graph);

            // Define the original ellipse (left, bottom, width, height)
            double left   = 100.0;
            double bottom = 100.0;
            double width  = 200.0;
            double height = 100.0;
            var ellipse = new Aspose.Pdf.Drawing.Ellipse(left, bottom, width, height);

            // Create a rotation matrix for 45 degrees (π/4 radians)
            Matrix rotationMatrix = Matrix.Rotation(Math.PI / 4);

            // Transform the ellipse's bounding rectangle using the matrix
            Aspose.Pdf.Rectangle originalRect = new Aspose.Pdf.Rectangle(
                left,
                bottom,
                left + width,
                bottom + height);

            Aspose.Pdf.Rectangle transformedRect = rotationMatrix.Transform(originalRect);

            // Update ellipse position and size based on the transformed rectangle
            ellipse.Left   = (float)transformedRect.LLX;
            ellipse.Bottom = (float)transformedRect.LLY;
            ellipse.Width  = (float)(transformedRect.URX - transformedRect.LLX);
            ellipse.Height = (float)(transformedRect.URY - transformedRect.LLY);

            // Set visual appearance of the ellipse
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color     = Aspose.Pdf.Color.Black,
                LineWidth = 2f
            };

            // Add the ellipse to the graph
            graph.Shapes.Add(ellipse);

            // Save the PDF (handle GDI+ requirement on non‑Windows platforms)
            string outputPath = "EllipseRotated.pdf";
            try
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
            {
                Console.WriteLine("GDI+ (gdiplus) is not available on this platform. PDF could not be saved.");
            }
        }
    }

    // Helper to detect missing native GDI+ library
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