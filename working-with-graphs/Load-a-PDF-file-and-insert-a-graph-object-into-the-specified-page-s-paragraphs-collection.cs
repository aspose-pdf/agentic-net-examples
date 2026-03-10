using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const int pageNumber = 1; // 1‑based page index

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Validate the requested page number
            if (pageNumber < 1 || pageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine("Invalid page number.");
                return;
            }

            // Get the target page
            Page page = doc.Pages[pageNumber];

            // Create a Graph object (width = 400 points, height = 200 points)
            Graph graph = new Graph(400, 200)
            {
                // Position the graph on the page (optional)
                Left = 100, // distance from the left edge
                Top  = 500  // distance from the bottom edge
            };

            // Insert the graph into the page's paragraphs collection
            page.Paragraphs.Add(graph);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Graph inserted and saved to '{outputPath}'.");
    }
}