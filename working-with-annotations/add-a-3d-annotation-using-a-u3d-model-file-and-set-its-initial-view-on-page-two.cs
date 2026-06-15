using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";      // source PDF
        const string outputPdfPath = "output.pdf";     // result PDF
        const string u3dModelPath  = "model.u3d";      // U3D file

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

        // Load the existing PDF document (lifecycle: load)
        using (Document doc = new Document(inputPdfPath))
        {
            // Ensure the document has at least two pages
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("The document must contain at least two pages.");
                return;
            }

            // Get page 2 (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[2];

            // Define the rectangle where the 3D annotation will appear
            // (left, bottom, right, top) – adjust as needed
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 400, 400, 700);

            // Load the U3D content
            PDF3DContent content = new PDF3DContent();
            content.LoadAsU3D(u3dModelPath);

            // Create the 3D artwork using the loaded content
            PDF3DArtwork artwork = new PDF3DArtwork(doc, content);

            // Create a default view for the 3D model
            // Matrix3D with default (identity) values; cameraOrbit set to 0
            Matrix3D cameraPosition = new Matrix3D(); // identity matrix
            PDF3DView view = new PDF3DView(doc, cameraPosition, 0.0, "DefaultView");

            // Add the view to the artwork's view array
            artwork.ViewArray.Add(view);

            // Create the 3D annotation on page 2
            PDF3DAnnotation annotation = new PDF3DAnnotation(page, rect, artwork);

            // Set the first view as the default view (index 0)
            annotation.SetDefaultViewIndex(0);

            // Add the annotation to the page
            page.Annotations.Add(annotation);

            // Save the modified PDF (lifecycle: save)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"3D annotation added and saved to '{outputPdfPath}'.");
    }
}