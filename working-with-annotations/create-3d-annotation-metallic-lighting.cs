using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string outputPdfPath = "3d_annotation.pdf";
        const string model3dPath   = "model.u3d";   // 3‑D model file (U3D/PRC)

        // Verify the 3‑D model file exists
        if (!File.Exists(model3dPath))
        {
            Console.Error.WriteLine($"3‑D model not found: {model3dPath}");
            return;
        }

        // Create a new PDF document
        var doc = new Document();

        // Add a blank page
        var page = doc.Pages.Add();

        // Load the 3‑D content directly from the file path (PDF3DContent expects a string path)
        var content = new PDF3DContent(model3dPath);

        // Choose a lighting scheme that gives a realistic metallic look
        PDF3DLightingScheme lightingScheme = PDF3DLightingScheme.Hard;

        // Choose a render mode that shades the surface – ShadedIllustration works well for metal.
        PDF3DRenderMode renderMode = PDF3DRenderMode.ShadedIllustration;

        // Create the 3‑D artwork with the content, lighting, and render mode
        var artwork = new PDF3DArtwork(doc, content, lightingScheme, renderMode);

        // Optionally adjust material properties via the render mode (e.g., face color, opacity)
        // Here we set a subtle gray face colour to simulate metal.
        artwork.RenderMode.SetFaceColor(Color.FromRgb(0.7, 0.7, 0.7));
        artwork.RenderMode.SetOpacity(0.9); // Slight transparency for realism

        // Define the rectangle on the page where the 3‑D annotation will appear
        var rect = new Rectangle(100, 400, 500, 800); // llx, lly, urx, ury

        // Create the 3‑D annotation and attach the artwork
        var annotation = new PDF3DAnnotation(page, rect, artwork);

        // Set a border colour for visual distinction (optional)
        annotation.Color = Color.LightGray;

        // Add the annotation to the page
        page.Annotations.Add(annotation);

        // Save the PDF
        doc.Save(outputPdfPath);
        Console.WriteLine($"PDF with 3‑D annotation saved to '{outputPdfPath}'.");
    }
}
