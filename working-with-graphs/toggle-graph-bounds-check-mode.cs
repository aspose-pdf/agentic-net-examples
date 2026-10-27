using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing; // Graph resides here

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

        // User setting: true = ignore bounds, false = throw exception if does not fit
        // Note: Aspose.Pdf.Drawing.Graph does not expose a BoundsCheckMode property.
        // The library always checks shape bounds when rendering. If you need custom
        // behaviour you must implement it manually (e.g., validate shape positions
        // before adding them). Here we simply respect the setting by performing a
        // manual check when the flag is false.
        bool ignoreBounds = GetUserPreference();

        using (Document doc = new Document(inputPath))
        {
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // Define the rectangle that will contain the graph
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Graph constructor takes only width and height (no position).
            double width = rect.URX - rect.LLX;   // 200
            double height = rect.URY - rect.LLY; // 200
            Graph graph = new Graph(width, height);

            // Position the graph on the page using Left and Top properties.
            graph.Left = rect.LLX;   // X‑coordinate of the left side
            graph.Top = rect.URY;    // Y‑coordinate of the top side (origin is bottom‑left)

            // Optional manual bounds validation when ignoreBounds == false
            if (!ignoreBounds)
            {
                // Example validation: ensure the graph fits within the page size.
                // If it does not, throw an exception mimicking the missing BoundsCheckMode.
                Page page = doc.Pages[1];
                if (graph.Left + graph.Width > page.PageInfo.Width ||
                    graph.Top - graph.Height < 0)
                {
                    throw new InvalidOperationException("Graph does not fit within the page bounds.");
                }
            }

            // Add the graph to the first page.
            doc.Pages[1].Paragraphs.Add(graph);

            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }

    // Placeholder for obtaining user preference; replace with real logic as needed
    static bool GetUserPreference()
    {
        Console.Write("Ignore bounds when drawing Graph? (y/n): ");
        string input = Console.ReadLine()?.Trim().ToLowerInvariant();
        return input == "y" || input == "yes";
    }
}
