using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing; // drawing shapes

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Document must be disposed via using
            using (Document doc = new Document(inputPath))
            {
                // Ensure at least one page exists (pages are 1‑based)
                if (doc.Pages.Count == 0)
                {
                    doc.Pages.Add();
                }

                Page page = doc.Pages[1];

                // Define rectangle bounds (lower‑left and upper‑right)
                double llx = 100; // lower‑left X
                double lly = 500; // lower‑left Y
                double urx = 300; // upper‑right X
                double ury = 700; // upper‑right Y

                // Create a Graph container that will hold the shape
                Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

                try
                {
                    // Aspose.Pdf.Drawing.Rectangle expects (left, bottom, width, height) as float
                    float left = (float)llx;
                    float bottom = (float)lly;
                    float width = (float)(urx - llx);
                    float height = (float)(ury - lly);

                    Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(left, bottom, width, height);
                    // Styling via GraphInfo
                    rectShape.GraphInfo.FillColor = Aspose.Pdf.Color.FromRgb(0.2f, 0.6f, 0.8f);
                    rectShape.GraphInfo.Color = Aspose.Pdf.Color.Black; // stroke colour
                    rectShape.GraphInfo.LineWidth = 2f;

                    // Add the shape to the graph, then the graph to the page
                    graph.Shapes.Add(rectShape);
                    page.Paragraphs.Add(graph);
                }
                catch (Exception ex) when (ex is ArgumentOutOfRangeException || ex is IndexOutOfRangeException)
                {
                    // Log detailed coordinate information when bounds checking fails
                    Console.Error.WriteLine($"Error adding rectangle shape. Bounds: llx={llx}, lly={lly}, urx={urx}, ury={ury}");
                    Console.Error.WriteLine($"Exception: {ex.GetType().Name} - {ex.Message}");
                    // Optionally rethrow or continue
                }

                // Save the modified document
                doc.Save(outputPath);
            }

            Console.WriteLine($"Document saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.GetType().Name} - {ex.Message}");
        }
    }
}
