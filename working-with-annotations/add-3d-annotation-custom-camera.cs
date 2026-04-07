using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_3d.pdf";
        const string modelPath = "model.u3d"; // Path to the 3D model file (U3D or PRC)

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(modelPath))
        {
            Console.Error.WriteLine($"3D model file not found: {modelPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Create 3D content from the external model file
            PDF3DContent content = new PDF3DContent(modelPath);

            // Create a 3D artwork container for the content
            PDF3DArtwork artwork = new PDF3DArtwork(doc, content);

            // Define the rectangle area where the 3D annotation will be placed
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 400, 500, 800);

            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create the 3D annotation on the page
            PDF3DAnnotation annotation = new PDF3DAnnotation(page, rect, artwork);

            // Build a custom camera matrix.
            // This example positions the camera 500 units back on the Z‑axis.
            // Matrix layout (row‑major): [a b c d, e f g h, i j k l, m n o p]
            double[] matrixValues = new double[]
            {
                1, 0, 0, 0,   // first row
                0, 1, 0, 0,   // second row
                0, 0, 1, 0,   // third row
                0, 0, -500, 1 // fourth row (translation component)
            };
            Aspose.Pdf.Matrix3D cameraMatrix = new Aspose.Pdf.Matrix3D(matrixValues);

            // Camera orbit distance (how far the camera is from the object)
            double cameraOrbit = 500;

            // Create a view with the custom camera settings
            PDF3DView view = new PDF3DView(doc, cameraMatrix, cameraOrbit, "CustomView");

            // Add the view to the artwork's view collection
            artwork.ViewArray.Add(view);

            // Set the newly added view as the default view for the annotation
            annotation.SetDefaultViewIndex(0);

            // Optional: set a render mode for the view (e.g., shaded illustration)
            view.RenderMode = PDF3DRenderMode.ShadedIllustration;

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(annotation);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"3D annotation added and saved to '{outputPdf}'.");
    }
}