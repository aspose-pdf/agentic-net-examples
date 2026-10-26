using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_3d.pdf";
        const string u3dPath = "model.u3d";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(u3dPath))
        {
            Console.Error.WriteLine($"U3D model not found: {u3dPath}");
            return;
        }

        using (Document doc = new Document(inputPdf))
        {
            // Ensure the document has at least two pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("Document contains fewer than two pages.");
                return;
            }

            // Define the rectangle where the 3D annotation will appear (llx, lly, urx, ury)
            // Use Aspose.Pdf.Rectangle for annotation bounds (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 400, 400, 700);

            // ------------------------------------------------------------
            // Build the 3D annotation chain (Content -> Artwork -> View -> Annotation)
            // ------------------------------------------------------------

            // 1. Load the U3D model into PDF3DContent
            PDF3DContent content = new PDF3DContent();
            content.LoadAsU3D(u3dPath);

            // 2. Choose a lighting scheme and render mode
            PDF3DLightingScheme lighting = new PDF3DLightingScheme(LightingSchemeType.CAD);
            PDF3DRenderMode renderMode = new PDF3DRenderMode(RenderModeType.Solid);

            // 3. Create the artwork that ties content, lighting and render mode together
            PDF3DArtwork artwork = new PDF3DArtwork(doc, content, lighting, renderMode);

            // 4. Define an initial view for the 3D annotation
            //    The view constructor requires a camera matrix, an orbit distance and a name.
            Matrix3D cameraMatrix = new Matrix3D(); // identity matrix – you can customise if needed
            PDF3DView view = new PDF3DView(doc, cameraMatrix, 100.0, "InitialView");
            // Optional: customise view properties (background colour, etc.)
            view.BackGroundColor = Aspose.Pdf.Color.White;
            // Add the view to the artwork's view collection
            artwork.ViewArray.Add(view);

            // 5. Create the annotation on page 2, linking the artwork and specifying activation behaviour
            PDF3DAnnotation pdf3d = new PDF3DAnnotation(doc.Pages[2], rect, artwork, PDF3DActivation.activeWhenOpen);
            // Set the view we just created as the default view
            pdf3d.SetDefaultViewIndex(0);

            // Add the annotation to the page
            doc.Pages[2].Annotations.Add(pdf3d);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"3D annotation added and saved to '{outputPdf}'.");
    }
}
