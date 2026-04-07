using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Facades;

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
            // Load the PDF document (load rule)
            using (Document doc = new Document(inputPath))
            {
                // Example shape coordinates (double for calculations)
                double llx = 100;
                double lly = 500;
                double urx = 300;
                double ury = 600;

                // Create a rectangle shape – constructor expects float values
                var rectShape = new Aspose.Pdf.Drawing.Rectangle(
                    (float)llx,
                    (float)lly,
                    (float)(urx - llx),
                    (float)(ury - lly));
                rectShape.GraphInfo = new Aspose.Pdf.GraphInfo
                {
                    FillColor = Aspose.Pdf.Color.LightGray,
                    Color = Aspose.Pdf.Color.Black,
                    LineWidth = 2f // float literal
                };

                // Get the first page (1‑based indexing)
                Page page = doc.Pages[1];
                double pageWidth = page.PageInfo.Width;
                double pageHeight = page.PageInfo.Height;

                try
                {
                    // Explicit bounds check; throws if shape does not fit
                    bool fits = rectShape.CheckBounds(pageWidth, pageHeight);
                    if (!fits)
                    {
                        throw new Aspose.Pdf.BoundsOutOfRangeException(
                            "Shape does not fit within page bounds.", pageWidth, pageHeight);
                    }

                    // Add the shape via a Graph container (creation rule)
                    // Graph constructor accepts double values for page size
                    Aspose.Pdf.Drawing.Graph graph = new Aspose.Pdf.Drawing.Graph(pageWidth, pageHeight);
                    graph.Shapes.Add(rectShape);
                    page.Paragraphs.Add(graph);
                }
                catch (Aspose.Pdf.BoundsOutOfRangeException ex)
                {
                    // Log detailed coordinates when bounds checking fails
                    Console.Error.WriteLine($"BoundsOutOfRangeException: {ex.Message}");
                    Console.Error.WriteLine($"Shape coordinates: LLX={llx}, LLY={lly}, URX={urx}, URY={ury}");
                    Console.Error.WriteLine($"Page dimensions: Width={pageWidth}, Height={pageHeight}");
                }

                // Save the modified PDF (save rule)
                doc.Save(outputPath);
                Console.WriteLine($"Document saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
