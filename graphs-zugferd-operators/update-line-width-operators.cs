using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Operators;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // ------------------------------------------------------------
        // 1. Create a sample PDF in memory that contains a line drawn
        //    with a line width of 1 point. This removes the external
        //    "input.pdf" dependency that caused the FileNotFoundException.
        // ------------------------------------------------------------
        using (Document doc = new Document())
        {
            // Add a page
            Page page = doc.Pages.Add();

            // Create a Graph (vector graphics container) – use double values for the Graph size
            Graph graph = new Graph(500.0, 200.0);

            // Define a line with a width of 1 point – the Line constructor expects a float[]
            Line line = new Line(new float[] { 50f, 150f, 450f, 150f })
            {
                // Visual properties are set through GraphInfo
                GraphInfo = new GraphInfo
                {
                    Color = Color.Black,
                    LineWidth = 1f // initial width (float)
                }
            };
            graph.Shapes.Add(line);
            page.Paragraphs.Add(graph);

            // --------------------------------------------------------
            // 2. Modify all SetLineWidth operators that set width == 1
            //    to a new width of 3 points.
            // --------------------------------------------------------
            foreach (Page p in doc.Pages)
            {
                // OperatorCollection uses 1‑based indexing
                for (int i = 1; i <= p.Contents.Count; i++)
                {
                    Operator op = p.Contents[i];
                    if (op is SetLineWidth setLineWidth && Math.Abs(setLineWidth.Width - 1.0) < 0.0001)
                    {
                        setLineWidth.Width = 3.0; // SetLineWidth expects a double
                    }
                }
            }

            // --------------------------------------------------------
            // 3. Save the modified PDF
            // --------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"Line widths updated and saved to '{outputPath}'.");
    }
}
