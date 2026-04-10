using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output_custom_appearance.pdf";

        // Ensure the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdfPath}");
            return;
        }

        // --------------------------------------------------------------------
        // Step 1: Build a PDF page that contains the vector graphic we want to
        // use as the custom appearance stream.
        // --------------------------------------------------------------------
        byte[] appearanceBytes;
        using (MemoryStream appearanceStream = new MemoryStream())
        {
            // Create a temporary PDF document that will hold the vector graphic.
            using (Document appearanceDoc = new Document())
            {
                // Add a single page.
                Page page = appearanceDoc.Pages.Add();

                // Create a Graph container (vector drawing surface).
                // Width and height are arbitrary; they define the coordinate system.
                // Use the double‑based constructor as the float overload is obsolete.
                Graph graph = new Graph(200.0, 200.0);

                // Example vector graphic: a light‑gray rectangle with a red border.
                Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(20f, 20f, 160f, 120f);
                rectShape.GraphInfo = new GraphInfo
                {
                    FillColor = Aspose.Pdf.Color.LightGray,
                    Color = Aspose.Pdf.Color.Red,
                    LineWidth = 2f
                };
                graph.Shapes.Add(rectShape);

                // Ellipse (x, y, width, height) – also a drawing shape.
                Ellipse ellipseShape = new Ellipse(60.0, 60.0, 80.0, 80.0);
                ellipseShape.GraphInfo = new GraphInfo
                {
                    FillColor = Aspose.Pdf.Color.Yellow,
                    Color = Aspose.Pdf.Color.Blue,
                    LineWidth = 1.5f
                };
                graph.Shapes.Add(ellipseShape);

                // Add the Graph to the page.
                page.Paragraphs.Add(graph);

                // Save the temporary document to the memory stream as PDF.
                appearanceDoc.Save(appearanceStream);
            }

            // Capture the PDF bytes for later use.
            appearanceBytes = appearanceStream.ToArray();
        }

        // --------------------------------------------------------------------
        // Step 2: Open the target PDF and apply the custom appearance stream
        // as a rubber‑stamp annotation.
        // --------------------------------------------------------------------
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF.
            editor.BindPdf(inputPdfPath);

            // Define the annotation rectangle using System.Drawing.Rectangle (the API expects this type).
            System.Drawing.Rectangle annotRect = new System.Drawing.Rectangle(100, 500, 200, 200); // x, y, width, height

            // Create a MemoryStream from the appearance PDF bytes.
            using (MemoryStream appStream = new MemoryStream(appearanceBytes))
            {
                // Add the rubber‑stamp annotation on page 1 using the custom appearance.
                // Parameters: page number (1‑based), rectangle, contents, color, appearance stream.
                editor.CreateRubberStamp(
                    page: 1,
                    annotRect: annotRect,
                    annotContents: "Custom Vector Stamp",
                    color: System.Drawing.Color.Black,
                    appearanceStream: appStream);
            }

            // Save the modified PDF.
            editor.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with custom annotation appearance: {outputPdfPath}");
    }
}
