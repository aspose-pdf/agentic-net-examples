using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing; // for Color, Matrix3D, Rectangle

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";   // existing PDF
        const string outputPdfPath = "output.pdf"; // result PDF
        const string modelU3dPath = "model.u3d";   // 3D model file (U3D or PRC)

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(modelU3dPath))
        {
            Console.Error.WriteLine($"3D model file not found: {modelU3dPath}");
            return;
        }

        // Load the PDF
        using (Document doc = new Document(inputPdfPath))
        {
            // Ensure at least one page exists
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            Page page = doc.Pages[1];

            // Rectangle where the 3D annotation will be placed
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // ----- 3D content setup -------------------------------------------------
            // 1. 3D model content
            PDF3DContent content = new PDF3DContent(modelU3dPath);

            // 2. Lighting scheme (preset – e.g., Headlamp for realistic illumination)
            PDF3DLightingScheme lighting = new PDF3DLightingScheme(LightingSchemeType.Headlamp);

            // 3. Render mode with material‑like settings
            // Use a valid RenderModeType value (e.g., Solid). Shaded is not a valid enum member.
            PDF3DRenderMode render = new PDF3DRenderMode(RenderModeType.Solid);
            render.SetFaceColor(Color.Silver);          // base colour of the model
            render.SetOpacity(0.85);                    // slight transparency for depth cueing
            render.SetAuxiliaryColour(Color.Gray);      // auxiliary colour (used for edges, etc.)
            render.SetCreaseValue(0.5);                 // controls sharpness of edges (simulates shininess)

            // 4. Assemble the artwork (model + lighting + render mode)
            PDF3DArtwork artwork = new PDF3DArtwork(doc, content, lighting, render);
            // Assign the render mode to the artwork (required for the settings to take effect)
            artwork.RenderMode = render;

            // 5. Define a default view (camera matrix, distance, name)
            Matrix3D camera = new Matrix3D(); // identity matrix – default view
            PDF3DView view = new PDF3DView(doc, camera, 100.0, "DefaultView");
            artwork.ViewArray.Add(view);

            // ----- Annotation creation ----------------------------------------------
            // Activation determines when the 3D content becomes active (open, visible, etc.)
            PDF3DAnnotation annotation = new PDF3DAnnotation(page, rect, artwork, PDF3DActivation.activeWhenOpen);
            // Set the first view as the default one shown when the annotation is opened
            annotation.SetDefaultViewIndex(0);

            // Add the annotation to the page
            page.Annotations.Add(annotation);

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"3D annotation added and saved to '{outputPdfPath}'.");
    }
}
