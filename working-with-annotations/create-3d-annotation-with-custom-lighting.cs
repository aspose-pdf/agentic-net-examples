using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string modelPath   = "model.u3d";      // 3‑D model file (U3D/PRC)
        const string outputPath  = "3d_annotation.pdf";

        // Ensure the 3‑D model file exists
        if (!File.Exists(modelPath))
        {
            Console.Error.WriteLine($"3‑D model not found: {modelPath}");
            return;
        }

        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Load the 3‑D content from the model file
            PDF3DContent content = new PDF3DContent(modelPath);

            // Create a custom lighting scheme – using the predefined CAD scheme here
            PDF3DLightingScheme lighting = PDF3DLightingScheme.CAD;

            // Choose a render mode that shows shaded illustration
            PDF3DRenderMode renderMode = PDF3DRenderMode.ShadedIllustration;

            // Build the 3‑D artwork with the content, lighting scheme and render mode
            PDF3DArtwork artwork = new PDF3DArtwork(doc, content, lighting, renderMode);

            // Define the rectangle where the annotation will appear (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // Create the 3‑D annotation and attach it to the page
            PDF3DAnnotation annotation = new PDF3DAnnotation(page, rect, artwork);
            page.Annotations.Add(annotation);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with 3‑D annotation saved to '{outputPath}'.");
    }
}