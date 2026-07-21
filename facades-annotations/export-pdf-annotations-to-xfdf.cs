using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations; // needed for AnnotationType enum

class ExportAnnotationsExample
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        // ---------------------------------------------------------------------
        // 1. Create a sample PDF with a couple of pages and some annotations.
        //    This guarantees that the file exists in the sandbox and that there
        //    are annotations to export.
        // ---------------------------------------------------------------------
        using (Document seed = new Document())
        {
            // Add two pages.
            seed.Pages.Add();
            seed.Pages.Add();

            // Add a simple text annotation on page 1.
            TextAnnotation txtAnn1 = new TextAnnotation(seed.Pages[1], new Rectangle(100, 600, 200, 650))
            {
                Title = "Note 1",
                Contents = "First page annotation",
                Color = Color.Yellow
            };
            seed.Pages[1].Annotations.Add(txtAnn1);

            // Add a simple text annotation on page 2.
            TextAnnotation txtAnn2 = new TextAnnotation(seed.Pages[2], new Rectangle(100, 500, 200, 550))
            {
                Title = "Note 2",
                Contents = "Second page annotation",
                Color = Color.Cyan
            };
            seed.Pages[2].Annotations.Add(txtAnn2);

            // Save the seed PDF so the later code can load it.
            seed.Save(pdfPath);
        }

        // ---------------------------------------------------------------------
        // 2. Export annotations from pages 1‑2 to an XFDF memory stream.
        // ---------------------------------------------------------------------
        using (MemoryStream xfdfStream = new MemoryStream())
        {
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Bind the freshly created PDF.
                editor.BindPdf(pdfPath);

                // Export all annotation types from pages 1 to 2.
                string[] allTypes = Enum.GetNames(typeof(AnnotationType));
                editor.ExportAnnotationsXfdf(xfdfStream, 1, 2, allTypes);
            }

            // Reset the stream position for reading.
            xfdfStream.Position = 0;

            // Optional: display the XFDF content.
            using (StreamReader reader = new StreamReader(xfdfStream))
            {
                string xfdfContent = reader.ReadToEnd();
                Console.WriteLine("Exported XFDF:");
                Console.WriteLine(xfdfContent);
            }
        }
    }
}
