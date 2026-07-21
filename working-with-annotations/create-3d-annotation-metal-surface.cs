using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths for the output PDF and the 3‑D model (U3D or PRC)
        const string outputPdf = "MetalSurface3D.pdf";
        const string modelPath = "metal_surface.u3d";

        // Ensure the 3‑D model file exists
        if (!File.Exists(modelPath))
        {
            Console.Error.WriteLine($"3‑D model not found: {modelPath}");
            return;
        }

        // Create a new PDF document inside a using block (ensures deterministic disposal)
        using (Document doc = new Document())
        {
            // Add a blank page (first page is index 1)
            Page page = doc.Pages.Add();

            // Load the 3‑D content from the model file using the string‑path constructor
            PDF3DContent content = new PDF3DContent(modelPath);

            // Choose a lighting scheme that gives a realistic metal appearance
            // The "Artwork" scheme provides a balanced illumination suitable for reflective surfaces
            PDF3DLightingScheme lighting = PDF3DLightingScheme.Artwork;

            // Use a shaded illustration render mode for realistic shading
            PDF3DRenderMode renderMode = PDF3DRenderMode.ShadedIllustration;

            // Create the 3‑D artwork object with the document, content, lighting, and render mode
            PDF3DArtwork artwork = new PDF3DArtwork(doc, content, lighting, renderMode);

            // Define the rectangle where the 3‑D annotation will appear
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 400, 400, 700);

            // Create the 3‑D annotation and associate it with the page and artwork
            PDF3DAnnotation annotation = new PDF3DAnnotation(page, rect, artwork);

            // Optionally set a background color for the annotation (e.g., dark gray to enhance metal look)
            annotation.Color = Aspose.Pdf.Color.FromRgb(0.2, 0.2, 0.2);

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(annotation);

            // Save the PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with 3‑D metal surface saved to '{outputPdf}'.");
    }
}
