using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;   // 3D annotation related classes

class Program
{
    static void Main()
    {
        // Input PDF (or create a new one) and 3D model file paths
        const string inputPdfPath   = "input.pdf";      // existing PDF or can be a blank PDF
        const string outputPdfPath  = "output_with_3d.pdf";
        const string model3dPath    = "model.u3d";      // path to a U3D or PRC 3D file

        // Ensure the 3D model file exists
        if (!File.Exists(model3dPath))
        {
            Console.Error.WriteLine($"3D model file not found: {model3dPath}");
            return;
        }

        // If the input PDF does not exist, create a simple one with a single page
        if (!File.Exists(inputPdfPath))
        {
            using (Document newDoc = new Document())
            {
                newDoc.Pages.Add();
                newDoc.Save(inputPdfPath);
            }
        }

        // Open the PDF, add the 3D annotation, and save
        using (Document doc = new Document(inputPdfPath))
        {
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // ------------------------------------------------------------
            // 1. Create the 3D content from the external model file
            // ------------------------------------------------------------
            PDF3DContent content = new PDF3DContent(model3dPath);

            // ------------------------------------------------------------
            // 2. Create the 3D artwork that will hold the content
            // ------------------------------------------------------------
            PDF3DArtwork artwork = new PDF3DArtwork(doc, content);
            // Optional: set lighting scheme or render mode if desired
            // artwork.LightingScheme = PDF3DLightingScheme.Headlamp;
            // artwork.RenderMode = PDF3DRenderMode.ShadedIllustration;

            // ------------------------------------------------------------
            // 3. Define a custom camera view (position + orbit)
            // ------------------------------------------------------------
            // Matrix3D represents a 4x4 transformation matrix.
            // Here we create a simple translation matrix as an example.
            // The array must contain 16 values (row‑major order).
            double[] matrixValues = new double[]
            {
                1, 0, 0, 0,   // Row 1
                0, 1, 0, 0,   // Row 2
                0, 0, 1, 0,   // Row 3
                0, 0, 0, 1    // Row 4 (translation part)
            };
            Matrix3D cameraMatrix = new Matrix3D(matrixValues);

            double cameraOrbit = 45.0; // degrees of orbit around the object
            PDF3DView view = new PDF3DView(doc, cameraMatrix, cameraOrbit, "CustomView");

            // Optional: set background color, lighting scheme, render mode, etc.
            // view.BackGroundColor = Aspose.Pdf.Color.LightGray;
            // view.LightingScheme = PDF3DLightingScheme.Headlamp;
            // view.RenderMode = PDF3DRenderMode.ShadedIllustration;

            // Add the view to the artwork's view collection (only one view is allowed)
            artwork.ViewArray.Add(view);

            // ------------------------------------------------------------
            // 4. Create the 3D annotation rectangle on the page
            // ------------------------------------------------------------
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // ------------------------------------------------------------
            // 5. Create the 3D annotation and attach it to the page
            // ------------------------------------------------------------
            PDF3DAnnotation annotation = new PDF3DAnnotation(page, rect, artwork);

            // Set the default view index to 0 (the first and only view we added)
            annotation.SetDefaultViewIndex(0);

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(annotation);

            // ------------------------------------------------------------
            // 6. Save the modified PDF
            // ------------------------------------------------------------
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with 3D annotation saved to '{outputPdfPath}'.");
    }
}