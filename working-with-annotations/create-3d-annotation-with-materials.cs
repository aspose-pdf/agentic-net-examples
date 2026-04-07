using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string outputPdf = "3d_annotation.pdf";
        const string model3dPath = "model.u3d"; // 3D file containing geometry and textures

        // Verify the 3D model file exists
        if (!File.Exists(model3dPath))
        {
            Console.Error.WriteLine($"3D model not found: {model3dPath}");
            return;
        }

        // Create a new PDF document and add a page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Load the 3D content (U3D/PRC) – this file may already embed textures
            PDF3DContent content = new PDF3DContent(model3dPath);

            // Choose a lighting scheme – using the built‑in "Artwork" scheme
            PDF3DLightingScheme lightingScheme = PDF3DLightingScheme.Artwork;

            // Choose a render mode – "ShadedIllustration" gives realistic shading
            PDF3DRenderMode renderMode = PDF3DRenderMode.ShadedIllustration;

            // Adjust material properties via the render mode
            // Example: set a light gray face color and semi‑transparent appearance
            renderMode.SetFaceColor(Aspose.Pdf.Color.LightGray);
            renderMode.SetOpacity(0.85f); // 85 % opacity

            // Create the 3D artwork that ties the content, lighting, and render mode together
            PDF3DArtwork artwork = new PDF3DArtwork(doc, content, lightingScheme, renderMode);

            // Define the annotation rectangle (position on the page)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 400, 500, 800);

            // Create the 3D annotation and associate it with the artwork
            PDF3DAnnotation annotation = new PDF3DAnnotation(page, rect, artwork);

            // Optionally set the default view (first view in the view array)
            annotation.SetDefaultViewIndex(0);

            // Add the annotation to the page
            page.Annotations.Add(annotation);

            // Save the PDF containing the 3D annotation
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with 3D annotation saved to '{outputPdf}'.");
    }
}