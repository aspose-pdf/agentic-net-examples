using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Paths for source PDF, output PDF and the external image
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string imagePath     = "image.png";

        // Desired rotation angle in degrees (e.g., 45°)
        const float rotationAngle = 45f;

        // Ensure the input files exist
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

        // Open the PDF document (lifecycle: load)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // -----------------------------------------------------------------
            // 1. Create a Graph container (optional – demonstrates graph usage)
            // -----------------------------------------------------------------
            // Width = 400 points, Height = 300 points
            Graph graph = new Graph(400, 300);
            // Add the graph to the first page's paragraph collection
            pdfDoc.Pages[1].Paragraphs.Add(graph);

            // ---------------------------------------------------------------
            // 2. Insert the external image as a stamp (allows scaling & rotate)
            // ---------------------------------------------------------------
            // ImageStamp loads the image from the file path
            ImageStamp imgStamp = new ImageStamp(imagePath);

            // Scale proportionally: set Zoom (applies equally to X and Y)
            // For example, 0.5f scales the image to 50% of its original size
            imgStamp.Zoom = 0.5f;

            // Rotate the image by the specified angle (arbitrary degrees)
            imgStamp.RotateAngle = rotationAngle;

            // Position the stamp – center it on the page
            imgStamp.HorizontalAlignment = HorizontalAlignment.Center;
            imgStamp.VerticalAlignment   = VerticalAlignment.Center;

            // Add the stamp to the first page (lifecycle: modify)
            pdfDoc.Pages[1].AddStamp(imgStamp);

            // ---------------------------------------------------------------
            // 3. Save the modified PDF (lifecycle: save)
            // ---------------------------------------------------------------
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Image inserted, scaled, and rotated. Saved to '{outputPdfPath}'.");
    }
}