using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string modelPath = "model.u3d"; // 3‑D model file (U3D or PRC)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        if (!File.Exists(modelPath))
        {
            Console.Error.WriteLine($"3D model not found: {modelPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Verify that page 21 exists (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 21)
            {
                Console.Error.WriteLine("The document contains fewer than 21 pages.");
                return;
            }

            // ------------------------------------------------------------
            // Build the 3D annotation chain (content → artwork → annotation)
            // ------------------------------------------------------------
            // 1. Load the 3D content
            PDF3DContent content = new PDF3DContent(modelPath);
            // Explicitly load as U3D (or LoadAsPRC for PRC files)
            content.LoadAsU3D(modelPath);

            // 2. Define lighting and render mode (optional but common)
            PDF3DLightingScheme lighting = new PDF3DLightingScheme(LightingSchemeType.CAD);
            PDF3DRenderMode render = new PDF3DRenderMode(RenderModeType.SolidWireframe);

            // 3. Create the artwork that ties the content with the visual settings
            PDF3DArtwork artwork = new PDF3DArtwork(doc, content, lighting, render);

            // 4. Define the rectangle where the annotation will appear on the page
            //    (lower‑left x, lower‑left y, upper‑right x, upper‑right y) – values are in points.
            //    Width = 200, Height = 50 => urx = 100 + 200, ury = 500 + 50
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // 5. Create the annotation. The activation enum makes the 3D model visible when the page opens.
            PDF3DAnnotation annotation = new PDF3DAnnotation(
                doc.Pages[21],   // target page (1‑based index)
                rect,            // annotation rectangle
                artwork,         // the artwork we just built
                PDF3DActivation.activeWhenOpen);

            // Optional: give the annotation a name (useful for JavaScript or later processing)
            annotation.Name = "3DModel";

            // 6. Add the annotation to the page
            doc.Pages[21].Annotations.Add(annotation);

            // 7. Create a PDF3DStream to embed the artwork data into the PDF (required for the model to be saved)
            PDF3DStream stream = new PDF3DStream(doc, artwork);
            // The stream does not need to be added anywhere; its constructor registers it with the document.

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with 3D annotation saved to '{outputPath}'.");
    }
}
