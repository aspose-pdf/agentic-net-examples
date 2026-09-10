using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // source PDF
        const string outputPdfPath = "output.pdf";        // result PDF
        const string u3dModelPath = "model.u3d";          // U3D file

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

        // Load the existing PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Ensure the document has at least two pages
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("The document must contain at least two pages.");
                return;
            }

            // Load the U3D content
            PDF3DContent content = new PDF3DContent();
            content.LoadAsU3D(u3dModelPath);

            // Create the 3D artwork using the loaded content
            PDF3DArtwork artwork = new PDF3DArtwork(doc, content);

            // Define the rectangle where the annotation will appear (coordinates in points)
            // Here we place it at (100,400) with width 300 and height 300
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 400, 400, 700);

            // Get page two (1‑based indexing)
            Page pageTwo = doc.Pages[2];

            // Create the 3D annotation on page two
            PDF3DAnnotation annotation = new PDF3DAnnotation(pageTwo, rect, artwork);

            // Create an initial view for the 3D annotation
            // Identity matrix for camera position, orbit = 0, view name = "Default"
            // Matrix3D requires 12 parameters (including translation components tx, ty, tz)
            Matrix3D cameraPos = new Matrix3D(
                1, 0, 0,
                0, 1, 0,
                0, 0, 1,
                0, 0, 0); // tx, ty, tz set to zero
            PDF3DView view = new PDF3DView(doc, cameraPos, 0.0, "Default");

            // Add the view to the annotation's view array
            annotation.ViewArray.Add(view);

            // Set the default view index to the first (and only) view added
            annotation.SetDefaultViewIndex(0);

            // Attach the annotation to the page
            pageTwo.Annotations.Add(annotation);

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"3D annotation added and saved to '{outputPdfPath}'.");
    }
}
