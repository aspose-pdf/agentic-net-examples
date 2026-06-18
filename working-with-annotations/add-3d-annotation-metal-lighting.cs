using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, 3D model (U3D/PRC) and output PDF
        const string sourcePdfPath = "source.pdf";
        const string model3dPath   = "model.u3d";   // 3D content file
        const string outputPdfPath = "output_with_3d.pdf";

        // Ensure source files exist
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(model3dPath))
        {
            Console.Error.WriteLine($"3D model file not found: {model3dPath}");
            return;
        }

        // Load the existing PDF document (using rule: document-disposal-with-using)
        using (Document doc = new Document(sourcePdfPath))
        {
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle where the 3D annotation will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 400, 400, 700);

            // Load the 3D content using the overload that accepts a file path (Fix: use‑file‑path‑overload‑for‑PDF3DContent)
            PDF3DContent content = new PDF3DContent(model3dPath);

            // Create a lighting scheme that mimics realistic metal surfaces.
            // The "Artwork" scheme provides balanced lighting; you can switch to "CAD" or "Cube" if needed.
            PDF3DLightingScheme lighting = PDF3DLightingScheme.Artwork;

            // Choose a render mode that shows shaded surfaces (good for metal appearance)
            PDF3DRenderMode renderMode = PDF3DRenderMode.ShadedIllustration;

            // Create the 3D artwork with the document, content, lighting scheme and render mode
            PDF3DArtwork artwork = new PDF3DArtwork(doc, content, lighting, renderMode);

            // Optionally, adjust the artwork's lighting scheme after creation
            artwork.LightingScheme = PDF3DLightingScheme.CAD; // stronger directional lighting

            // Create the 3D annotation and attach it to the page
            PDF3DAnnotation annotation = new PDF3DAnnotation(page, rect, artwork);

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(annotation);

            // Save the modified PDF (using rule: document-disposal-with-using)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"3D annotation added and saved to '{outputPdfPath}'.");
    }
}
