using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths for the output PDF and the 3D model file (U3D, PRC, etc.)
        const string outputPdfPath = "3d_annotation.pdf";
        const string modelPath     = "model.u3d";

        // Ensure the 3D model file exists
        if (!File.Exists(modelPath))
        {
            Console.Error.WriteLine($"3D model not found: {modelPath}");
            return;
        }

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define the rectangle where the 3D annotation will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 400, 500, 800);

            // Load the 3D model using the file path (PDF3DContent expects a path or stream)
            PDF3DContent content = new PDF3DContent(modelPath);

            // Create a custom lighting scheme.
            // Using the predefined CAD scheme; alternatively, a custom name can be passed.
            PDF3DLightingScheme lighting = PDF3DLightingScheme.CAD;

            // Choose a render mode that provides shaded illustration
            PDF3DRenderMode renderMode = PDF3DRenderMode.ShadedIllustration;

            // Create the 3D artwork with the content, lighting scheme, and render mode
            PDF3DArtwork artwork = new PDF3DArtwork(doc, content, lighting, renderMode);

            // Create the 3D annotation and attach it to the page
            PDF3DAnnotation annotation = new PDF3DAnnotation(page, rect, artwork);

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(annotation);

            // Save the PDF (no SaveOptions needed for PDF output)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with 3D annotation saved to '{outputPdfPath}'.");
    }
}
