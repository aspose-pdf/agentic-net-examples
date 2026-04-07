using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing; // for Graph if needed (not used here)

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";   // existing PDF (can be empty or a template)
        const string outputPath = "polygon_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the document
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page to annotate
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // Initialize the content editor facade
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // Define the polygon vertices (x1,y1, x2,y2, ...). Example: triangle.
            LineInfo lineInfo = new LineInfo();
            lineInfo.VerticeCoordinate = new float[] { 100, 100, 200, 100, 150, 200 };
            lineInfo.Visibility = true; // make the polygon visible

            // Create the polygon annotation on page 1.
            // The rectangle parameter can be zero‑sized because the vertices define the shape.
            editor.CreatePolygon(lineInfo, 1, new System.Drawing.Rectangle(0, 0, 0, 0), "Sample Polygon");

            // Retrieve the newly created annotation (it is the last one added).
            var annotations = doc.Pages[1].Annotations;
            if (annotations.Count == 0)
            {
                Console.Error.WriteLine("No annotations were created.");
                return;
            }

            if (annotations[annotations.Count - 1] is PolygonAnnotation polygon)
            {
                // Fill the polygon with a solid interior color (placeholder for a pattern).
                polygon.InteriorColor = Aspose.Pdf.Color.LightGray;

                // Set the border (outline) color via the annotation itself.
                polygon.Color = Aspose.Pdf.Color.DarkBlue;

                // Configure the border dash pattern. Border requires the parent annotation.
                polygon.Border = new Border(polygon)
                {
                    Width = 2,
                    // Apply a dash pattern: 5 units on, 2 units off.
                    Dash = new Dash(5, 2)
                };
            }
            else
            {
                Console.Error.WriteLine("The created annotation is not a PolygonAnnotation.");
            }

            // Save the modified document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Polygon annotation saved to '{outputPath}'.");
    }
}
