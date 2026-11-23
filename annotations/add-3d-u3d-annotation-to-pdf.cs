using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing; // Point3D, Matrix3D, Border, etc.

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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Ensure the document has at least 7 pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 7)
            {
                Console.Error.WriteLine("The document has fewer than 7 pages.");
                return;
            }

            // Get page 7
            Page page = doc.Pages[7];

            // Define the annotation rectangle (lower‑left x,y and upper‑right x,y)
            var rect = new Aspose.Pdf.Rectangle(100, 400, 400, 700);

            // ---------- 3D content, artwork and annotation chain ----------
            // Load the U3D file into a PDF3DContent object
            PDF3DContent content = new PDF3DContent(u3dModelPath);
            content.LoadAsU3D(u3dModelPath);

            // Define lighting scheme and render mode (optional, can be changed as needed)
            PDF3DLightingScheme lighting = new PDF3DLightingScheme(LightingSchemeType.CAD);
            PDF3DRenderMode render = new PDF3DRenderMode(RenderModeType.Solid);

            // Create the artwork that ties the document, content, lighting and render mode together
            PDF3DArtwork artwork = new PDF3DArtwork(doc, content, lighting, render);

            // Create a default view for the 3‑D model (using a simple identity matrix)
            // The constructor requires a Document, a Matrix3D, a view distance and a view name.
            PDF3DView view = new PDF3DView(doc, new Matrix3D(), 100.0, "DefaultView");
            artwork.ViewArray.Add(view);

            // Create the 3D annotation on the target page with activation on page open
            PDF3DAnnotation pdf3d = new PDF3DAnnotation(page, rect, artwork, PDF3DActivation.activeWhenOpen);

            // Set the default view index (0 = the first view we just added)
            pdf3d.SetDefaultViewIndex(0);

            // Set a solid border of width 2 (Border.Width expects an int)
            pdf3d.Border = new Border(pdf3d) { Width = 2 };

            // Add the annotation to the page
            page.Annotations.Add(pdf3d);

            // Ensure the 3‑D stream is written to the document
            PDF3DStream stream = new PDF3DStream(doc, artwork);

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with 3D annotation saved to '{outputPdfPath}'.");
    }
}
