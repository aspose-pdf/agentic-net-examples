using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing; // Matrix3D

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_3d.pdf";
        const string modelFile = "model.u3d"; // 3‑D model file (U3D/PRC) with embedded textures

        // Verify required files exist
        if (!File.Exists(inputPdf) || !File.Exists(modelFile))
        {
            Console.Error.WriteLine("Missing input PDF or 3D model file.");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Create 3‑D content from the model file (textures are read from the file)
            PDF3DContent content = new PDF3DContent(modelFile);

            // Create the 3‑D artwork; default lighting and render mode are used
            PDF3DArtwork artwork = new PDF3DArtwork(doc, content);

            // Define the annotation rectangle (left, bottom, right, top) in points
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 400, 400, 700);

            // Get the first page where the annotation will be placed
            Page page = doc.Pages[1];

            // Create the 3‑D annotation on the page
            PDF3DAnnotation annotation = new PDF3DAnnotation(page, rect, artwork);

            // Build a view that shows the front of the model
            // Camera positioned at (0,0,1) looking toward the origin, orbit = 0° (front view)
            double[] matrixValues = new double[]
            {
                1, 0, 0, 0,
                0, 1, 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1
            };
            Matrix3D cameraPos = new Matrix3D(matrixValues);
            double cameraOrbit = 0; // front view
            PDF3DView frontView = new PDF3DView(doc, cameraPos, cameraOrbit, "FrontView");

            // Add the view to the artwork's view array
            artwork.ViewArray.Add(frontView);

            // Set the newly added view as the default view (index 0)
            annotation.SetDefaultViewIndex(0);

            // Attach the annotation to the page
            page.Annotations.Add(annotation);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"3D annotation added and saved to '{outputPdf}'.");
    }
}
