using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // User setting: true = enforce bounds, false = ignore bounds
        bool enforceBounds = GetUserSetting();

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (width: 500, height: 400)
            Graph graph = new Graph(500, 400);

            // Toggle the bounds‑checking mode for the Shapes collection
            if (enforceBounds)
            {
                // Throw an exception if a shape does not fit within the graph bounds
                graph.Shapes.UpdateBoundsCheckMode(Aspose.Pdf.BoundsCheckMode.ThrowExceptionIfDoesNotFit);
            }
            else
            {
                // Disable bounds checking (Default behavior)
                graph.Shapes.UpdateBoundsCheckMode(Aspose.Pdf.BoundsCheckMode.Default);
            }

            // Example shape: a horizontal line
            float[] linePoints = { 0, 0, 200, 0 };
            Line line = new Line(linePoints);
            line.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Blue,
                LineWidth = 2
            };

            // Add the line to the graph
            graph.Shapes.Add(line);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save("output.pdf");
        }

        Console.WriteLine("PDF created with bounds‑check mode set to " +
                          (enforceBounds ? "ThrowExceptionIfDoesNotFit" : "Default"));
    }

    // Placeholder for obtaining the user setting (could be from config, UI, etc.)
    static bool GetUserSetting()
    {
        // For demonstration, return true to enforce bounds.
        // Change to false to ignore bounds.
        return true;
    }
}