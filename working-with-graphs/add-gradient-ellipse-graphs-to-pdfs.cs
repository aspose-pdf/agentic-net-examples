using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Input and output directories
        string inputDir = "InputPdfs";
        string outputDir = "OutputPdfs";

        if (!Directory.Exists(inputDir))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDir}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Get all PDF files in the input directory
        string[] pdfFiles = Directory.GetFiles(inputDir, "*.pdf");

        // Process each PDF concurrently
        Parallel.ForEach(pdfFiles, pdfPath =>
        {
            try
            {
                // Load the PDF inside a using block (deterministic disposal)
                using (Document doc = new Document(pdfPath))
                {
                    // Ensure the document has at least one page
                    if (doc.Pages.Count == 0)
                    {
                        doc.Pages.Add();
                    }

                    // Access the first page (1‑based indexing)
                    Page page = doc.Pages[1];

                    // Create a Graph container that spans the whole page
                    Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

                    // Define the ellipse (left, bottom, width, height)
                    Aspose.Pdf.Drawing.Ellipse ellipse = new Aspose.Pdf.Drawing.Ellipse(100, 300, 200, 200);

                    // Style the ellipse via GraphInfo (solid fill – gradient not supported in core API)
                    ellipse.GraphInfo.FillColor = Aspose.Pdf.Color.Blue; // solid fill (replace with gradient logic if available)
                    ellipse.GraphInfo.Color = Aspose.Pdf.Color.Black;   // border color
                    ellipse.GraphInfo.LineWidth = 2;                     // border width

                    // Add the ellipse to the graph, then the graph to the page
                    graph.Shapes.Add(ellipse);
                    page.Paragraphs.Add(graph);

                    // Save the modified PDF to the output directory
                    string fileName = System.IO.Path.GetFileName(pdfPath);
                    string outPath = System.IO.Path.Combine(outputDir, fileName);
                    doc.Save(outPath);
                }

                Console.WriteLine($"Processed: {pdfPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing {pdfPath}: {ex.Message}");
            }
        });

        Console.WriteLine("All PDFs processed.");
    }
}