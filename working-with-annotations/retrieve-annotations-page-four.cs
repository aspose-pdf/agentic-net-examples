using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath = "sample.pdf";

        // ------------------------------------------------------------
        // Create a PDF with at least four pages and add a sample annotation
        // ------------------------------------------------------------
        using (Document doc = new Document())
        {
            // Add four blank pages (Aspose.Pdf pages are 1‑based)
            for (int i = 0; i < 4; i++)
                doc.Pages.Add();

            // Add a TextAnnotation on the fourth page
            Page page4 = doc.Pages[4];
            // Use Aspose.Pdf.Rectangle for annotations (not Aspose.Pdf.Drawing.Rectangle)
            var rect = new Aspose.Pdf.Rectangle(100, 600, 200, 650);
            TextAnnotation txtAnn = new TextAnnotation(page4, rect);
            txtAnn.Contents = "Sample note"; // set the annotation text
            page4.Annotations.Add(txtAnn);

            // Persist the document so we can load it later
            doc.Save(pdfPath);
        }

        // ------------------------------------------------------------
        // Load the PDF and output the types of all annotations on page 4
        // ------------------------------------------------------------
        using (Document doc = new Document(pdfPath))
        {
            if (doc.Pages.Count < 4)
            {
                Console.WriteLine("The document contains fewer than 4 pages.");
                return;
            }

            Page page = doc.Pages[4];
            foreach (Annotation annotation in page.Annotations)
            {
                Console.WriteLine($"Annotation Type: {annotation.AnnotationType}");
            }
        }
    }
}
