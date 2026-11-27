using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Bind the PDF to the annotation editor
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(doc);

            // Remove all existing annotations
            editor.DeleteAnnotations();

            // Define a rectangle for the new annotation (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Get the first page (pages are 1‑based)
            Page page = doc.Pages[1];

            // Create a TextAnnotation on page 1 with the specified rectangle
            TextAnnotation newAnno = new TextAnnotation(page, rect)
            {
                Subject = "New"
            };

            // Add the new annotation directly to the page's annotation collection
            page.Annotations.Add(newAnno);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
