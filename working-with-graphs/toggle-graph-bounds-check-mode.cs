using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

// Alias to avoid ambiguity with Aspose.Pdf.Rectangle (page rectangle)
using DrawRect = Aspose.Pdf.Drawing.Rectangle;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the input file exists – create a minimal PDF if it does not.
        if (!File.Exists(inputPath))
        {
            using var placeholder = new Document();
            placeholder.Pages.Add();
            placeholder.Save(inputPath);
        }

        // Determine user preference (true = ignore bounds, false = throw exception)
        bool ignoreBounds = GetUserPreference();

        // Load the PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // For demonstration, work with the first page
            Page page = doc.Pages[1];

            // Use the double‑precision constructor (the float overload is obsolete).
            Graph graph = new Graph(200.0, 200.0)
            {
                Left = 100,
                Top = 100
            };

            // Toggle the bounds‑checking mode on the Shapes collection.
            if (ignoreBounds)
                graph.Shapes.UpdateBoundsCheckMode(BoundsCheckMode.Default); // ignore out‑of‑bounds shapes
            else
                graph.Shapes.UpdateBoundsCheckMode(BoundsCheckMode.ThrowExceptionIfDoesNotFit);

            // Add a sample shape (this will throw in strict mode because it exceeds the graph size).
            var rect = new DrawRect(0, 0, 250, 250);
            rect.GraphInfo.Color = Color.Red;
            rect.GraphInfo.LineWidth = 2;
            graph.Shapes.Add(rect);

            // Add the graph to the page's content
            page.Paragraphs.Add(graph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }

    // Placeholder for obtaining the user's setting; replace with real logic as needed
    static bool GetUserPreference()
    {
        // Example: read from a config file, command‑line argument, UI, etc.
        // Here we simply return true to demonstrate the "Ignore" mode.
        return true;
    }
}
