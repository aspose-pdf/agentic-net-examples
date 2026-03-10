using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Input PCL file and output PDF file paths
        const string pclPath   = "input.pcl";
        const string pdfPath   = "output.pdf";

        // Verify the source file exists
        if (!File.Exists(pclPath))
        {
            Console.Error.WriteLine($"File not found: {pclPath}");
            return;
        }

        // Load the PCL file into a PDF document using the correct load options
        using (Document doc = new Document(pclPath, new PclLoadOptions()))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create a Graph container – this is the proper way to draw vector shapes
            // Width and height define the drawing area inside the page
            Graph graph = new Graph(400, 200);

            // ---------- Rectangle ----------
            // Constructor: left, bottom, width, height
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(50, 150, 200, 100);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color     = Color.Black,
                LineWidth = 2
            };
            graph.Shapes.Add(rect);

            // ---------- Ellipse ----------
            // Constructor: left, bottom, width, height
            Ellipse ellipse = new Ellipse(250, 150, 100, 80);
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = Color.Yellow,
                Color     = Color.Red,
                LineWidth = 1.5f
            };
            graph.Shapes.Add(ellipse);

            // ---------- Line ----------
            // The Line constructor expects a float array: { x1, y1, x2, y2 }
            float[] linePoints = { 100, 300, 300, 300 };
            Line line = new Line(linePoints);
            line.GraphInfo = new GraphInfo
            {
                Color     = Color.Blue,
                LineWidth = 2
            };
            graph.Shapes.Add(line);

            // Add the completed graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the modified document as a PDF file
            doc.Save(pdfPath);
        }

        Console.WriteLine($"PCL converted to PDF with shapes added: '{pdfPath}'.");
    }
}