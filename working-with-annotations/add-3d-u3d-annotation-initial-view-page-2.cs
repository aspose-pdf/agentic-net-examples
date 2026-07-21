using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
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

        // Load the existing PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Ensure the document has at least two pages
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("The PDF must contain at least two pages.");
                return;
            }

            // Get page 2 (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[2];

            // Define the rectangle where the 3D annotation will appear
            // (left, bottom, right, top) – adjust coordinates as required
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 400, 500, 800);

            // Load the U3D content
            PDF3DContent u3dContent = new PDF3DContent(u3dModelPath);

            // Create the 3D artwork that holds the content
            PDF3DArtwork artwork = new PDF3DArtwork(doc, u3dContent);

            // Create an initial view for the 3D model
            // Matrix3D with default (identity) values is sufficient for a basic view
            Matrix3D cameraPosition = new Matrix3D(); // identity matrix
            double cameraOrbit = 0; // default orbit
            PDF3DView initialView = new PDF3DView(doc, cameraPosition, cameraOrbit, "InitialView");

            // Add the view to the artwork's view array
            artwork.ViewArray.Add(initialView);

            // Create the 3D annotation on page 2 with the artwork.
            // Use an activation mode – e.g., active when the page becomes visible.
            PDF3DAnnotation annotation = new PDF3DAnnotation(page, rect, artwork, PDF3DActivation.activeWhenVisible);

            // Set the default view index (0‑based). Since we added only one view, index is 0.
            annotation.SetDefaultViewIndex(0);

            // Add the annotation to the page
            page.Annotations.Add(annotation);

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"3D annotation added and saved to '{outputPdfPath}'.");
    }
}