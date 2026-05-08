using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page to host the 3D annotation
            Page page = doc.Pages.Add();

            // Define the rectangle where the 3D annotation will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // Path to the 3D content (U3D or PRC file). The annotation is created only if the file exists.
            string u3dPath = "sample.u3d";
            if (!File.Exists(u3dPath))
            {
                Console.WriteLine($"3D file not found at '{u3dPath}'. The PDF will be created without a 3D annotation.");
                // Save a plain PDF and exit.
                doc.Save("3DMetallicAnnotation.pdf");
                return;
            }

            // Load the 3D content.
            PDF3DContent content = new PDF3DContent(u3dPath);

            // Choose a lighting scheme that gives a realistic metallic look.
            // The CAD lighting scheme provides multiple directional lights.
            PDF3DLightingScheme lightingScheme = PDF3DLightingScheme.CAD;

            // Use a render mode that shows shaded surfaces.
            PDF3DRenderMode renderMode = PDF3DRenderMode.ShadedIllustration;

            // Create the 3D artwork with the content, lighting, and render mode.
            PDF3DArtwork artwork = new PDF3DArtwork(doc, content, lightingScheme, renderMode);

            // Create the 3D annotation and associate it with the page, rectangle, and artwork.
            PDF3DAnnotation annotation = new PDF3DAnnotation(page, rect, artwork);

            // Add the annotation to the page's annotation collection.
            page.Annotations.Add(annotation);

            // Save the resulting PDF.
            doc.Save("3DMetallicAnnotation.pdf");
        }

        Console.WriteLine("PDF with 3D annotation created successfully.");
    }
}
