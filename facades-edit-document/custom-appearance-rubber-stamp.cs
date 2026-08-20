using System;
using System.IO;
using System.Drawing;                     // For System.Drawing.Rectangle and System.Drawing.Color
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Drawing;                // For Graph, GraphInfo, Aspose.Pdf.Drawing.Rectangle

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_custom_appearance.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // ------------------------------------------------------------
        // Step 1: Build an in‑memory PDF that contains the vector graphic
        // ------------------------------------------------------------
        MemoryStream appearanceStream = new MemoryStream();
        using (Document appearanceDoc = new Document())
        {
            // Add a single page to host the graphic
            Page appPage = appearanceDoc.Pages.Add();

            // Create a Graph container (size in points). Use double overload as the float overload is obsolete.
            Graph graph = new Graph(200.0, 100.0); // width = 200pt, height = 100pt

            // Define a rectangle shape inside the graph (Aspose.Pdf.Drawing.Rectangle, not Aspose.Pdf.Rectangle)
            Aspose.Pdf.Drawing.Rectangle shapeRect = new Aspose.Pdf.Drawing.Rectangle(0, 0, 200, 100);
            shapeRect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 2f
            };
            graph.Shapes.Add(shapeRect);

            // Add the graph to the page's content
            appPage.Paragraphs.Add(graph);

            // Save the appearance PDF into the memory stream
            appearanceDoc.Save(appearanceStream);
        }

        // Reset the stream so it can be read by the editor
        appearanceStream.Position = 0;

        // ------------------------------------------------------------
        // Step 2: Open the target PDF and add a rubber‑stamp annotation
        //         using the custom appearance stream created above.
        // ------------------------------------------------------------
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the existing PDF
            editor.BindPdf(inputPath);

            // Define the stamp rectangle on the page (System.Drawing.Rectangle)
            // Parameters: x, y, width, height (all in points)
            System.Drawing.Rectangle stampRect = new System.Drawing.Rectangle(100, 500, 200, 150);

            // Create the rubber stamp with custom appearance. The color parameter also expects System.Drawing.Color.
            editor.CreateRubberStamp(
                page: 1,                     // First page (1‑based indexing)
                annotRect: stampRect,
                annotContents: "Custom Vector Stamp",
                color: System.Drawing.Color.Blue,
                appearanceStream: appearanceStream);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Custom appearance stamp saved to '{outputPath}'.");
    }
}
