using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string outputPath = "3d_annotation.pdf";
        const string modelPath   = "model.u3d"; // 3‑D model file (U3D/PRC)

        // Ensure the 3‑D model file exists
        if (!File.Exists(modelPath))
        {
            Console.Error.WriteLine($"3‑D model not found: {modelPath}");
            return;
        }

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Load the 3‑D model into a PDF3DContent object (string overload)
            PDF3DContent content = new PDF3DContent(modelPath);

            // Create a custom lighting scheme – using the built‑in CAD scheme as an example
            // You can also instantiate a new scheme: new PDF3DLightingScheme(LightingSchemeType.CAD)
            PDF3DLightingScheme lightingScheme = PDF3DLightingScheme.CAD;

            // Choose a render mode – ShadedIllustration gives a realistic shaded view
            PDF3DRenderMode renderMode = PDF3DRenderMode.ShadedIllustration;

            // Create the 3‑D artwork with the document, content, lighting scheme and render mode
            PDF3DArtwork artwork = new PDF3DArtwork(doc, content, lightingScheme, renderMode);

            // Define the annotation rectangle (coordinates are in points; lower‑left origin)
            // Here we place the annotation at (100,400) with width 400 and height 400
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 400, 500, 800);

            // Create the 3‑D annotation and attach it to the page
            PDF3DAnnotation annotation = new PDF3DAnnotation(page, rect, artwork);
            page.Annotations.Add(annotation);

            // Save the PDF – no SaveOptions needed because we are saving to PDF format
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with 3‑D annotation saved to '{outputPath}'.");
    }
}
