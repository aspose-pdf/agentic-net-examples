using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

namespace GraphBoundsCheckExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a sample PDF file (empty) to work with
            using (Document sampleDoc = new Document())
            {
                sampleDoc.Pages.Add();
                sampleDoc.Save("input.pdf");
            }

            // Open the sample PDF and add a graph with shapes
            using (Document doc = new Document("input.pdf"))
            {
                Page page = doc.Pages[1];

                // Create a graph of size 200x200 points
                Graph graph = new Graph(200.0, 200.0);
                graph.Left = 100.0f;
                graph.Top = 500.0f;

                // Enable bounds checking that throws an exception if a shape does not fit
                graph.Shapes.UpdateBoundsCheckMode(Aspose.Pdf.BoundsCheckMode.ThrowExceptionIfDoesNotFit);

                // First rectangle (fits inside the graph)
                Aspose.Pdf.Drawing.Rectangle rect1 = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 150f, 150f);
                rect1.GraphInfo = new GraphInfo
                {
                    FillColor = Aspose.Pdf.Color.LightBlue,
                    Color = Aspose.Pdf.Color.Black,
                    LineWidth = 2.0f
                };
                graph.Shapes.Add(rect1);

                // Second rectangle (partially outside the graph bounds)
                Aspose.Pdf.Drawing.Rectangle rect2 = new Aspose.Pdf.Drawing.Rectangle(100f, 100f, 150f, 150f);
                rect2.GraphInfo = new GraphInfo
                {
                    FillColor = Aspose.Pdf.Color.LightCoral,
                    Color = Aspose.Pdf.Color.Black,
                    LineWidth = 2.0f
                };

                try
                {
                    graph.Shapes.Add(rect2);
                }
                catch (BoundsOutOfRangeException ex)
                {
                    Console.WriteLine("Bounds check prevented adding shape: " + ex.Message);
                }

                // Add the graph to the page
                page.Paragraphs.Add(graph);

                // Save the resulting PDF
                doc.Save("output.pdf");
            }
        }
    }
}
