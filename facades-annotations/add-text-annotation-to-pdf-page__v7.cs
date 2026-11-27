using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

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

                // Define the annotation rectangle (coordinates in points).
                // Rectangle(left, bottom, right, top)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

                // Get the target page (pages are 1‑based).
                Page targetPage = doc.Pages[3];

                // Create the text annotation for the specific page.
                TextAnnotation annotation = new TextAnnotation(targetPage, rect)
                {
                    Title    = "Note",
                    Contents = "This is a text annotation on page 3.",
                    Open     = true // Open the annotation by default.
                };

                // Add/modify the annotation on page 3 using the editor.
                editor.ModifyAnnotations(3, 3, annotation);
            }

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotated PDF saved to '{outputPath}'.");
    }
}
