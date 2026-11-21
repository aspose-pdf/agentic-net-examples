using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;
using Aspose.Pdf.Facades; // included as requested

class TextAnnotationTest
{
    static void Main(string[] args)
    {
        // Paths for temporary and final PDFs
        string tempPdfPath = "temp.pdf";
        string finalPdfPath = "annotated.pdf";

        // -------------------------------------------------
        // Create a PDF with a single page and add a text annotation
        // -------------------------------------------------
        using (Document doc = new Document())
        {
            // Add a page
            Page page = doc.Pages.Add();

            // Add some visible content (optional)
            page.Paragraphs.Add(new TextFragment("Sample page"));

            // Define the annotation rectangle (lower‑left X/Y, upper‑right X/Y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the text annotation
            TextAnnotation annotation = new TextAnnotation(page, rect)
            {
                Title    = "Note",
                Contents = "Test annotation",
                Open     = true,
                Icon     = TextIcon.Note
            };

            // Attach the annotation to the page
            page.Annotations.Add(annotation);

            // Save the PDF to a temporary file
            doc.Save(tempPdfPath);
        }

        // -------------------------------------------------
        // Load the PDF and verify the annotation's position
        // -------------------------------------------------
        using (Document doc = new Document(tempPdfPath))
        {
            // Access the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Ensure an annotation exists
            if (page.Annotations.Count == 0)
            {
                Console.WriteLine("FAIL: No annotations found on the page.");
                return;
            }

            // Retrieve the first annotation and cast to TextAnnotation
            TextAnnotation ann = page.Annotations[1] as TextAnnotation;
            if (ann == null)
            {
                Console.WriteLine("FAIL: Annotation is not a TextAnnotation.");
                return;
            }

            // Get the rectangle of the annotation
            Aspose.Pdf.Rectangle r = ann.Rect;

            // Verify that the coordinates match the expected values
            bool isCorrect = Math.Abs(r.LLX - 100) < 0.001 &&
                             Math.Abs(r.LLY - 500) < 0.001 &&
                             Math.Abs(r.URX - 300) < 0.001 &&
                             Math.Abs(r.URY - 550) < 0.001;

            Console.WriteLine(isCorrect
                ? "PASS: Annotation positioned at the expected coordinates."
                : "FAIL: Annotation coordinates do not match the expected values.");

            // Optionally save the verified PDF
            doc.Save(finalPdfPath);
        }

        // Clean up the temporary file
        if (File.Exists(tempPdfPath))
        {
            File.Delete(tempPdfPath);
        }
    }
}