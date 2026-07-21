using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "output_with_3d.pdf";
        const string threeDFilePath = "model.u3d"; // 3‑D artwork file (U3D, PRC, etc.)

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(threeDFilePath))
        {
            Console.Error.WriteLine($"3‑D file not found: {threeDFilePath}");
            return;
        }

        // Open the PDF
        using (Document doc = new Document(inputPdfPath))
        {
            // Ensure there is at least one page
            Page page = doc.Pages[1];

            // -----------------------------------------------------------------
            // 1. Create the 3‑D content (the raw 3‑D file)
            // -----------------------------------------------------------------
            PDF3DContent content = new PDF3DContent(threeDFilePath);

            // -----------------------------------------------------------------
            // 2. Create the 3‑D artwork – use default lighting and render mode
            // -----------------------------------------------------------------
            PDF3DArtwork artwork = new PDF3DArtwork(doc, content);

            // -----------------------------------------------------------------
            // 3. Define a view that shows the front of the model
            //    – Use an identity matrix for the camera position and zero orbit
            // -----------------------------------------------------------------
            Matrix3D cameraMatrix = new Matrix3D(); // identity matrix
            double cameraOrbit = 0.0;               // no orbit rotation
            PDF3DView frontView = new PDF3DView(doc, cameraMatrix, cameraOrbit, "FrontView");

            // Optional: set a background colour for the view (white)
            frontView.BackGroundColor = Aspose.Pdf.Color.White;

            // Add the view to the artwork's view array
            artwork.ViewArray.Add(frontView);

            // -----------------------------------------------------------------
            // 4. Create the 3‑D annotation and place it on the page
            // -----------------------------------------------------------------
            // Define the rectangle where the annotation will appear
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 400, 400, 700);

            // Use the activation mode that makes the 3‑D model active when the page is opened
            PDF3DAnnotation annotation = new PDF3DAnnotation(
                page,
                rect,
                artwork,
                PDF3DActivation.activeWhenOpen);

            // Set the default view to the one we just created (index 0)
            annotation.SetDefaultViewIndex(0);

            // Add the annotation to the page
            page.Annotations.Add(annotation);

            // -----------------------------------------------------------------
            // 5. Save the resulting PDF
            // -----------------------------------------------------------------
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with 3‑D annotation saved to '{outputPdfPath}'.");
    }
}