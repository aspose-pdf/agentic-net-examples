using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "output_3d.pdf";
        const string modelFilePath  = "model.u3d"; // 3‑D file that contains textures

        // Ensure source files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(modelFilePath))
        {
            Console.Error.WriteLine($"3‑D model not found: {modelFilePath}");
            return;
        }

        // Load the existing PDF (lifecycle rule: wrap Document in using)
        using (Document doc = new Document(inputPdfPath))
        {
            // Choose the page where the annotation will be placed
            Page page = doc.Pages[1]; // 1‑based indexing

            // Create the 3‑D content from the external file (textures are embedded in the file)
            PDF3DContent content = new PDF3DContent(modelFilePath);

            // Create the 3‑D artwork; default lighting and render mode are used
            PDF3DArtwork artwork = new PDF3DArtwork(doc, content);

            // Define the annotation rectangle (coordinates are in points)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 400, 400, 700);

            // Create the 3‑D annotation and associate it with the artwork
            PDF3DAnnotation annotation = new PDF3DAnnotation(page, rect, artwork);

            // -----------------------------------------------------------------
            // Create a view that shows the front of the model.
            // An identity matrix places the camera directly in front of the object.
            // CameraOrbit = 0 keeps the view orthogonal.
            // -----------------------------------------------------------------
            Matrix3D cameraMatrix = new Matrix3D(
                1, 0, 0, 0,   // first row
                0, 1, 0, 0,   // second row
                0, 0, 1, 0);  // third row (translation = 0)

            PDF3DView frontView = new PDF3DView(doc, cameraMatrix, 0, "FrontView");

            // Add the view to the artwork's view array
            artwork.ViewArray.Add(frontView);

            // Set the newly added view as the default (index 0)
            annotation.SetDefaultViewIndex(0);

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(annotation);

            // Save the modified PDF (lifecycle rule: save inside using block)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"3‑D annotation added and saved to '{outputPdfPath}'.");
    }
}