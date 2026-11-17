using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing; // required for Matrix3D and Color

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output_3d.pdf";
        const string threeDFilePath = "model.u3d"; // U3D file that already contains textures

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(threeDFilePath))
        {
            Console.Error.WriteLine($"3D model file not found: {threeDFilePath}");
            return;
        }

        // Load the existing PDF inside a using block (document‑disposal‑with‑using rule)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create a rectangle where the annotation will appear (use Aspose.Pdf.Rectangle for annotation bounds)
            Aspose.Pdf.Rectangle annotRect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // ------------------------------------------------------------
            // 1. Load the U3D content
            // ------------------------------------------------------------
            PDF3DContent content = new PDF3DContent();
            content.LoadAsU3D(threeDFilePath);

            // 2. Define lighting and render mode (optional but recommended)
            PDF3DLightingScheme lighting = new PDF3DLightingScheme(LightingSchemeType.CAD);
            PDF3DRenderMode render = new PDF3DRenderMode(RenderModeType.Solid);
            render.SetFaceColor(Color.Silver); // example colour

            // 3. Create the artwork that ties content, lighting and render mode together
            PDF3DArtwork artwork = new PDF3DArtwork(pdfDoc, content, lighting, render);

            // 4. Define a default view – front view (identity matrix gives a straight front view)
            //    The constructor is: PDF3DView(Document, Matrix3D, double distance, string name)
            Matrix3D identity = new Matrix3D(); // no rotation, no translation
            PDF3DView frontView = new PDF3DView(pdfDoc, identity, 0.0, "Front");
            artwork.ViewArray.Add(frontView);

            // 5. Create the 3D annotation. Activation is an enum value.
            PDF3DAnnotation threeDAnnot = new PDF3DAnnotation(
                pdfDoc.Pages[1],
                annotRect,
                artwork,
                PDF3DActivation.activeWhenOpen);

            // Set the default view index to the view we just added (index 0)
            threeDAnnot.SetDefaultViewIndex(0);

            // Add the annotation to the page
            pdfDoc.Pages[1].Annotations.Add(threeDAnnot);

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"3D annotation added and saved to '{outputPdfPath}'.");
    }
}
