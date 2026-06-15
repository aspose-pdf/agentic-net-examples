using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

namespace AsposePdfExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Step 1: Create a simple sample PDF and save it as input.pdf
            using (Document sampleDoc = new Document())
            {
                Page samplePage = sampleDoc.Pages.Add();
                // First graph (just a line) to have something in the PDF
                Graph firstGraph = new Graph(200.0, 200.0);
                Line line = new Line(new float[] { 10f, 10f, 190f, 190f });
                line.GraphInfo = new GraphInfo();
                line.GraphInfo.Color = Aspose.Pdf.Color.Black;
                firstGraph.Shapes.Add(line);
                samplePage.Paragraphs.Add(firstGraph);
                sampleDoc.Save("input.pdf");
            }

            // Step 2: Load the PDF bytes into a byte array
            byte[] pdfBytes = File.ReadAllBytes("input.pdf");

            // Step 3: Deserialize the byte array back into a Document
            using (MemoryStream memoryStream = new MemoryStream(pdfBytes))
            {
                using (Document doc = new Document(memoryStream))
                {
                    // Access the first page (1‑based indexing)
                    Page page = doc.Pages[1];

                    // Step 4: Create a second graph with a rectangle and a text label
                    Graph secondGraph = new Graph(200.0, 200.0);

                    // Drawing rectangle shape (Aspose.Pdf.Drawing.Rectangle)
                    Aspose.Pdf.Drawing.Rectangle rectangle = new Aspose.Pdf.Drawing.Rectangle(20f, 20f, 140f, 80f);
                    rectangle.GraphInfo = new GraphInfo();
                    rectangle.GraphInfo.Color = Aspose.Pdf.Color.Blue;
                    secondGraph.Shapes.Add(rectangle);

                    // Text label inside the graph – use Graph.Title (TextFragment)
                    TextFragment label = new TextFragment("Sample Label");
                    label.Position = new Position(30f, 80f);
                    // Configure appearance via TextState (DefaultAppearance does not exist)
                    label.TextState.Font = FontRepository.FindFont("Helvetica");
                    label.TextState.FontSize = 12.0f; // float literal
                    label.TextState.ForegroundColor = Aspose.Pdf.Color.Red;
                    secondGraph.Title = label;

                    // Add the second graph to the page
                    page.Paragraphs.Add(secondGraph);

                    // Step 5: Save the modified PDF
                    doc.Save("output.pdf");
                }
            }
        }
    }
}
