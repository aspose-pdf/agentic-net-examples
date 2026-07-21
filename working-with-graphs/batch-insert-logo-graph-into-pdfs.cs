using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        string inputFolder = "InputPdfs";
        // Folder where modified PDFs will be saved
        string outputFolder = "OutputPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string filePath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = System.IO.Path.GetFileName(filePath);
            string outputPath = System.IO.Path.Combine(outputFolder, fileName);

            try
            {
                // Load the PDF document
                using (Document doc = new Document(filePath))
                {
                    // Create a graph container (width: 200, height: 100 points)
                    Graph graph = new Graph(200, 100);

                    // Define a rectangle shape that will represent the company logo area
                    // Parameters: left, bottom, width, height
                    Aspose.Pdf.Drawing.Rectangle logoRect = new Aspose.Pdf.Drawing.Rectangle(10, 10, 80, 40);
                    logoRect.GraphInfo = new GraphInfo
                    {
                        FillColor = Aspose.Pdf.Color.LightGray, // Background of the logo rectangle
                        Color = Aspose.Pdf.Color.Black,        // Border color
                        LineWidth = 1                           // Border thickness
                    };
                    // Add the rectangle to the graph
                    graph.Shapes.Add(logoRect);

                    // (Optional) Add additional shapes or lines to the graph here
                    // Example: a diagonal line across the rectangle
                    // float[] linePoints = { 10, 10, 90, 50 };
                    // Line line = new Line(linePoints);
                    // line.GraphInfo = new GraphInfo { Color = Aspose.Pdf.Color.DarkBlue, LineWidth = 0.5f };
                    // graph.Shapes.Add(line);

                    // Insert the graph into the first page of the PDF
                    Page firstPage = doc.Pages[1];
                    firstPage.Paragraphs.Add(graph);

                    // Save the modified PDF to the output folder
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {fileName}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing {fileName}: {ex.Message}");
            }
        }
    }
}