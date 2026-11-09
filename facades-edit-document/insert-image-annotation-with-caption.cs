using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imagePath = "image.jpg";
        const string caption   = "Sample Image Caption";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Bind the existing PDF to the annotation editor (Facades API)
        using (PdfAnnotationEditor annotEditor = new PdfAnnotationEditor())
        {
            annotEditor.BindPdf(inputPdf);

            // Get the first page (page numbers are 1‑based in Aspose.Pdf)
            Page page = annotEditor.Document.Pages[1];

            // -------------------------------------------------
            // 1. Add the image annotation at (50,150)
            // -------------------------------------------------
            // Rectangle: lower‑left (50,150), upper‑right (150,250) – adjust size as needed
            Aspose.Pdf.Rectangle imgRect = new Aspose.Pdf.Rectangle(50, 150, 150, 250);

            // StampAnnotation expects a Stream for the Image property.
            // Wrap the byte[] in a MemoryStream.
            StampAnnotation imgAnn = new StampAnnotation(page, imgRect)
            {
                Title = caption,
                Image = new MemoryStream(File.ReadAllBytes(imagePath))
            };

            // Add the image annotation to the page's annotation collection
            page.Annotations.Add(imgAnn);

            // -------------------------------------------------
            // 2. Add a caption (text annotation) underneath the image
            // -------------------------------------------------
            // Place the caption slightly below the image at (50,130)
            Aspose.Pdf.Rectangle txtRect = new Aspose.Pdf.Rectangle(50, 130, 250, 150);
            TextAnnotation txtAnn = new TextAnnotation(page, txtRect)
            {
                Title    = "Caption",
                Contents = caption,
                Open     = true,
                Icon     = TextIcon.Note
            };

            // Add the text annotation to the page's annotation collection
            page.Annotations.Add(txtAnn);

            // -------------------------------------------------
            // Save the modified PDF
            // -------------------------------------------------
            annotEditor.Save(outputPdf);
        }

        Console.WriteLine($"Image annotation with caption saved to '{outputPdf}'.");
    }
}
