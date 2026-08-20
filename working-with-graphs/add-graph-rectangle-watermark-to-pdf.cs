using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Use the executable's folder as a reliable base directory
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string inputDir = System.IO.Path.Combine(baseDir, "input_pdfs");
        string outputDir = System.IO.Path.Combine(baseDir, "output_pdfs");

        // Ensure the folders exist so the program never throws DirectoryNotFoundException
        Directory.CreateDirectory(inputDir);
        Directory.CreateDirectory(outputDir);

        // Get all PDF files – if none are found we simply exit with a friendly message
        string[] pdfFiles = Directory.GetFiles(inputDir, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine($"No PDF files found in '{inputDir}'. Place PDFs there and rerun the program.");
            return;
        }

        foreach (string inputPath in pdfFiles)
        {
            string fileName = System.IO.Path.GetFileName(inputPath);
            string outputPath = System.IO.Path.Combine(outputDir, fileName);

            try
            {
                // Load the PDF document (lifecycle rule: use using)
                using (Document doc = new Document(inputPath))
                {
                    // Process each page in the document
                    foreach (Page page in doc.Pages)
                    {
                        // Create a Graph container sized to the current page
                        double pageWidth = page.PageInfo.Width;
                        double pageHeight = page.PageInfo.Height;
                        Graph graph = new Graph(pageWidth, pageHeight);

                        // Define a rectangle shape that will act as a watermark
                        // Position: lower‑left corner (50,50), size: 200x100 points
                        Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(50, 50, 200, 100);
                        rectShape.GraphInfo = new GraphInfo
                        {
                            FillColor = Color.LightGray, // Background of the rectangle
                            Color = Color.Black,         // Border color
                            LineWidth = 2                // Border thickness
                        };

                        // Add the rectangle to the graph
                        graph.Shapes.Add(rectShape);

                        // Add the graph (containing the rectangle) to the page
                        page.Paragraphs.Add(graph);
                    }

                    // Save the modified document (lifecycle rule: use Save)
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed '{fileName}' → '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{fileName}': {ex.Message}");
            }
        }
    }
}
