using System;
using System.IO;
using Aspose.Pdf;                                 // Core PDF API
using Aspose.Pdf.Annotations;                    // 3D annotation types

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string modelPath   = "model.u3d";          // 3‑D model file
        const string outputPath  = "3d_annotation.pdf";

        // Verify the 3‑D model exists
        if (!File.Exists(modelPath))
        {
            Console.Error.WriteLine($"3‑D model not found: {modelPath}");
            return;
        }

        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define the rectangle where the 3‑D annotation will appear
            // (left, bottom, right, top) – coordinates are in points
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // Load the 3‑D content from the U3D file
            PDF3DContent content = new PDF3DContent(modelPath);

            // Choose a lighting scheme – e.g., CAD lighting
            PDF3DLightingScheme lightingScheme = PDF3DLightingScheme.CAD;

            // Choose a render mode – e.g., ShadedIllustration
            PDF3DRenderMode renderMode = PDF3DRenderMode.ShadedIllustration;

            // Create the 3‑D artwork with the content, lighting, and render mode
            PDF3DArtwork artwork = new PDF3DArtwork(doc, content, lightingScheme, renderMode);

            // Create the 3‑D annotation and attach it to the page
            PDF3DAnnotation annotation = new PDF3DAnnotation(page, rect, artwork);

            // Optionally set a name or other properties
            annotation.Name = "My3DModel";

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(annotation);

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with 3‑D annotation saved to '{outputPath}'.");
    }
}