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
        const string outputPath = "output_annotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document.
        using (Document doc = new Document(inputPath))
        {
            // Bind the document to the annotation editor.
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(doc);

                // Define the annotation rectangle (llx, lly, urx, ury) in points.
                var rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

                // Get the target page (page numbers are 1‑based).
                Page targetPage = doc.Pages[6];

                // Create a text annotation for the target page.
                var textAnno = new TextAnnotation(targetPage, rect)
                {
                    Subject = "Note",
                    Color = Aspose.Pdf.Color.Blue
                };

                // Add the annotation to page 6 using ModifyAnnotations.
                editor.ModifyAnnotations(6, 6, textAnno);
            }

            // Save the modified PDF.
            doc.Save(outputPath);
        }
    }
}
