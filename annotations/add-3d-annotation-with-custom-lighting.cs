using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string modelPath = "model.u3d"; // 3D model file (U3D/PRC)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        if (!File.Exists(modelPath))
        {
            Console.Error.WriteLine($"3D model file not found: {modelPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 8 pages
            if (doc.Pages.Count < 8)
            {
                Console.Error.WriteLine("The document does not contain page 8.");
                return;
            }

            // Get page 8 (1‑based indexing)
            Page page = doc.Pages[8];

            // Define the annotation rectangle (llx, lly, urx, ury)
            Rectangle rect = new Rectangle(100, 500, 400, 800);

            // ---------- 3D annotation setup ----------
            // 1. 3D content (U3D/PRC file)
            PDF3DContent content = new PDF3DContent(modelPath);

            // 2. Lighting scheme – Aspose.Pdf supports only preset schemes.
            //    A "custom" scheme is not available; we use a preset that mimics a custom look.
            PDF3DLightingScheme lighting = new PDF3DLightingScheme(LightingSchemeType.Headlamp);

            // 3. Render mode – solid rendering with optional colour/opacity adjustments.
            PDF3DRenderMode renderMode = new PDF3DRenderMode(RenderModeType.Solid);
            renderMode.SetFaceColor(Color.Silver);
            renderMode.SetOpacity(0.9);

            // 4. Artwork – combines content, lighting and render mode.
            PDF3DArtwork artwork = new PDF3DArtwork(doc, content, lighting, renderMode);

            // 5. Default view – required for the annotation. Use an identity matrix as a simple camera.
            PDF3DView defaultView = new PDF3DView(doc, new Matrix3D(), 100.0, "Default");
            artwork.ViewArray.Add(defaultView);

            // 6. Create the annotation. Activation set to visible when the page is shown.
            PDF3DAnnotation pdf3DAnnotation = new PDF3DAnnotation(page, rect, artwork, PDF3DActivation.activeWhenVisible);

            // Add the annotation to the page
            page.Annotations.Add(pdf3DAnnotation);
            // ------------------------------------------

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with 3D annotation to '{outputPath}'.");
    }
}
