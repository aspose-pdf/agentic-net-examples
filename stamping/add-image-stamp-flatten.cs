using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Create a sample PDF (self‑contained example)
        using (Document doc = new Document())
        {
            doc.Pages.Add();
            doc.Save("input.pdf");
        }

        // Prepare a simple PNG image for the stamp (1x1 pixel transparent)
        byte[] pngBytes = Convert.FromBase64String(
            "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mP8/x8AAwMCAO+XK6cAAAAASUVORK5CYII=");
        File.WriteAllBytes("stamp.png", pngBytes);

        // Open the PDF and add an image stamp
        using (Document doc = new Document("input.pdf"))
        {
            // Create stamp annotation with an image stream
            StampAnnotation stamp = new StampAnnotation(doc);
            stamp.Rect = new Aspose.Pdf.Rectangle(100, 100, 200, 200);
            // StampAnnotation.Image expects a System.IO.Stream, not Aspose.Pdf.Image
            stamp.Image = new MemoryStream(pngBytes);
            doc.Pages[1].Annotations.Add(stamp);

            // Flatten all annotations on each page
            foreach (Page page in doc.Pages)
            {
                // Copy annotations to an array to avoid modifying the collection while iterating
                Annotation[] annotations = new Annotation[page.Annotations.Count];
                int i = 0;
                foreach (Annotation ann in page.Annotations)
                {
                    annotations[i++] = ann;
                }

                foreach (Annotation ann in annotations)
                {
                    ann.Flatten();
                }
            }

            doc.Save("output.pdf");
        }
    }
}
