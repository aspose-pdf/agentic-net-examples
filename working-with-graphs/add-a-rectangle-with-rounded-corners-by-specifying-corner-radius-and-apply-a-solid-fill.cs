using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF classes
using Aspose.Pdf.Drawing;            // Graph and shape classes

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // Existing PDF or will be created if not found
        const string outputPath = "output.pdf";

        // Ensure the input file exists; if not, create a blank PDF with one page
        if (!File.Exists(inputPath))
        {
            using (Document blank = new Document())
            {
                blank.Pages.Add(); // add a single empty page
                blank.Save(inputPath);
            }
        }

        // Load the PDF, add a rounded‑corner rectangle, and save
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Create a Graph container (size is not critical when adding to page)
            Aspose.Pdf.Drawing.Graph graph = new Aspose.Pdf.Drawing.Graph(500, 500);

            // Define a rectangle shape: left, bottom, width, height
            Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(100, 400, 200, 100);

            // Set visual properties via GraphInfo
            rectShape.GraphInfo = new Aspose.Pdf.GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,   // solid fill
                Color     = Aspose.Pdf.Color.Black,      // border color
                LineWidth = 1.5f
            };

            // Rounded corners: radius in points
            rectShape.RoundedCornerRadius = 15; // adjust as needed

            // Add the rectangle to the graph
            graph.Shapes.Add(rectShape);

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rounded‑corner rectangle added and saved to '{outputPath}'.");
    }
}