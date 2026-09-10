using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input files
        const string inputPdf = "template.pdf";   // existing PDF to host the 3D annotation
        const string modelU3d = "product.u3d";    // 3D model file (U3D or PRC)
        const string outputPdf = "product_3d.pdf";

        // Ensure files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(modelU3d))
        {
            Console.Error.WriteLine($"3D model file not found: {modelU3d}");
            return;
        }

        // Load the base PDF (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // -----------------------------------------------------------------
            // 1. Create the 3D content from the model file
            // -----------------------------------------------------------------
            PDF3DContent content = new PDF3DContent(modelU3d);

            // -----------------------------------------------------------------
            // 2. Create the 3D artwork (default lighting & render mode)
            // -----------------------------------------------------------------
            PDF3DArtwork artwork = new PDF3DArtwork(doc, content);

            // -----------------------------------------------------------------
            // 3. Define a custom camera position (Matrix3D) and orbit angle
            // -----------------------------------------------------------------
            // Example: place the camera 200 units away on the Z axis, looking at origin
            Matrix3D cameraMatrix = new Matrix3D(new double[]
            {
                1, 0, 0, 0,
                0, 1, 0, 0,
                0, 0, 1, 0,
                0, 0, 200, 1
            });
            double cameraOrbit = 30.0; // rotate camera 30 degrees around the object

            // -----------------------------------------------------------------
            // 4. Create a view that uses the custom camera
            // -----------------------------------------------------------------
            PDF3DView customView = new PDF3DView(doc, cameraMatrix, cameraOrbit, "CustomPerspective");
            // Optional: set background color, lighting scheme, render mode, etc.
            // customView.BackGroundColor = Aspose.Pdf.Color.LightGray;
            // customView.LightingScheme = PDF3DLightingScheme.Headlamp;
            // customView.RenderMode = PDF3DRenderMode.Illustration;

            // Add the view to the artwork's view collection
            artwork.ViewArray.Add(customView);

            // -----------------------------------------------------------------
            // 5. Create the 3D annotation on the first page
            // -----------------------------------------------------------------
            Page page = doc.Pages[1];
            // Define the rectangle where the 3D annotation will appear (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 400, 400, 700);
            PDF3DAnnotation annotation = new PDF3DAnnotation(page, rect, artwork);

            // Set the default view to the one we just created (index 0)
            annotation.SetDefaultViewIndex(0);

            // Add the annotation to the page
            page.Annotations.Add(annotation);

            // -----------------------------------------------------------------
            // 6. Save the resulting PDF (lifecycle rule: save inside using)
            // -----------------------------------------------------------------
            doc.Save(outputPdf);
        }

        Console.WriteLine($"3D annotation added and saved to '{outputPdf}'.");
    }
}
