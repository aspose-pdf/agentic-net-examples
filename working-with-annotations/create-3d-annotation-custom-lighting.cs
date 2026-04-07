using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // PDF3DAnnotation, PDF3DArtwork, PDF3DContent, PDF3DLightingScheme, PDF3DRenderMode

class Program
{
    static void Main()
    {
        const string outputPath = "3d_annotation.pdf";
        const string modelPath   = "model.u3d"; // path to a U3D or PRC 3‑D model file

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

            // Load the 3‑D model into a PDF3DContent object using the file‑path overload
            PDF3DContent content = new PDF3DContent(modelPath);

            // Choose a custom lighting scheme (e.g., CAD) and a render mode
            PDF3DLightingScheme lighting = PDF3DLightingScheme.CAD;
            PDF3DRenderMode renderMode = PDF3DRenderMode.ShadedIllustration;

            // Create the 3‑D artwork with the content, lighting scheme and render mode
            PDF3DArtwork artwork = new PDF3DArtwork(doc, content, lighting, renderMode);

            // Define the rectangle where the annotation will appear (coordinates are in points)
            // Fully qualify to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // Create the 3‑D annotation and attach it to the page
            PDF3DAnnotation annotation = new PDF3DAnnotation(page, rect, artwork);

            // Optionally set a border and a name for the annotation
            annotation.Border = new Border(annotation) { Width = 1 };
            annotation.Name = "My3DModel";

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with 3‑D annotation saved to '{outputPath}'.");
    }
}
