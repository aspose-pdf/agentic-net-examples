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
        // ------------------------------------------------------------
        // 1. Prepare a source PDF as a byte array.
        //    In a real scenario you would obtain this array from a
        //    database, a web service, etc.  For this self‑contained
        //    example we create a minimal PDF in memory and serialize
        //    it to a byte[].
        // ------------------------------------------------------------
        byte[] pdfBytes;
        using (var initStream = new MemoryStream())
        {
            // Create a simple PDF with a single blank page.
            using (var initDoc = new Document())
            {
                initDoc.Pages.Add();
                initDoc.Save(initStream);
            }
            pdfBytes = initStream.ToArray();
        }

        // ------------------------------------------------------------
        // 2. Deserialize the byte array back into a Document instance.
        //    The MemoryStream must be positioned at the beginning.
        // ------------------------------------------------------------
        using (var ms = new MemoryStream(pdfBytes))
        using (var doc = new Document(ms))
        {
            // Ensure there is at least one page to work with.
            Page page = doc.Pages.Count > 0 ? doc.Pages[1] : doc.Pages.Add();

            // --------------------------------------------------------
            // 3. Create a new Graph container (width: 400, height: 200).
            // --------------------------------------------------------
            Graph graph = new Graph(400.0, 200.0);

            // ----- Add a rectangle shape -----
            // Rectangle constructor: (left, bottom, width, height)
            var rect = new Aspose.Pdf.Drawing.Rectangle(50f, 50f, 100f, 50f)
            {
                GraphInfo = new GraphInfo
                {
                    FillColor = Color.LightGray,
                    Color = Color.Black,
                    LineWidth = 2f
                }
            };
            graph.Shapes.Add(rect);

            // ----- Add a line shape -----
            // Line expects a float array: { x1, y1, x2, y2 }.
            var line = new Line(new float[] { 50f, 150f, 200f, 150f })
            {
                GraphInfo = new GraphInfo
                {
                    Color = Color.Red,
                    LineWidth = 1.5f
                }
            };
            graph.Shapes.Add(line);

            // Attach the graph to the page.
            page.Paragraphs.Add(graph);

            // ----- Add text labels for the shapes -----
            var rectLabel = new TextFragment("Aspose.Pdf.Rectangle")
            {
                Position = new Position(55, 45), // Slightly below the rectangle.
                TextState =
                {
                    FontSize = 12,
                    Font = FontRepository.FindFont("Helvetica"),
                    ForegroundColor = Color.Blue
                }
            };
            page.Paragraphs.Add(rectLabel);

            var lineLabel = new TextFragment("Line")
            {
                Position = new Position(210, 155),
                TextState =
                {
                    FontSize = 12,
                    Font = FontRepository.FindFont("Helvetica"),
                    ForegroundColor = Color.Green
                }
            };
            page.Paragraphs.Add(lineLabel);

            // --------------------------------------------------------
            // 4. Save the modified PDF to disk (or to another stream).
            //    Guard the Save call on non‑Windows platforms where libgdiplus
            //    (GDI+) may be missing.
            // --------------------------------------------------------
            const string outputPath = "output.pdf";
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
                    Console.WriteLine($"PDF saved to '{outputPath}' (non‑Windows platform).");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF was generated without rendering the Graph.");
                }
            }
        }
    }

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
