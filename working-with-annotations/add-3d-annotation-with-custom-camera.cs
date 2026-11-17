using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // PDF3D* classes live here
using Aspose.Pdf.Drawing;   // for Matrix3D, Color, etc.

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string outputPdf  = "output_with_3d.pdf"; // result PDF
        const string modelU3D   = "product.u3d";        // 3‑D model file (U3D or PRC)

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(modelU3D))
        {
            Console.Error.WriteLine($"3‑D model file not found: {modelU3D}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Choose the page where the annotation will be placed (first page here)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // ------------------------------------------------------------
            // 3‑D annotation creation – using the PDF3D* API (prefixed types)
            // ------------------------------------------------------------

            // 1. Load the 3‑D model into a PDF3DContent object
            PDF3DContent content = new PDF3DContent(modelU3D);
            // Explicitly load as U3D (optional – the constructor may already do this)
            content.LoadAsU3D(modelU3D);

            // 2. Create a lighting scheme and render mode (optional but common)
            PDF3DLightingScheme lighting = new PDF3DLightingScheme(LightingSchemeType.CAD);
            PDF3DRenderMode render = new PDF3DRenderMode(RenderModeType.Solid);

            // 3. Create the artwork that will hold the content, lighting and render mode
            PDF3DArtwork artwork = new PDF3DArtwork(doc, content, lighting, render);

            // 4. Define a custom camera using a Matrix3D. The matrix below places the
            //    virtual camera 500 units back on the Z‑axis looking toward the origin.
            //    Matrix3D expects a single array of 16 values (float or double).
            Matrix3D cameraMatrix = new Matrix3D(new double[]
            {
                1, 0, 0, 0,
                0, 1, 0, 0,
                0, 0, 1, -500,
                0, 0, 0, 1
            });

            // 5. Create a view that uses this camera. The zoom factor (100) and a name are optional.
            PDF3DView view = new PDF3DView(doc, cameraMatrix, 100.0, "CustomView");
            artwork.ViewArray.Add(view);

            // 6. Build the annotation. The activation enum value "activeWhenOpen" makes the
            //    3‑D content visible as soon as the page is opened.
            PDF3DAnnotation annotation = new PDF3DAnnotation(page, rect, artwork, PDF3DActivation.activeWhenOpen);
            // Set the default view (the one we just added) – index 0 because it is the first view.
            annotation.SetDefaultViewIndex(0);

            // 7. Add the annotation to the page
            page.Annotations.Add(annotation);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"3‑D annotation added and saved to '{outputPdf}'.");
    }
}
