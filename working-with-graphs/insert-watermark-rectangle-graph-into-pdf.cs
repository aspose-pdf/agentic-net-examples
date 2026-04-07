using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Facades; // Included as per requirement

class Program
{
    static void Main()
    {
        // Folder containing source PDF files
        const string inputFolder = "InputPdfs";
        // Folder where processed PDFs will be saved
        const string outputFolder = "OutputPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found to process.");
            return;
        }

        foreach (string pdfPath in pdfFiles)
        {
            string fileName = System.IO.Path.GetFileName(pdfPath);
            string outputPath = System.IO.Path.Combine(outputFolder, fileName);

            try
            {
                // Load the PDF document (using rule: wrap in using for deterministic disposal)
                using (Document doc = new Document(pdfPath))
                {
                    // Iterate over each page (Aspose.Pdf uses 1‑based indexing)
                    for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                    {
                        Page page = doc.Pages[pageIndex];

                        // Create a Graph that covers the whole page
                        Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

                        // Define a rectangle shape that will act as the watermark
                        // Constructor: left, bottom, width, height
                        Aspose.Pdf.Drawing.Rectangle shapeRect = new Aspose.Pdf.Drawing.Rectangle(100, 500, 200, 100);
                        shapeRect.GraphInfo = new GraphInfo
                        {
                            FillColor = Aspose.Pdf.Color.LightGray, // semi‑transparent fill
                            Color = Aspose.Pdf.Color.Black,        // border color
                            LineWidth = 2
                        };

                        // Add the rectangle to the graph
                        graph.Shapes.Add(shapeRect);

                        // Add the graph to the page's content
                        page.Paragraphs.Add(graph);
                    }

                    // Save the modified document (using rule: Save inside using block)
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed and saved: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("All files processed.");
    }
}