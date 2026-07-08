using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "output_with_3d.pdf";
        const string model3d    = "model.u3d";   // 3‑D artwork file (U3D/PRC)

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(model3d))
        {
            Console.Error.WriteLine($"3‑D model not found: {model3d}");
            return;
        }

        // Load the source PDF (document‑disposal‑with‑using rule)
        using (Document doc = new Document(inputPdf))
        {
            // -----------------------------------------------------------------
            // 1. Create the 3‑D content object from the external file
            // -----------------------------------------------------------------
            PDF3DContent content = new PDF3DContent(model3d);

            // -----------------------------------------------------------------
            // 2. Create the 3‑D artwork (default lighting & render mode)
            // -----------------------------------------------------------------
            PDF3DArtwork artwork = new PDF3DArtwork(doc, content);

            // -----------------------------------------------------------------
            // 3. Define an initial view that shows the front of the model
            //    – CameraPosition: identity matrix (default view direction)
            //    – CameraOrbit: 0 (no orbit rotation)
            // -----------------------------------------------------------------
            Matrix3D cameraPos = new Matrix3D(); // identity matrix
            double cameraOrbit = 0.0;
            PDF3DView frontView = new PDF3DView(doc, cameraPos, cameraOrbit, "FrontView");

            // Add the view to the artwork's view array
            artwork.ViewArray.Add(frontView);

            // -----------------------------------------------------------------
            // 4. Create the 3‑D annotation on the first page
            // -----------------------------------------------------------------
            Page page = doc.Pages[1];
            // Rectangle: left, bottom, width, height (using fully qualified type)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 400, 300, 600);
            // Activation mode – show when the page is opened
            PDF3DActivation activation = PDF3DActivation.activeWhenOpen;

            PDF3DAnnotation annotation = new PDF3DAnnotation(page, rect, artwork, activation);

            // Optional: set the default view index to the front view (index 0)
            annotation.SetDefaultViewIndex(0);

            // Add the annotation to the page
            page.Annotations.Add(annotation);

            // -----------------------------------------------------------------
            // 5. Save the modified PDF (lifecycle rule)
            // -----------------------------------------------------------------
            doc.Save(outputPdf);
        }

        Console.WriteLine($"3‑D annotation added and saved to '{outputPdf}'.");
    }
}