using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Folder containing the source PDFs
        const string inputFolder = "InputPdfs";
        // Folder where modified PDFs will be written
        const string outputFolder = "OutputPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Process every PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Disambiguate System.IO.Path calls (Aspose.Pdf.Drawing also defines a Path class)
            string fileName = System.IO.Path.GetFileName(pdfPath);
            string outPath = System.IO.Path.Combine(outputFolder, fileName);

            try
            {
                // Load the PDF document (lifecycle rule: use using)
                using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(pdfPath))
                {
                    // Add the predefined graph to each page
                    foreach (Aspose.Pdf.Page page in doc.Pages)
                    {
                        // Create a Graph container (width, height) – use double values as required
                        Aspose.Pdf.Drawing.Graph graph = new Aspose.Pdf.Drawing.Graph(200.0, 100.0);

                        // ----- Company logo rectangle -----
                        // Rectangle constructor: (left, bottom, width, height)
                        Aspose.Pdf.Drawing.Rectangle logoRect = new Aspose.Pdf.Drawing.Rectangle(0, 0, 80, 40);
                        logoRect.GraphInfo = new Aspose.Pdf.GraphInfo
                        {
                            FillColor = Aspose.Pdf.Color.LightGray,
                            Color = Aspose.Pdf.Color.Black,
                            LineWidth = 1
                        };
                        graph.Shapes.Add(logoRect);

                        // ----- Example additional shape (a line) -----
                        // Line constructor expects a float array: { x1, y1, x2, y2 }
                        float[] linePoints = { 0, 40, 200, 40 };
                        Aspose.Pdf.Drawing.Line line = new Aspose.Pdf.Drawing.Line(linePoints);
                        line.GraphInfo = new Aspose.Pdf.GraphInfo
                        {
                            Color = Aspose.Pdf.Color.Blue,
                            LineWidth = 2
                        };
                        graph.Shapes.Add(line);

                        // Position the graph on the page.
                        // Adding the graph to the page's Paragraphs collection places it at the default location.
                        // For explicit positioning, set the graph's Matrix (translation) if needed.
                        // Here we simply add it; adjust coordinates as required for your layout.
                        page.Paragraphs.Add(graph);
                    }

                    // Save the modified PDF (lifecycle rule: use Save with path)
                    doc.Save(outPath);
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
