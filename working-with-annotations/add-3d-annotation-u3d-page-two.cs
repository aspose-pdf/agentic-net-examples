using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";      // source PDF
        const string outputPdf  = "output.pdf";     // result PDF
        const string u3dModel   = "model.u3d";      // U3D file

        // Ensure the source PDF exists
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the U3D model exists
        if (!System.IO.File.Exists(u3dModel))
        {
            Console.Error.WriteLine($"U3D model not found: {u3dModel}");
            return;
        }

        // Open the PDF document (lifecycle: using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Load the 3‑D content from the U3D file
            PDF3DContent content = new PDF3DContent();
            content.LoadAsU3D(u3dModel);

            // Create the 3‑D artwork (default lighting and render mode)
            PDF3DArtwork artwork = new PDF3DArtwork(doc, content);

            // Define a view for the 3‑D model (identity matrix, zero orbit)
            // Matrix3D constructor requires 12 parameters: 9 for rotation/scale and 3 for translation.
            // Identity matrix with zero translation is (1,0,0, 0,1,0, 0,0,1, 0,0,0).
            Matrix3D cameraPos = new Matrix3D(
                1, 0, 0,
                0, 1, 0,
                0, 0, 1,
                0, 0, 0);
            PDF3DView view = new PDF3DView(doc, cameraPos, 0.0, "InitialView");

            // Add the view to the artwork's view array
            artwork.ViewArray.Add(view);

            // Get page 2 (Aspose.Pdf uses 1‑based indexing)
            Page pageTwo = doc.Pages[2];

            // Define the annotation rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 400, 400, 700);

            // Create the 3‑D annotation on page two
            PDF3DAnnotation annotation = new PDF3DAnnotation(pageTwo, rect, artwork);

            // Set the default view to the first (and only) view we added (index 0)
            annotation.SetDefaultViewIndex(0);

            // Add the annotation to the page's annotation collection
            pageTwo.Annotations.Add(annotation);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"3D annotation added and saved to '{outputPdf}'.");
    }
}
