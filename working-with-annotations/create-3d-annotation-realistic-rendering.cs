using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string outputPdf = "3d_annotation.pdf";
        const string modelFile = "model.u3d"; // 3D model file (U3D, PRC, etc.)

        // Ensure the 3D model file exists
        if (!File.Exists(modelFile))
        {
            Console.Error.WriteLine($"3D model file not found: {modelFile}");
            return;
        }

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page
            Page page = doc.Pages.Add();

            // -----------------------------------------------------------------
            // 1. Create the 3D content from the external file
            // -----------------------------------------------------------------
            PDF3DContent content = new PDF3DContent(modelFile);

            // -----------------------------------------------------------------
            // 2. Define a lighting scheme – use a named scheme (e.g., "Default")
            // -----------------------------------------------------------------
            PDF3DLightingScheme lightingScheme = new PDF3DLightingScheme("Default");

            // -----------------------------------------------------------------
            // 3. Choose a render mode that provides realistic shading
            //    (ShadedIllustration gives a shaded view of the 3D model)
            // -----------------------------------------------------------------
            PDF3DRenderMode renderMode = PDF3DRenderMode.ShadedIllustration;

            // -----------------------------------------------------------------
            // 4. Create the 3D artwork, linking the document, content,
            //    lighting scheme and render mode
            // -----------------------------------------------------------------
            PDF3DArtwork artwork = new PDF3DArtwork(doc, content, lightingScheme, renderMode);

            // -----------------------------------------------------------------
            // 5. Create a view – position the camera and set a background color
            // -----------------------------------------------------------------
            // Matrix3D represents the camera position; using the default constructor
            // creates an identity matrix (camera at origin looking towards the model).
            Matrix3D cameraMatrix = new Matrix3D();
            double cameraOrbit = 0.0; // No orbit rotation
            PDF3DView view = new PDF3DView(doc, cameraMatrix, cameraOrbit, "DefaultView");

            // Optional: adjust view properties for realism
            view.BackGroundColor = Aspose.Pdf.Color.LightGray;
            view.LightingScheme = lightingScheme;
            view.RenderMode = PDF3DRenderMode.ShadedIllustration;

            // Add the view to the artwork's view collection
            // The ViewArray is read‑only, but adding a view is performed via the
            // artwork's internal collection when the view is constructed with the
            // same document – the view is automatically associated.
            // No explicit Add method is required.

            // -----------------------------------------------------------------
            // 6. Create the 3D annotation and place it on the page
            // -----------------------------------------------------------------
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 400, 500, 800);
            PDF3DAnnotation annotation = new PDF3DAnnotation(page, rect, artwork);

            // Set the default view index (0‑based) – we have only one view, so index 0
            annotation.SetDefaultViewIndex(0);

            // Optional: set a border and color for the annotation rectangle
            annotation.Color = Aspose.Pdf.Color.DarkGray;
            annotation.Border = new Border(annotation) { Width = 1 };

            // Add the annotation to the page
            page.Annotations.Add(annotation);

            // -----------------------------------------------------------------
            // 7. Save the PDF
            // -----------------------------------------------------------------
            doc.Save(outputPdf);
            Console.WriteLine($"PDF with 3D annotation saved to '{outputPdf}'.");
        }
    }
}