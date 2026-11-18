using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Directory containing source PDFs
        const string sourceDir = @"C:\PdfSource";
        // Directory where modified PDFs will be saved
        const string outputDir = @"C:\PdfOutput";

        if (!Directory.Exists(sourceDir))
        {
            Console.Error.WriteLine($"Source directory not found: {sourceDir}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Iterate over all PDF files in the source directory
        foreach (string pdfPath in Directory.GetFiles(sourceDir, "*.pdf"))
        {
            // Fully‑qualify System.IO.Path to avoid ambiguity with Aspose.Pdf.Drawing.Path
            string fileName = System.IO.Path.GetFileName(pdfPath);
            string outPath  = System.IO.Path.Combine(outputDir, fileName);

            // Load each PDF inside a using block (document‑disposal‑with‑using rule)
            using (Document doc = new Document(pdfPath))
            {
                // Ensure the document has at least one page
                if (doc.Pages.Count == 0)
                {
                    doc.Pages.Add();
                }

                // Use the first page (1‑based indexing rule)
                Page page = doc.Pages[1];

                // -------------------------------------------------
                // Insert an identical graph (chart) on the page
                // -------------------------------------------------
                // Width and height are expressed in points (double).
                const double graphWidth  = 300.0;
                const double graphHeight = 200.0;
                Graph graph = new Graph(graphWidth, graphHeight);

                // Example: a light‑gray filled rectangle representing a bar/area of the chart
                Aspose.Pdf.Drawing.Rectangle chartRect = new Aspose.Pdf.Drawing.Rectangle(20f, 20f, 120f, 150f);
                chartRect.GraphInfo.FillColor = Color.FromRgb(0.95, 0.95, 0.95); // light background
                chartRect.GraphInfo.Color     = Color.Black;                 // border colour
                chartRect.GraphInfo.LineWidth = 1f;
                graph.Shapes.Add(chartRect);

                // Position the graph on the page – use Graph's Left/Top properties.
                graph.Left = 100f;   // distance from the left edge of the page
                graph.Top  = 400f;   // distance from the bottom edge of the page

                page.Paragraphs.Add(graph);

                // -------------------------------------------------
                // Insert a watermark rectangle over the same area
                // -------------------------------------------------
                Graph watermarkGraph = new Graph(graphWidth, graphHeight);
                Aspose.Pdf.Drawing.Rectangle watermarkRect = new Aspose.Pdf.Drawing.Rectangle(20f, 20f, 120f, 150f);
                watermarkRect.GraphInfo.FillColor = Color.Transparent; // no fill
                // Semi‑transparent reddish border (30 % opacity → alpha ≈ 178)
                watermarkRect.GraphInfo.Color = Color.FromArgb(178, 204, 0, 0);
                watermarkRect.GraphInfo.LineWidth = 2f;
                watermarkGraph.Shapes.Add(watermarkRect);
                watermarkGraph.Left = 100f;
                watermarkGraph.Top  = 400f;

                page.Paragraphs.Add(watermarkGraph);

                // -------------------------------------------------
                // Save the modified PDF (document‑disposal‑with‑using rule)
                // -------------------------------------------------
                doc.Save(outPath);
            }

            Console.WriteLine($"Processed and saved: {outPath}");
        }
    }
}
