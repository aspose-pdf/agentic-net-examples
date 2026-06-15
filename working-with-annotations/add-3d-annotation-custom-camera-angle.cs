using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;          // 3D annotation types
using Aspose.Pdf.Drawing;             // for Rectangle (if needed for other drawing operations)

class Add3DAnnotation
{
    static void Main()
    {
        // Input PDF, 3‑D model file (U3D/PRC) and output PDF paths
        const string inputPdfPath = "input.pdf";
        const string model3DPath = "model.u3d";   // replace with your 3‑D file
        const string outputPdfPath = "output_with_3d.pdf";

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(model3DPath))
        {
            Console.Error.WriteLine($"3‑D model not found: {model3DPath}");
            return;
        }

        // Load the existing PDF (lifecycle rule: wrap Document in using)
        using (Document doc = new Document(inputPdfPath))
        {
            // ------------------------------------------------------------
            // 1. Prepare the 3‑D content
            // ------------------------------------------------------------
            // PDF3DContent can be created from a file path or a stream containing the U3D/PRC data.
            PDF3DContent content = new PDF3DContent(model3DPath);

            // ------------------------------------------------------------
            // 2. Create the 3‑D artwork that will hold the content
            // ------------------------------------------------------------
            PDF3DArtwork artwork = new PDF3DArtwork(doc, content);

            // ------------------------------------------------------------
            // 3. Define a custom view (camera position + orbit)
            // ------------------------------------------------------------
            // Matrix3D expects a 16‑element double array representing the transformation matrix.
            Matrix3D cameraPos = new Matrix3D(new double[]
            {
                1, 0, 0, 0,   // first row
                0, 1, 0, 0,   // second row
                0, 0, 1, -200, // third row (translate -200 on Z)
                0, 0, 0, 1    // fourth row
            });

            double cameraOrbit = 30.0; // degrees of orbit around the model
            PDF3DView customView = new PDF3DView(doc, cameraPos, cameraOrbit, "CustomPerspective");

            // Optional: set background color of the view
            customView.BackGroundColor = Aspose.Pdf.Color.LightGray;

            // Add the view to the artwork's view collection
            artwork.ViewArray.Add(customView);

            // ------------------------------------------------------------
            // 4. Create the annotation rectangle on the target page
            // ------------------------------------------------------------
            // Use Aspose.Pdf.Rectangle (page coordinates are in points, origin at bottom‑left)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 400, 400, 700);

            // ------------------------------------------------------------
            // 5. Create the 3‑D annotation and attach it to the page
            // ------------------------------------------------------------
            Page page = doc.Pages[1]; // first page (1‑based indexing)
            PDF3DAnnotation annotation = new PDF3DAnnotation(page, rect, artwork);

            // Set a default view index (0 = first view in the array)
            annotation.SetDefaultViewIndex(0);

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(annotation);

            // ------------------------------------------------------------
            // 6. Save the modified PDF
            // ------------------------------------------------------------
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"3‑D annotation added and saved to '{outputPdfPath}'.");
    }
}