using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Paths for source PDF and output PDF
        const string sourcePdfPath = "source.pdf";
        const string outputPdfPath = "output_with_custom_stamp.pdf";

        // Ensure the source PDF exists
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }

        // -----------------------------------------------------------------
        // Step 1: Create a small PDF that will serve as the appearance stream.
        // The appearance PDF contains a simple vector graphic (a blue rectangle
        // with a light‑gray fill). This PDF is saved into a memory stream.
        // -----------------------------------------------------------------
        MemoryStream appearanceStream = new MemoryStream();
        using (Document appearanceDoc = new Document())
        {
            // Add a single page
            Page appearancePage = appearanceDoc.Pages.Add();

            // Create a Graph container (size 200x200 points)
            Graph graph = new Graph(200, 200);

            // Define a rectangle shape (position 0,0; width 100, height 50)
            Aspose.Pdf.Drawing.Rectangle shapeRect = new Aspose.Pdf.Drawing.Rectangle(0, 0, 100, 50);
            shapeRect.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Blue,          // Stroke color
                FillColor = Aspose.Pdf.Color.LightGray, // Fill color
                LineWidth = 2
            };
            graph.Shapes.Add(shapeRect);

            // Add the graph to the page
            appearancePage.Paragraphs.Add(graph);

            // Save the appearance PDF into the memory stream
            appearanceDoc.Save(appearanceStream);
        }

        // Reset the stream position so it can be read by the editor
        appearanceStream.Position = 0;

        // -----------------------------------------------------------------
        // Step 2: Open the target PDF with PdfContentEditor and add a rubber
        // stamp annotation that uses the custom appearance stream.
        // -----------------------------------------------------------------
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF
            editor.BindPdf(sourcePdfPath);

            // Define the annotation rectangle (position and size on the page)
            // Here we place it at (100,500) with width=150 and height=100 points.
            System.Drawing.Rectangle annotRect = new System.Drawing.Rectangle(100, 500, 150, 100);

            // Create the rubber stamp annotation:
            //   page = 1 (first page)
            //   annotRect = rectangle defined above
            //   annotContents = "Custom Stamp"
            //   color = red (border/color of the annotation)
            //   appearanceStream = the PDF we created with the vector graphic
            editor.CreateRubberStamp(
                page: 1,
                annotRect: annotRect,
                annotContents: "Custom Stamp",
                color: System.Drawing.Color.Red,
                appearanceStream: appearanceStream);

            // Save the modified PDF
            editor.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with custom annotation saved to '{outputPdfPath}'.");
    }
}