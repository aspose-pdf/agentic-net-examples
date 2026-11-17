using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string u3dModelPath = "model.u3d";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(u3dModelPath))
        {
            Console.Error.WriteLine($"U3D model not found: {u3dModelPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Ensure the document has at least two pages
            if (doc.Pages.Count < 2)
                doc.Pages.Add();

            // Page indexing in Aspose.Pdf is 1‑based; page 2 is the target page
            Page page = doc.Pages[2];

            // Define the rectangle where the 3D annotation will appear
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // ----- Build the 3D annotation chain -----
            // 1. Load the U3D content
            PDF3DContent content = new PDF3DContent(u3dModelPath);
            content.LoadAsU3D(u3dModelPath);

            // 2. Optional lighting and render mode (using defaults works as well)
            PDF3DLightingScheme lighting = new PDF3DLightingScheme(LightingSchemeType.CAD);
            PDF3DRenderMode render = new PDF3DRenderMode(RenderModeType.Solid);

            // 3. Create the artwork that ties the content, lighting and render mode together
            PDF3DArtwork artwork = new PDF3DArtwork(doc, content, lighting, render);

            // 4. Define an initial view for the 3D model
            //    Matrix3D represents the camera transformation; using the default identity matrix here.
            PDF3DView view = new PDF3DView(doc, new Matrix3D(), 100.0, "Default");
            artwork.ViewArray.Add(view);

            // 5. Create the annotation, activating it when the page is opened
            PDF3DAnnotation threeDAnno = new PDF3DAnnotation(page, rect, artwork, PDF3DActivation.activeWhenOpen);
            // Set the default view (index 0 corresponds to the view we just added)
            threeDAnno.SetDefaultViewIndex(0);

            // Add the annotation to the page
            page.Annotations.Add(threeDAnno);

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"3D annotation added and saved to '{outputPdfPath}'.");
    }
}
