using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
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
            // Load 3D content from the U3D file
            PDF3DContent content = new PDF3DContent();
            content.LoadAsU3D(u3dFile);

            // Create a 3D artwork object using the loaded content
            PDF3DArtwork artwork = new PDF3DArtwork(doc, content);

            // Get page two and define the annotation rectangle
            Page page = doc.Pages[2];
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 400, 400, 700);

            // Create the 3D annotation on page two
            PDF3DAnnotation annotation = new PDF3DAnnotation(page, rect, artwork);
            page.Annotations.Add(annotation);

            // Create an initial view for the 3D model
            Matrix3D cameraPos = new Matrix3D(); // default camera position
            double cameraOrbit = 0;               // default orbit
            PDF3DView view = new PDF3DView(doc, cameraPos, cameraOrbit, "InitialView");

            // Add the view to the artwork's view array
            artwork.ViewArray.Add(view);

            // Set the default view index (first view = index 0)
            annotation.SetDefaultViewIndex(0);

            // Save the modified PDF with the 3D annotation
            doc.Save(outputPdf);
        }

        Console.WriteLine($"3D annotation added and saved to '{outputPdf}'.");
    }
}