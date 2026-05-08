using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string outputPdf = "product_with_3d.pdf";
        const string modelFile = "product.u3d"; // 3D model file (U3D or PRC)

        // Verify the 3D model exists
        if (!File.Exists(modelFile))
        {
            Console.Error.WriteLine($"3D model not found: {modelFile}");
            return;
        }

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define the rectangle where the 3D annotation will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // Load the 3D content (U3D/PRC) from file
            PDF3DContent content = new PDF3DContent(modelFile);

            // Create the 3D artwork with default lighting and render mode
            PDF3DArtwork artwork = new PDF3DArtwork(doc, content);

            // Create a custom camera position using a Matrix3D.
            // This matrix translates the camera to (0, 0, 200) in 3D space.
            // Matrix3D(a,b,c,d,e,f,g,h,i,tx,ty,tz)
            Matrix3D cameraMatrix = new Matrix3D(
                1, 0, 0,   // X axis
                0, 1, 0,   // Y axis
                0, 0, 1,   // Z axis
                0, 0, 200  // translation (camera position)
            );

            // Define the orbit (distance from the object) – adjust as needed
            double cameraOrbit = 300.0;

            // Create a view with the custom camera
            PDF3DView view = new PDF3DView(doc, cameraMatrix, cameraOrbit, "CustomView");

            // Optionally set additional view properties
            view.BackGroundColor = Aspose.Pdf.Color.White;
            view.RenderMode = PDF3DRenderMode.Illustration; // use illustration render mode

            // Add the view to the artwork's view array (only one view allowed)
            artwork.ViewArray.Add(view);

            // Create the 3D annotation and associate it with the artwork
            PDF3DAnnotation annotation = new PDF3DAnnotation(page, rect, artwork);

            // Set a default view index (0 = first view)
            annotation.SetDefaultViewIndex(0);

            // Add the annotation to the page
            page.Annotations.Add(annotation);

            // Save the PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with 3D annotation saved to '{outputPdf}'.");
    }
}