using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

namespace WorkingWithGraphs
{
    public static class PdfGraphHelper
    {
        /// <summary>
        /// Builds a predefined graph (a set of drawing primitives) on the specified page
        /// and returns the Graph object for further manipulation.
        /// </summary>
        /// <param name="page">The Aspose.Pdf.Page on which the graph will be placed.</param>
        /// <returns>The created Graph instance containing the predefined drawing.</returns>
        public static Graph BuildPredefinedGraph(Page page)
        {
            if (page == null) throw new ArgumentNullException(nameof(page));

            // Use the double‑based constructor (the float overload is obsolete).
            const double graphWidth = 500.0;
            const double graphHeight = 300.0;
            var graph = new Graph(graphWidth, graphHeight)
            {
                // Position the graph on the page. Left/Top are distances from the page's left and bottom edges.
                Left = 50f,
                Top = 500f,
                // Optional border for visual reference.
                Border = new BorderInfo(BorderSide.All, 1f, Color.Black)
            };

            // Title is a TextFragment that lives inside the graph.
            var titleFragment = new TextFragment("Sample Graph");
            titleFragment.TextState.FontSize = 18;
            titleFragment.TextState.FontStyle = FontStyles.Bold;
            titleFragment.TextState.ForegroundColor = Color.DarkBlue;
            graph.Title = titleFragment;

            // -----------------------------------------------------------------
            // Add a blue rectangle.
            // -----------------------------------------------------------------
            var rect = new Aspose.Pdf.Drawing.Rectangle(20f, 20f, 180f, 100f); // left, bottom, width, height
            rect.GraphInfo.Color = Color.Blue; // stroke color
            rect.GraphInfo.LineWidth = 2f;
            rect.GraphInfo.FillColor = Color.FromRgb(0.8, 0.9, 1.0); // light blue fill
            graph.Shapes.Add(rect);

            // -----------------------------------------------------------------
            // Add a red ellipse.
            // -----------------------------------------------------------------
            var ellipse = new Aspose.Pdf.Drawing.Ellipse(250f, 20f, 200f, 100f);
            ellipse.GraphInfo.Color = Color.Red;
            ellipse.GraphInfo.LineWidth = 2f;
            ellipse.GraphInfo.FillColor = Color.FromRgb(1.0, 0.8, 0.8); // light red fill
            graph.Shapes.Add(ellipse);

            // -----------------------------------------------------------------
            // Add a green line.
            // -----------------------------------------------------------------
            var line = new Aspose.Pdf.Drawing.Line(new float[] { 20f, 150f, 450f, 150f });
            line.GraphInfo.Color = Color.Green;
            line.GraphInfo.LineWidth = 3f;
            graph.Shapes.Add(line);

            // -----------------------------------------------------------------
            // Add the graph to the page.
            // -----------------------------------------------------------------
            page.Paragraphs.Add(graph);

            // Return the constructed graph so callers can further customize it if needed.
            return graph;
        }
    }

    // A minimal entry point to satisfy the compiler. In real usage the helper would be called
    // from your own application code.
    internal class Program
    {
        private static void Main()
        {
            // Create a new PDF document and a single page.
            using var doc = new Document();
            var page = doc.Pages.Add();

            // Build the predefined graph on the page.
            PdfGraphHelper.BuildPredefinedGraph(page);

            // Save the document to a file (or stream) – this is just for demonstration.
            doc.Save("graph_output.pdf");
        }
    }
}