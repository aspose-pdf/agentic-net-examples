using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

namespace Create3DAnnotationExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Step 1: Create a simple PDF file to work with (self‑contained example)
            using (Document createDoc = new Document())
            {
                // Add a blank page
                createDoc.Pages.Add();
                // Save the temporary PDF
                createDoc.Save("input.pdf");
            }

            // Step 2: Re‑open the PDF and add a 3D annotation
            using (Document doc = new Document("input.pdf"))
            {
                // Create a placeholder 3D file (U3D/PRC). In a real scenario this would be a valid 3D model.
                string u3dPath = "sample.u3d";
                using (FileStream fs = new FileStream(u3dPath, FileMode.Create, FileAccess.Write))
                {
                    // Minimal placeholder content – just to have a file on disk.
                    byte[] placeholder = new byte[] { 0x25, 0x50, 0x44, 0x46 }; // "%PDF"
                    fs.Write(placeholder, 0, placeholder.Length);
                }

                // Load the 3D content from the file
                PDF3DContent content = new PDF3DContent(u3dPath);

                // Create the 3D artwork and set a realistic render mode
                PDF3DArtwork artwork = new PDF3DArtwork(doc, content);
                artwork.RenderMode = PDF3DRenderMode.ShadedIllustration; // Use shaded illustration for realistic lighting

                // Define the annotation rectangle (left, bottom, right, top)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 200, 400, 500);

                // Create the 3D annotation on the first page
                PDF3DAnnotation annotation = new PDF3DAnnotation(doc.Pages[1], rect, artwork);
                // Set the default view (index 0 corresponds to the first view in the view array)
                annotation.SetDefaultViewIndex(0);

                // Add the annotation to the page's annotation collection
                doc.Pages[1].Annotations.Add(annotation);

                // Save the resulting PDF with the 3D annotation
                doc.Save("output.pdf");
            }
        }
    }
}
