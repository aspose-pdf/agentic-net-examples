using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
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

        // Get all PDF files in the input folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            // Use fully qualified System.IO.Path to avoid ambiguity with Aspose.Pdf.Drawing.Path
            string fileName = System.IO.Path.GetFileNameWithoutExtension(pdfPath);
            string outputPath = System.IO.Path.Combine(outputFolder, $"{fileName}_logo.pdf");

            try
            {
                // Load the PDF document (1‑based page indexing)
                using (Document doc = new Document(pdfPath))
                {
                    // Insert the logo graph on every page
                    for (int i = 1; i <= doc.Pages.Count; i++)
                    {
                        Page page = doc.Pages[i];

                        // Create a Graph that covers the whole page (constructor expects double values)
                        Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

                        // Define logo rectangle size and position
                        float logoWidth = 100f;   // width in points
                        float logoHeight = 50f;   // height in points
                        float llx = 50f;          // lower‑left X
                        // page.PageInfo.Height is double, cast the result to float
                        float lly = (float)(page.PageInfo.Height - 50f - logoHeight); // lower‑left Y (top‑left corner offset)

                        // Drawing rectangle for the logo (uses Aspose.Pdf.Drawing.Rectangle)
                        Aspose.Pdf.Drawing.Rectangle logoRect = new Aspose.Pdf.Drawing.Rectangle(llx, lly, logoWidth, logoHeight);

                        // Visual styling via GraphInfo (no direct FillColor/StrokeColor properties)
                        logoRect.GraphInfo = new GraphInfo
                        {
                            FillColor = Aspose.Pdf.Color.LightGray,
                            Color = Aspose.Pdf.Color.Black,
                            LineWidth = 1f
                        };

                        // Add the rectangle shape to the graph
                        graph.Shapes.Add(logoRect);

                        // Attach the graph to the page's content
                        page.Paragraphs.Add(graph);
                    }

                    // Save the modified PDF
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {pdfPath} → {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}
