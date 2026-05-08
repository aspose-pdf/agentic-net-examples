using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPdf = "output.pdf";
        const string imagePath = "sample.jpg";   // external image file
        const double scaleFactor = 0.5;          // proportion of original size
        const float rotateAngle = 45f;           // rotation in degrees (arbitrary angle)

        // Ensure the image file exists
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image not found: {imagePath}");
            return;
        }

        // Create a new PDF document and add a blank page
        using (Document pdfDoc = new Document())
        {
            pdfDoc.Pages.Add();

            // ------------------------------------------------------------
            // 1. Create a Graph (graphics container) and add it to the page
            // ------------------------------------------------------------
            // Graph(width, height) – dimensions are in points (1/72 inch)
            // Use the double‑based constructor as required by newer Aspose.Pdf versions
            Graph graph = new Graph(400.0, 300.0);

            // Optional visual styling for the graph border – use GraphInfo instead of the obsolete Border class
            graph.GraphInfo = new GraphInfo
            {
                Color = Color.Gray,   // border colour
                LineWidth = 1f        // border thickness (points)
            };

            // Add the graph to the page's paragraph collection
            pdfDoc.Pages[1].Paragraphs.Add(graph);

            // ------------------------------------------------------------
            // 2. Insert the external image as a stamp (allows scaling & rotation)
            // ------------------------------------------------------------
            // ImageStamp resides in Aspose.Pdf.Drawing, not in the Facades namespace
            ImageStamp imgStamp = new ImageStamp(imagePath);

            // Scale proportionally: Zoom sets both axes equally (expects a float)
            imgStamp.Zoom = (float)scaleFactor;

            // Rotate by an arbitrary angle (not limited to multiples of 90)
            imgStamp.RotateAngle = rotateAngle;

            // Position the stamp – here we place it at (100, 400) points from the bottom‑left
            imgStamp.XIndent = 100;
            imgStamp.YIndent = 400;

            // Add the stamp to the page (the stamp is rendered on top of existing content)
            pdfDoc.Pages[1].AddStamp(imgStamp);

            // ------------------------------------------------------------
            // 3. Save the resulting PDF
            // ------------------------------------------------------------
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF created: {outputPdf}");
    }
}
