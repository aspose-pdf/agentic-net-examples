using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "3d_annotation.pdf";
        const string modelPath = "model.u3d"; // Path to a U3D/PRC 3‑D model file

        // Verify that the 3‑D model file exists
        if (!File.Exists(modelPath))
        {
            Console.Error.WriteLine($"3‑D model not found: {modelPath}");
            return;
        }

        // === CREATE: new PDF document ===
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // === LOAD: 3‑D content into PDF3DContent ===
            // PDF3DContent expects a file path (string) or a byte array, not a Stream.
            PDF3DContent content = new PDF3DContent(modelPath);

            // === CREATE: 3‑D artwork with realistic lighting and shading ===
            // Use the "Artwork" lighting scheme (simulates realistic material)
            // and the "ShadedIllustration" render mode for proper shading.
            PDF3DArtwork artwork = new PDF3DArtwork(
                doc,
                content,
                PDF3DLightingScheme.Artwork,      // lighting scheme
                PDF3DRenderMode.ShadedIllustration // render mode
            );

            // Define the rectangle where the annotation will appear (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 400, 400, 700);

            // === CREATE: 3‑D annotation and attach the artwork ===
            PDF3DAnnotation annotation = new PDF3DAnnotation(page, rect, artwork);

            // Optional visual tweaks: border and background color
            annotation.Border = new Border(annotation) { Width = 1 };
            annotation.Color = Aspose.Pdf.Color.LightGray;

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(annotation);

            // === SAVE: write the PDF to disk ===
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with 3‑D annotation saved to '{outputPath}'.");
    }
}
