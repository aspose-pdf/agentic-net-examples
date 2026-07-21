using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath   = "template.pdf";   // existing PDF to host the annotation
        const string outputPdfPath  = "3d_annotation.pdf";
        const string modelFilePath  = "model.u3d";      // 3‑D file (U3D/PRC) that contains textures

        // Ensure source files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(modelFilePath))
        {
            Console.Error.WriteLine($"3‑D model file not found: {modelFilePath}");
            return;
        }

        // Load the existing PDF document (lifecycle: load)
        using (Document doc = new Document(inputPdfPath))
        {
            // Create the 3‑D content from the external file (embeds textures automatically)
            PDF3DContent content = new PDF3DContent(modelFilePath);

            // Create the 3‑D artwork; associate it with the document and the content
            PDF3DArtwork artwork = new PDF3DArtwork(doc, content);

            // Adjust rendering for realistic shading (material‑aware rendering)
            artwork.RenderMode = PDF3DRenderMode.ShadedIllustration; // alternative: ShadedVertices

            // Optionally, customize the lighting scheme (default is acceptable for most cases)
            // artwork.LightingScheme = new PDF3DLightingScheme(); // uncomment and configure if needed

            // Define the rectangle on the page where the annotation will appear
            // Note: Aspose.Pdf uses 1‑based page indexing
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 400, 400, 700);

            // Create the 3‑D annotation on the first page
            PDF3DAnnotation annotation = new PDF3DAnnotation(doc.Pages[1], rect, artwork);

            // Add the annotation to the page's annotation collection
            doc.Pages[1].Annotations.Add(annotation);

            // Save the modified PDF (lifecycle: save)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"3‑D annotation created and saved to '{outputPdfPath}'.");
    }
}