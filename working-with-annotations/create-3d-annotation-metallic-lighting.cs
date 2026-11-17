using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string modelPath = "metal_model.u3d"; // U3D file representing a metal object
        const string outputPath = "3d_metal.pdf";

        if (!File.Exists(modelPath))
        {
            Console.Error.WriteLine($"3D model file not found: {modelPath}");
            return;
        }

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define the rectangle where the 3D annotation will appear
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // ------------------------------------------------------------
            // 1. Load the U3D model into a PDF3DContent object
            // ------------------------------------------------------------
            PDF3DContent content = new PDF3DContent(modelPath);
            // Explicitly load as U3D (required for some older versions)
            content.LoadAsU3D(modelPath);

            // ------------------------------------------------------------
            // 2. Define a lighting scheme – use a preset (e.g., Headlamp)
            // ------------------------------------------------------------
            PDF3DLightingScheme lighting = new PDF3DLightingScheme(LightingSchemeType.Headlamp);

            // ------------------------------------------------------------
            // 3. Define a render mode that mimics a metallic material
            // ------------------------------------------------------------
            PDF3DRenderMode render = new PDF3DRenderMode(RenderModeType.Solid);
            // Silver‑like face colour
            render.SetFaceColor(Aspose.Pdf.Color.Silver);
            // Slight opacity to give a subtle depth (0 = fully opaque)
            render.SetOpacity(1.0);
            // Auxiliary colour can be used for specular highlights – light gray
            render.SetAuxiliaryColour(Aspose.Pdf.Color.FromRgb(0.9, 0.9, 0.9));
            // Crease value influences how sharp edges appear; a higher value gives a more "metal" look
            render.SetCreaseValue(0.5);

            // ------------------------------------------------------------
            // 4. Create the artwork that ties content, lighting and render mode together
            // ------------------------------------------------------------
            PDF3DArtwork artwork = new PDF3DArtwork(doc, content, lighting, render);

            // ------------------------------------------------------------
            // 5. Create the 3D annotation – note the required constructor signature
            // ------------------------------------------------------------
            PDF3DAnnotation threeD = new PDF3DAnnotation(page, rect, artwork, PDF3DActivation.activeWhenOpen);

            // Attach the annotation to the page
            page.Annotations.Add(threeD);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with 3D metal annotation saved to '{outputPath}'.");
    }
}
