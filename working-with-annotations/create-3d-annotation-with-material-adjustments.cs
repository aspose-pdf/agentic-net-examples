using System;
using System.IO;
using Aspose.Pdf;                                   // Core PDF classes
using Aspose.Pdf.Annotations;                       // 3D annotation classes
using Aspose.Pdf.Drawing;                           // For Rectangle (core, not drawing)
using Aspose.Pdf.Text;                              // For Matrix3D (belongs to Text namespace)

// NOTE: No usage of Aspose.Pdf.Facades or System.Drawing.

class Program
{
    static void Main()
    {
        // Paths – adjust as needed.
        const string outputPdfPath = "3d_annotation.pdf";
        const string model3dPath   = "model.u3d";   // 3‑D model file (U3D/PRC)

        // Verify the 3‑D model exists.
        if (!File.Exists(model3dPath))
        {
            Console.Error.WriteLine($"3‑D model not found: {model3dPath}");
            return;
        }

        // Create a new PDF document.
        using (Document doc = new Document())
        {
            // Add a blank page.
            Page page = doc.Pages.Add();

            // -----------------------------------------------------------------
            // 1. Create the 3‑D content from the external file.
            // -----------------------------------------------------------------
            // PDF3DContent has a constructor that accepts the file name.
            PDF3DContent content = new PDF3DContent(model3dPath);

            // -----------------------------------------------------------------
            // 2. Create the 3‑D artwork container.
            //    Use the overload without explicit lighting scheme – defaults are fine.
            // -----------------------------------------------------------------
            PDF3DArtwork artwork = new PDF3DArtwork(doc, content);

            // -----------------------------------------------------------------
            // 3. Adjust material / rendering properties.
            //    PDF3DRenderMode provides methods to set face colour, opacity, etc.
            // -----------------------------------------------------------------
            // Choose a render mode – ShadedIllustration gives a realistic look.
            artwork.RenderMode = PDF3DRenderMode.ShadedIllustration;

            // Example material adjustments:
            artwork.RenderMode.SetFaceColor(Aspose.Pdf.Color.LightGray); // Base colour
            artwork.RenderMode.SetOpacity(0.95);                        // Slight transparency
            artwork.RenderMode.SetCreaseValue(0.5);                     // Edge sharpness

            // -----------------------------------------------------------------
            // 4. Create a view (camera position) for the 3‑D annotation.
            //    Matrix3D defines the camera transformation.
            // -----------------------------------------------------------------
            // Simple identity matrix – you can customize translation/rotation as needed.
            Matrix3D cameraMatrix = new Matrix3D();

            // Camera orbit (distance from the object) – 0 uses default.
            double cameraOrbit = 0;

            PDF3DView view = new PDF3DView(doc, cameraMatrix, cameraOrbit, "DefaultView");
            view.RenderMode = PDF3DRenderMode.ShadedIllustration; // Consistent with artwork
            view.LightingScheme = artwork.LightingScheme;        // Use same lighting

            // -----------------------------------------------------------------
            // 5. Define the rectangle on the page where the annotation will appear.
            // -----------------------------------------------------------------
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 400, 500, 800);

            // -----------------------------------------------------------------
            // 6. Create the 3‑D annotation and attach it to the page.
            // -----------------------------------------------------------------
            PDF3DAnnotation annotation = new PDF3DAnnotation(page, rect, artwork);

            // Optional: set a border colour for visual debugging.
            annotation.Border = new Border(annotation) { Width = 1 };
            annotation.Color = Aspose.Pdf.Color.DarkGray;

            // Add the annotation to the page.
            page.Annotations.Add(annotation);

            // -----------------------------------------------------------------
            // 7. Save the PDF.
            // -----------------------------------------------------------------
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with 3‑D annotation saved to '{outputPdfPath}'.");
    }
}