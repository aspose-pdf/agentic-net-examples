using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";      // source PDF
        const string outputPdfPath  = "output.pdf";     // result PDF
        const string imagePath      = "image.png";      // external image file
        const double targetWidth    = 200.0;            // desired width (points)
        const double rotationAngle = 45.0;             // rotation angle in degrees

        // Ensure source files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Work with the first page (page indexing is 1‑based)
            Page page = pdfDoc.Pages[1];

            // ------------------------------------------------------------
            // 1. Create a Graph container (acts as a drawing canvas)
            // ------------------------------------------------------------
            // Width and height of the graph are arbitrary; they define the
            // coordinate system for the shapes placed inside it.
            Graph graph = new Graph(500.0, 400.0); // use double constructor
            // Optional visual styling for the graph border – omitted because
            // the Border class is not available in the current Aspose.Pdf.Drawing
            // namespace version. The graph will be rendered without an explicit border.
            page.Paragraphs.Add(graph);

            // ------------------------------------------------------------
            // 2. Load the image to obtain its original dimensions
            // ------------------------------------------------------------
            // Adding the image to the page resources gives us an XImage
            // object that exposes Width and Height (in points).
            XImage xImg;
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                string imgName = page.Resources.Images.Add(imgStream);
                xImg = page.Resources.Images[imgName];
            }

            double originalWidth  = xImg.Width;   // original width in points
            double originalHeight = xImg.Height;  // original height in points

            // ------------------------------------------------------------
            // 3. Compute proportional scaling factor
            // ------------------------------------------------------------
            double scaleFactor   = targetWidth / originalWidth;
            double targetHeight  = originalHeight * scaleFactor;

            // ------------------------------------------------------------
            // 4. Create an ImageStamp, set size, rotation and position
            // ------------------------------------------------------------
            ImageStamp imgStamp = new ImageStamp(imagePath)
            {
                // Scale proportionally
                Width  = targetWidth,
                Height = targetHeight,

                // Rotate by an arbitrary angle (not limited to multiples of 90)
                RotateAngle = rotationAngle,

                // Position the stamp (coordinates are from the bottom‑left corner)
                XIndent = 100,   // horizontal offset from the left edge
                YIndent = 500    // vertical offset from the bottom edge
            };

            // ------------------------------------------------------------
            // 5. Add the stamp to the page (the stamp sits on top of the graph)
            // ------------------------------------------------------------
            page.AddStamp(imgStamp);

            // ------------------------------------------------------------
            // 6. Save the modified PDF (lifecycle rule: save inside using)
            // ------------------------------------------------------------
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Image inserted, scaled to {targetWidth} points width, rotated {rotationAngle}°, and saved to '{outputPdfPath}'.");
    }
}
