using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class BatchGraphInserter
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = "InputPdfs";
        // Folder where modified PDFs will be saved
        const string outputFolder = "OutputPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Dimensions for the graph container (points) – Graph ctor expects double
        const double graphWidth = 400;
        const double graphHeight = 200;

        // Dimensions and position for the company‑logo rectangle inside the graph
        const double logoLeft   = 10;
        const double logoBottom = 10;
        const double logoWidth  = 80;
        const double logoHeight = 40;

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Fully‑qualify System.IO.Path to avoid ambiguity with Aspose.Pdf.Drawing.Path
            string fileName = System.IO.Path.GetFileName(pdfPath);
            string outPath  = System.IO.Path.Combine(outputFolder, fileName);

            try
            {
                // Load the PDF (lifecycle rule: use Document constructor with path)
                using (Document doc = new Document(pdfPath))
                {
                    // For this example we add the graph to the first page.
                    // Adjust the page index as needed.
                    Page page = doc.Pages[1];

                    // Create a Graph container that will hold vector shapes.
                    Graph graph = new Graph(graphWidth, graphHeight);

                    // ----- Background rectangle representing the graph area -----
                    // Rectangle ctor expects float values → cast from double
                    var background = new Aspose.Pdf.Drawing.Rectangle(
                        (float)0,
                        (float)0,
                        (float)graphWidth,
                        (float)graphHeight);
                    background.GraphInfo = new GraphInfo
                    {
                        FillColor = Aspose.Pdf.Color.LightGray,
                        Color     = Aspose.Pdf.Color.Black,
                        LineWidth = 1f // float literal
                    };
                    graph.Shapes.Add(background);

                    // ----- Company logo rectangle (predefined) -----
                    var logo = new Aspose.Pdf.Drawing.Rectangle(
                        (float)logoLeft,
                        (float)logoBottom,
                        (float)logoWidth,
                        (float)logoHeight);
                    logo.GraphInfo = new GraphInfo
                    {
                        FillColor = Aspose.Pdf.Color.Blue,
                        Color     = Aspose.Pdf.Color.White,
                        LineWidth = 1f
                    };
                    graph.Shapes.Add(logo);

                    // ----- Example line to illustrate a simple graph -----
                    float[] linePoints = { 0f, (float)graphHeight, (float)graphWidth, 0f };
                    var line = new Line(linePoints);
                    line.GraphInfo = new GraphInfo
                    {
                        Color     = Aspose.Pdf.Color.Red,
                        LineWidth = 2f
                    };
                    graph.Shapes.Add(line);

                    // Add the completed graph to the page.
                    // The graph is added as a paragraph; its position defaults to the lower‑left corner.
                    page.Paragraphs.Add(graph);

                    // Save the modified PDF (lifecycle rule: use Document.Save with path)
                    doc.Save(outPath);
                }

                Console.WriteLine($"Processed: {fileName}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing {fileName}: {ex.Message}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}
