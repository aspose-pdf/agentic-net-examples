using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // 3D annotation types
using Aspose.Pdf.Drawing; // Matrix3D, PDF3DLightingScheme, PDF3DRenderMode, etc.

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output_3d.pdf";
        const string model3dPath = "model.u3d"; // U3D or PRC file

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(model3dPath))
        {
            Console.Error.WriteLine($"3D model file not found: {model3dPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // ------------------------------------------------------------
            // 1. Load the 3D content (U3D/PRC)
            // ------------------------------------------------------------
            PDF3DContent content = new PDF3DContent(model3dPath);
            // For a .u3d file use LoadAsU3D, for .prc use LoadAsPRC
            content.LoadAsU3D(model3dPath);

            // ------------------------------------------------------------
            // 2. Define lighting and render mode (these are presets)
            // ------------------------------------------------------------
            // Choose a lighting scheme that gives good depth perception – e.g., CAD or Headlamp
            PDF3DLightingScheme lighting = new PDF3DLightingScheme(LightingSchemeType.CAD);

            // Render mode – solid gives a clear visual of the model
            PDF3DRenderMode renderMode = new PDF3DRenderMode(RenderModeType.Solid);
            // Optional: tweak material appearance
            renderMode.SetFaceColor(Aspose.Pdf.Color.Silver);
            renderMode.SetOpacity(0.9);

            // ------------------------------------------------------------
            // 3. Create the artwork that ties content, lighting and render mode
            // ------------------------------------------------------------
            PDF3DArtwork artwork = new PDF3DArtwork(doc, content, lighting, renderMode);

            // ------------------------------------------------------------
            // 4. Create a view (camera) and add it to the artwork
            // ------------------------------------------------------------
            // Camera positioned at origin looking towards +Z; orbit distance is arbitrary
            Matrix3D cameraMatrix = new Matrix3D(); // identity matrix (default view)
            PDF3DView view = new PDF3DView(doc, cameraMatrix, 100.0, "DefaultView");
            view.BackGroundColor = Aspose.Pdf.Color.White;
            // Add the view to the artwork's view collection
            artwork.ViewArray.Add(view);

            // ------------------------------------------------------------
            // 5. Create the 3D annotation on the desired page/rectangle
            // ------------------------------------------------------------
            Page page = doc.Pages[1];
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);
            // Activation enum – show the model when the page is opened
            PDF3DAnnotation annotation = new PDF3DAnnotation(page, rect, artwork, PDF3DActivation.activeWhenOpen);
            // Set the default view (the one we just added at index 0)
            annotation.SetDefaultViewIndex(0);

            // Add the annotation to the page
            page.Annotations.Add(annotation);

            // ------------------------------------------------------------
            // 6. Save the modified PDF
            // ------------------------------------------------------------
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"3D annotation with custom lighting saved to '{outputPdfPath}'.");
    }
}
