using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "3d_annotation.pdf";
        const string modelPath = "model.u3d"; // Path to a U3D or PRC 3‑D model file

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page to host the annotation
            Page page = doc.Pages.Add();

            // Verify that the 3‑D model file exists before creating the annotation
            if (!File.Exists(modelPath))
            {
                Console.WriteLine($"3‑D model file '{modelPath}' not found. PDF will be created without a 3D annotation.");
                doc.Save(outputPath);
                return;
            }

            // Load the 3‑D model using the string‑based constructor (required by this Aspose.Pdf version)
            PDF3DContent content = new PDF3DContent(modelPath);

            // Choose a lighting scheme – here we use the predefined CAD scheme
            PDF3DLightingScheme lightingScheme = PDF3DLightingScheme.CAD;

            // Choose a render mode – ShadedIllustration gives a realistic look
            PDF3DRenderMode renderMode = PDF3DRenderMode.ShadedIllustration;

            // Create the 3‑D artwork with the content, lighting, and render mode
            PDF3DArtwork artwork = new PDF3DArtwork(doc, content, lightingScheme, renderMode);

            // Define the rectangle (in points) where the annotation will appear
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 400, 500, 800);

            // Create the 3‑D annotation and attach it to the page
            PDF3DAnnotation annotation = new PDF3DAnnotation(page, rect, artwork);

            // Optionally set the default view (first view in the view array)
            annotation.SetDefaultViewIndex(0);

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(annotation);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with 3D annotation saved to '{outputPath}'.");
    }
}
