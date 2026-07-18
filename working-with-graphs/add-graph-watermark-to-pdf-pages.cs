using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Input folder containing PDF files
        const string inputFolder = @"C:\InputPdfs";
        // Output folder for processed PDFs
        const string outputFolder = @"C:\OutputPdfs";

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");

        foreach (string pdfPath in pdfFiles)
        {
            // Load each PDF inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Iterate over all pages (1‑based indexing)
                foreach (Page page in doc.Pages)
                {
                    // Create a Graph container (size can be adjusted as needed)
                    // Use double literals as the Graph constructor now expects double values
                    Graph graph = new Graph(400.0, 200.0);

                    // Define a rectangle shape that will act as the watermark
                    // Constructor: left, bottom, width, height (float parameters)
                    Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 200f, 100f);
                    // Set visual properties via GraphInfo
                    rectShape.GraphInfo = new GraphInfo
                    {
                        FillColor = Color.LightGray,   // background of the rectangle
                        Color = Color.Black,           // border color
                        LineWidth = 2f                 // border thickness (float)
                    };

                    // Add the rectangle to the graph
                    graph.Shapes.Add(rectShape);

                    // Add the graph (containing the rectangle) to the page's content
                    page.Paragraphs.Add(graph);
                }

                // Build output file path (same name, different folder)
                string outputPath = System.IO.Path.Combine(outputFolder, System.IO.Path.GetFileName(pdfPath));

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Processed and saved: {System.IO.Path.GetFileName(pdfPath)}");
        }
    }
}
