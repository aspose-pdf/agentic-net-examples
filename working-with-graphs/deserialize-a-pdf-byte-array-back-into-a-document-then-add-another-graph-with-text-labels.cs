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
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the source PDF exists. If it does not, create a minimal one.
        if (!File.Exists(inputPath))
        {
            using (var tempDoc = new Document())
            {
                tempDoc.Pages.Add();
                tempDoc.Save(inputPath);
            }
        }

        // Read the PDF into a byte array.
        byte[] pdfBytes = File.ReadAllBytes(inputPath);

        // Deserialize the PDF byte array into a Document using a MemoryStream.
        using (MemoryStream ms = new MemoryStream(pdfBytes))
        using (Document doc = new Document(ms))
        {
            // Work with the first page (1‑based indexing).
            Page page = doc.Pages[1];

            // Add the Graph only on Windows (libgdiplus is required on non‑Windows platforms).
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // Create a Graph container to hold vector shapes.
                Graph graph = new Graph(400.0, 200.0);

                // ----- Add a rectangle shape -----
                var rectShape = new Aspose.Pdf.Drawing.Rectangle(50f, 150f, 100f, 80f);
                rectShape.GraphInfo = new GraphInfo
                {
                    FillColor = Aspose.Pdf.Color.LightGray,
                    Color = Aspose.Pdf.Color.Black,
                    LineWidth = 1f
                };
                graph.Shapes.Add(rectShape);

                // ----- Add a line shape -----
                float[] linePoints = { 200f, 150f, 300f, 250f };
                var lineShape = new Line(linePoints);
                lineShape.GraphInfo = new GraphInfo
                {
                    Color = Aspose.Pdf.Color.Red,
                    LineWidth = 2f
                };
                graph.Shapes.Add(lineShape);

                // Add the Graph to the page's content.
                page.Paragraphs.Add(graph);
            }
            else
            {
                Console.WriteLine("Running on a non‑Windows platform – Graph rendering is skipped because libgdiplus is not available.");
            }

            // ----- Add text labels for the shapes -----
            // TextFragment for the rectangle label.
            TextFragment rectLabel = new TextFragment("Aspose.Pdf.Rectangle");
            rectLabel.Position = new Position(55, 140); // Position near the rectangle.
            rectLabel.TextState.FontSize = 12;
            rectLabel.TextState.Font = FontRepository.FindFont("Helvetica");
            rectLabel.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;
            page.Paragraphs.Add(rectLabel);

            // TextFragment for the line label.
            TextFragment lineLabel = new TextFragment("Line");
            lineLabel.Position = new Position(210, 260); // Position near the line.
            lineLabel.TextState.FontSize = 12;
            lineLabel.TextState.Font = FontRepository.FindFont("Helvetica");
            lineLabel.TextState.ForegroundColor = Aspose.Pdf.Color.Green;
            page.Paragraphs.Add(lineLabel);

            // Save the modified document – guard against missing GDI+ on non‑Windows platforms.
            try
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
            {
                Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. The PDF was not saved.");
            }
        }
    }

    // Helper method to detect a nested DllNotFoundException (e.g., missing libgdiplus).
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
