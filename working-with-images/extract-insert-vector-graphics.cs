using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "source.pdf";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdfPath}");
            return;
        }

        // Load the source PDF that contains the vector graphic.
        using (Document sourceDoc = new Document(sourcePdfPath))
        {
            // Select the page that holds the vector graphic (example: first page).
            Page sourcePage = sourceDoc.Pages[1];

            // Create a new PDF document to receive the extracted graphic.
            using (Document targetDoc = new Document())
            {
                // Add a blank page to the target document.
                Page targetPage = targetDoc.Pages.Add();

                // ------------------------------------------------------------
                // Extract vector graphics (Graph objects) from the source page
                // and insert each one into the target page's Paragraphs collection.
                // ------------------------------------------------------------
                var sourceGraphs = sourcePage.Paragraphs.OfType<Graph>().ToList();
                foreach (var srcGraph in sourceGraphs)
                {
                    // Clone the graph: create a new Graph with the same size and copy its shapes.
                    var clonedGraph = new Graph(srcGraph.Width, srcGraph.Height);
                    foreach (var shape in srcGraph.Shapes)
                    {
                        // Most shape objects are clone‑able via the Add method (they are reference types).
                        // Adding the same shape instance works because Aspose.Pdf treats the shape as a
                        // lightweight drawing instruction. If a deep copy is required, create a new
                        // shape of the same type and copy its properties.
                        clonedGraph.Shapes.Add(shape);
                    }
                    // Insert the cloned graph into the target page's Paragraphs collection.
                    targetPage.Paragraphs.Add(clonedGraph);
                }

                // Save the resulting PDF.
                targetDoc.Save(outputPdfPath);
                Console.WriteLine($"Extracted vector graphic saved to '{outputPdfPath}'.");
            }
        }
    }
}
