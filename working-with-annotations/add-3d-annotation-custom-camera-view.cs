using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing; // Required for Matrix3D

class Program
{
    static void Main()
    {
        const string modelPath = "model.u3d";          // Path to the 3‑D model file
        const string outputPath = "product3d.pdf";    // Output PDF file

        if (!File.Exists(modelPath))
        {
            Console.Error.WriteLine($"3‑D model not found: {modelPath}");
            return;
        }

        // Create a new PDF document
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document())
        {
            // Add a page that will host the 3‑D annotation
            Aspose.Pdf.Page page = doc.Pages.Add();

            // Define the rectangle area for the annotation (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // Load the 3‑D content from the model file
            PDF3DContent content = new PDF3DContent(modelPath);

            // Create the 3‑D artwork that holds the content
            PDF3DArtwork artwork = new PDF3DArtwork(doc, content);

            // Create the 3‑D annotation and attach it to the page
            PDF3DAnnotation annotation = new PDF3DAnnotation(page, rect, artwork);

            // -----------------------------------------------------------------
            // Define a custom camera view (position + orbit) for the 3‑D model
            // -----------------------------------------------------------------
            // Camera position matrix – default constructor creates an identity matrix.
            // For a real scenario you would populate the matrix with translation/rotation values.
            Matrix3D cameraPosition = new Matrix3D();

            // Camera orbit (distance from the model). Adjust as needed.
            double cameraOrbit = 0.0;

            // Create a view with a custom name
            PDF3DView customView = new PDF3DView(
                doc,
                cameraPosition,
                cameraOrbit,
                "CustomPerspective");

            // Set the render mode for the view (e.g., shaded illustration)
            customView.RenderMode = PDF3DRenderMode.ShadedIllustration;

            // Add the view to the artwork's view collection
            artwork.ViewArray.Add(customView);

            // Make the newly added view the default view for the annotation
            annotation.SetDefaultViewIndex(0);

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(annotation);

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"3‑D PDF created: {outputPath}");
    }
}
