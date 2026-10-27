using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class PdfGenerator
{
    public static byte[] CreatePdfWithGraph()
    {
        // MemoryStream will hold the PDF in memory
        using (MemoryStream ms = new MemoryStream())
        {
            // Create a new PDF document
            using (Document doc = new Document())
            {
                // Add a blank page (first page)
                Page page = doc.Pages.Add();

                // Create a graph container of size 400x300 points
                Graph graph = new Graph(400.0, 300.0);

                // Sample data points for a simple line graph
                double[] xValues = new double[] { 10, 30, 50, 70, 90 };
                double[] yValues = new double[] { 20, 40, 60, 80, 100 };

                // Convert the double values to the coordinate system used by the Graph (points)
                // The Graph's origin (0,0) is at the lower‑left corner of the container.
                // We'll simply map the values directly because they already fit within the 0‑100 range.
                for (int i = 0; i < xValues.Length - 1; i++)
                {
                    // Create a line segment between two consecutive points
                    Line line = new Line(new float[]
                    {
                        (float)xValues[i], (float)yValues[i],
                        (float)xValues[i + 1], (float)yValues[i + 1]
                    });

                    // Set visual appearance of the line
                    line.GraphInfo = new GraphInfo
                    {
                        Color = Color.FromRgb(0.2, 0.5, 0.8), // blue‑ish colour
                        LineWidth = 2
                    };

                    // Add the line to the graph
                    graph.Shapes.Add(line);
                }

                // Optionally, add axes as simple lines for visual reference
                // X‑axis
                Line xAxis = new Line(new float[] { 0, 0, 100, 0 });
                xAxis.GraphInfo = new GraphInfo { Color = Color.Black, LineWidth = 1 };
                graph.Shapes.Add(xAxis);
                // Y‑axis
                Line yAxis = new Line(new float[] { 0, 0, 0, 100 });
                yAxis.GraphInfo = new GraphInfo { Color = Color.Black, LineWidth = 1 };
                graph.Shapes.Add(yAxis);

                // Add the graph to the page
                page.Paragraphs.Add(graph);

                // Save the PDF into the memory stream
                doc.Save(ms);
            }

            // Return the PDF as a byte array
            return ms.ToArray();
        }
    }
}

class Program
{
    static void Main()
    {
        byte[] pdfBytes = PdfGenerator.CreatePdfWithGraph();
        Console.WriteLine($"Generated PDF size: {pdfBytes.Length} bytes");
        // Uncomment the line below to write the PDF to disk for manual inspection
        // File.WriteAllBytes("graph.pdf", pdfBytes);
    }
}
