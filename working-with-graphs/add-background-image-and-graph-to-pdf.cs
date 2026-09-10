using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_with_background.pdf";
        const string backgroundImagePath = "background.jpg";

        // Ensure the background image file exists
        if (!File.Exists(backgroundImagePath))
        {
            Console.Error.WriteLine($"Background image not found: {backgroundImagePath}");
            return;
        }

        // Create a new PDF document and add a single page
        using (Document doc = new Document())
        {
            Aspose.Pdf.Page page = doc.Pages.Add();

            // ---------- Add background image as a page artifact ----------
            // Create a background artifact (type Background, subtype Background)
            Aspose.Pdf.Artifact bgArtifact = new Aspose.Pdf.Artifact(
                Aspose.Pdf.Artifact.ArtifactType.Background,
                Aspose.Pdf.Artifact.ArtifactSubtype.Background);

            // Set the image for the artifact
            bgArtifact.SetImage(backgroundImagePath);
            // Mark it as background so it is rendered behind page contents
            bgArtifact.IsBackground = true;

            // Add the artifact to the page
            page.Artifacts.Add(bgArtifact);

            // ---------- Create a graph (graphics container) ----------
            // Graph constructor takes width and height (double)
            Aspose.Pdf.Drawing.Graph graph = new Aspose.Pdf.Drawing.Graph(400, 200);
            // Position the graph on the page (optional)
            graph.Left = 50;   // distance from left edge
            graph.Top = 500;   // distance from bottom edge
            // Ensure the graph is drawn above the background artifact
            graph.ZIndex = 1;   // any positive value will be on top of background

            // ---------- Add shapes to the graph ----------
            // Example: a filled rectangle
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(0, 0, 150, 80);
            rect.GraphInfo = new Aspose.Pdf.GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 1
            };
            graph.Shapes.Add(rect);

            // Example: a red line drawn over the rectangle
            float[] linePoints = { 0, 0, 150, 80 };
            Aspose.Pdf.Drawing.Line line = new Aspose.Pdf.Drawing.Line(linePoints);
            line.GraphInfo = new Aspose.Pdf.GraphInfo
            {
                Color = Aspose.Pdf.Color.Red,
                LineWidth = 2
            };
            graph.Shapes.Add(line);

            // Add the graph (with its shapes) to the page
            page.Paragraphs.Add(graph);

            // Save the document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}