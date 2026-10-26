using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "metal_3d.pdf";
        const string modelPath = "metal_model.u3d";

        if (!File.Exists(modelPath))
        {
            Console.Error.WriteLine($"3D model file not found: {modelPath}");
            return;
        }

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page to host the annotation
            Page page = doc.Pages.Add();

            // Define the annotation rectangle (lower‑left x,y, upper‑right x,y)
            // Use Aspose.Pdf.Rectangle for annotation bounds (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 400, 500, 800);

            // Load the U3D model into a PDF3DContent object
            PDF3DContent content = new PDF3DContent();
            content.LoadAsU3D(modelPath);

            // Choose a lighting scheme – Headlamp gives a realistic directional light
            PDF3DLightingScheme lighting = new PDF3DLightingScheme(LightingSchemeType.Headlamp);

            // Define a render mode that mimics a metallic surface
            PDF3DRenderMode render = new PDF3DRenderMode(RenderModeType.Solid);
            render.SetFaceColor(Color.Silver);          // base metal colour
            render.SetAuxiliaryColour(Color.Gray);      // secondary colour for subtle shading
            render.SetOpacity(1.0);                     // fully opaque

            // Combine content, lighting and render mode into an artwork object
            PDF3DArtwork artwork = new PDF3DArtwork(doc, content, lighting, render);

            // Create the 3D annotation – activate it when the page opens
            PDF3DAnnotation annotation = new PDF3DAnnotation(page, rect, artwork, PDF3DActivation.activeWhenOpen);

            // Add the annotation to the page
            page.Annotations.Add(annotation);

            // Save the PDF (PDF format is the default)
            doc.Save(outputPath);
        }

        Console.WriteLine($"3D PDF saved to '{outputPath}'.");
    }
}
