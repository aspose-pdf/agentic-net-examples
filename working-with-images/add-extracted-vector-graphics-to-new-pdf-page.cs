using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document srcDoc = new Document(inputPath))
        {
            // ------------------------------------------------------------
            // Extract vector graphics from the first page (as an example)
            // ------------------------------------------------------------
            // The correct absorber class is GraphicsAbsorber (in Aspose.Pdf.Vector)
            GraphicsAbsorber absorber = new GraphicsAbsorber();
            absorber.Visit(srcDoc.Pages[1]); // search vector graphics on page 1

            // The absorber returns a collection of Graphic objects which derive from Shape.
            // Cast them to Shape so they can be added to a Graph.
            var extractedGraphics = absorber.Elements
                                            .Cast<Shape>()
                                            .ToList();

            // ------------------------------------------------------------
            // Create a new page and add the extracted graphics using Add (loop)
            // ------------------------------------------------------------
            Page newPage = srcDoc.Pages.Add(); // adds an empty page at the end

            // Graph container – size matches the page size (width, height are double)
            var graph = new Graph(newPage.PageInfo.Width, newPage.PageInfo.Height);

            // Add each graphical object to the graph's Shapes collection.
            foreach (var shape in extractedGraphics)
            {
                graph.Shapes.Add(shape);
            }

            // Add the Graph to the page's paragraphs collection
            newPage.Paragraphs.Add(graph);

            // ------------------------------------------------------------
            // Save the modified document
            // ------------------------------------------------------------
            srcDoc.Save(outputPath);
            Console.WriteLine($"New PDF with added graphics saved to '{outputPath}'.");
        }
    }
}
