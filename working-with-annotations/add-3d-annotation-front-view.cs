using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdf   = "input.pdf";          // source PDF
        const string outputPdf  = "output_with_3d.pdf"; // result PDF
        const string modelFile  = "model.u3d";          // 3‑D artwork file (U3D/PRC)

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(modelFile))
        {
            Console.Error.WriteLine($"3D model file not found: {modelFile}");
            return;
        }

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(inputPdf))
        {
            // -----------------------------------------------------------------
            // 1. Create the 3‑D content object from the external file.
            // -----------------------------------------------------------------
            PDF3DContent content = new PDF3DContent(modelFile);

            // -----------------------------------------------------------------
            // 2. Create the 3‑D artwork. Use default lighting and render mode.
            // -----------------------------------------------------------------
            PDF3DArtwork artwork = new PDF3DArtwork(doc, content);

            // -----------------------------------------------------------------
            // 3. Define a view that shows the front of the model.
            //    - Use an identity matrix for the camera position (looking straight on).
            //    - Set camera orbit to 0 (no rotation around the object).
            //    - Give the view a name.
            // -----------------------------------------------------------------
            Matrix3D cameraPos = new Matrix3D(); // identity matrix
            double cameraOrbit = 0.0;            // front view
            PDF3DView frontView = new PDF3DView(doc, cameraPos, cameraOrbit, "FrontView");

            // Add the view to the artwork's view array.
            artwork.ViewArray.Add(frontView);

            // -----------------------------------------------------------------
            // 4. Create the 3‑D annotation on the first page.
            //    - Define the rectangle where the annotation will appear.
            //    - Use activation mode "activeWhenOpen" so it is visible immediately.
            // -----------------------------------------------------------------
            Page page = doc.Pages[1];
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 400, 400, 700);
            PDF3DAnnotation annotation = new PDF3DAnnotation(page, rect, artwork, PDF3DActivation.activeWhenOpen);

            // Optional: set a border and background color for the annotation.
            annotation.Border = new Border(annotation) { Width = 1 };
            annotation.Color = Aspose.Pdf.Color.LightGray;

            // Set the default view index to the first (and only) view we added.
            annotation.SetDefaultViewIndex(0);

            // Add the annotation to the page.
            page.Annotations.Add(annotation);

            // -----------------------------------------------------------------
            // 5. Save the modified PDF.
            // -----------------------------------------------------------------
            doc.Save(outputPdf);
        }

        Console.WriteLine($"3‑D annotation added and saved to '{outputPdf}'.");
    }
}