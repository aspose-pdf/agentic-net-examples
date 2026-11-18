using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph.pdf";

        // Ensure the Aspose.Pdf license is set if required
        // Aspose.Pdf.License license = new Aspose.Pdf.License();
        // license.SetLicense("Aspose.Pdf.lic");

        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Create a Graph object (width:200, height:200) – use double constructor as the float one is obsolete
            Graph graph = new Graph(200.0, 200.0);
            // Optional positioning – Graph has Left/Top properties (no GraphInfo.Position)
            graph.Left = 100;   // X coordinate on the page
            graph.Top = 500;    // Y coordinate on the page

            // Build a regular hexagon (6‑sided polygon) as a closed Line shape
            float radius = 80f;          // radius of the hexagon
            float centerX = 100f;        // centre X within the graph surface
            float centerY = 100f;        // centre Y within the graph surface
            float[] points = new float[14]; // 6 vertices + repeat first vertex to close the shape
            for (int i = 0; i < 6; i++)
            {
                double angle = Math.PI / 3 * i - Math.PI / 2; // start at the top vertex
                points[i * 2] = (float)(centerX + radius * Math.Cos(angle));
                points[i * 2 + 1] = (float)(centerY + radius * Math.Sin(angle));
            }
            // Close the polygon by repeating the first point
            points[12] = points[0];
            points[13] = points[1];

            Line hexagon = new Line(points);
            // Set border (stroke) colour and thickness via GraphInfo
            hexagon.GraphInfo.Color = Aspose.Pdf.Color.Blue;   // border colour
            hexagon.GraphInfo.LineWidth = 2;                   // border thickness
            // Optional fill colour
            hexagon.GraphInfo.FillColor = Aspose.Pdf.Color.LightGray;

            // Add the hexagon to the graph and the graph to the page
            graph.Shapes.Add(hexagon);
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with a hexagonal polygon saved to '{outputPath}'.");
    }
}
