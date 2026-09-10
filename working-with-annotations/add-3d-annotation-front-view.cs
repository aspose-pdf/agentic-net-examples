using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdf   = "input.pdf";      // source PDF
        const string outputPdf  = "output_3d.pdf";  // result PDF
        const string modelFile  = "model.u3d";      // 3‑D artwork file (contains textures)

        // Verify required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(modelFile))
        {
            Console.Error.WriteLine($"3‑D model file not found: {modelFile}");
            return;
        }

        // Load the existing PDF inside a using block (ensures proper disposal)
        using (Document doc = new Document(inputPdf))
        {
            // -----------------------------------------------------------------
            // 1. Create the 3‑D content from the external file (U3D/PRC)
            // -----------------------------------------------------------------
            PDF3DContent content = new PDF3DContent(modelFile);

            // -----------------------------------------------------------------
            // 2. Create the 3‑D artwork that will hold the content.
            //    Use default lighting scheme and render mode.
            // -----------------------------------------------------------------
            PDF3DArtwork artwork = new PDF3DArtwork(doc, content);

            // -----------------------------------------------------------------
            // 3. Define the rectangle on the page where the annotation will appear.
            //    Fully qualified type avoids ambiguity with System.Drawing.Rectangle.
            // -----------------------------------------------------------------
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 400, 400, 700);

            // -----------------------------------------------------------------
            // 4. Create the 3‑D annotation on the first page.
            // -----------------------------------------------------------------
            PDF3DAnnotation annotation = new PDF3DAnnotation(doc.Pages[1], rect, artwork);

            // -----------------------------------------------------------------
            // 5. Create a view that shows the front of the model.
            //    - Use an identity Matrix3D for the camera position (default view).
            //    - CameraOrbit = 0 positions the camera directly in front.
            // -----------------------------------------------------------------
            Matrix3D cameraPosition = new Matrix3D(); // identity matrix
            PDF3DView frontView = new PDF3DView(doc, cameraPosition, 0, "Front");

            // Optional: set a white background for the view
            frontView.BackGroundColor = Aspose.Pdf.Color.White;

            // Add the view to the artwork's view array (only one view is allowed)
            artwork.ViewArray.Add(frontView);

            // Set the newly added view as the default view (index 0)
            annotation.SetDefaultViewIndex(0);

            // -----------------------------------------------------------------
            // 6. Attach the annotation to the page's annotation collection.
            // -----------------------------------------------------------------
            doc.Pages[1].Annotations.Add(annotation);

            // -----------------------------------------------------------------
            // 7. Save the modified PDF.
            // -----------------------------------------------------------------
            doc.Save(outputPdf);
        }

        Console.WriteLine($"3‑D annotation added and saved to '{outputPdf}'.");
    }
}