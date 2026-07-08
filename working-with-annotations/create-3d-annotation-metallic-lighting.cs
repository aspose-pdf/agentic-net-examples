using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string outputPdfPath = "output_with_3d.pdf";
        const string model3dPath   = "model.u3d";   // 3‑D artwork file (U3D/PRC)

        // Verify the 3‑D model file exists
        if (!File.Exists(model3dPath))
        {
            Console.Error.WriteLine($"3‑D model not found: {model3dPath}");
            return;
        }

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define the rectangle where the 3‑D annotation will appear
            // (left, bottom, width, height) – adjust coordinates as required
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 400, 300, 600);

            // Load the 3‑D content from the external file
            PDF3DContent content = new PDF3DContent(model3dPath);

            // Choose a lighting scheme that gives a metallic look (e.g., Hard)
            PDF3DLightingScheme lighting = PDF3DLightingScheme.Hard;

            // Choose a render mode that provides shaded illustration
            PDF3DRenderMode renderMode = PDF3DRenderMode.ShadedIllustration;

            // Create the 3‑D artwork with the content, lighting, and render mode
            PDF3DArtwork artwork = new PDF3DArtwork(doc, content, lighting, renderMode);

            // Optionally, you can modify the artwork's lighting or render mode later:
            // artwork.LightingScheme = PDF3DLightingScheme.Artwork;
            // artwork.RenderMode = PDF3DRenderMode.ShadedVertices;

            // Create the 3‑D annotation and attach it to the page
            PDF3DAnnotation annotation = new PDF3DAnnotation(page, rect, artwork);

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(annotation);

            // Save the resulting PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with 3‑D annotation saved to '{outputPdfPath}'.");
    }
}