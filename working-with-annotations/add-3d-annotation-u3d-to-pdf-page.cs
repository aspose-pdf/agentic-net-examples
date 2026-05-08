using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_with_3d.pdf";
        const string u3dFile = "model.u3d";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(u3dFile))
        {
            Console.Error.WriteLine($"U3D file not found: {u3dFile}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Ensure page 2 exists; add blank pages if necessary
            Page page;
            if (doc.Pages.Count >= 2)
                page = doc.Pages[2];
            else
            {
                while (doc.Pages.Count < 2)
                    doc.Pages.Add();
                page = doc.Pages[2];
            }

            // Load the U3D model into a PDF3DContent object
            PDF3DContent content = new PDF3DContent();
            content.LoadAsU3D(u3dFile);

            // Create the 3D artwork that will hold the content
            PDF3DArtwork artwork = new PDF3DArtwork(doc, content);

            // Define the rectangle where the annotation will be placed (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 400, 400);

            // Create the 3D annotation on page 2
            PDF3DAnnotation annotation = new PDF3DAnnotation(page, rect, artwork);

            // Create an initial view for the 3D model
            // Use an identity matrix for camera position and zero orbit as a simple default
            Matrix3D cameraPos = new Matrix3D(); // identity matrix
            double cameraOrbit = 0.0;
            PDF3DView view = new PDF3DView(doc, cameraPos, cameraOrbit, "DefaultView");

            // Add the view to the annotation's view array
            annotation.ViewArray.Add(view);

            // Set the first (and only) view as the default view (index is zero‑based)
            annotation.SetDefaultViewIndex(0);

            // Attach the annotation to the page
            page.Annotations.Add(annotation);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"3D annotation added and saved to '{outputPdf}'.");
    }
}