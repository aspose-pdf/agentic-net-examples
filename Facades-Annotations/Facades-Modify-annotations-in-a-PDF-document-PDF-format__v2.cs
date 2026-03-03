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

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Initialize the annotation editor facade (also disposable)
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Bind the loaded document to the facade
                editor.BindPdf(doc);

                // ------------------------------------------------------------
                // Example 1: Change the author of all annotations on all pages
                // ------------------------------------------------------------
                // Parameters: startPage, endPage, newAuthor, oldAuthor (empty string means any)
                editor.ModifyAnnotationsAuthor(1, doc.Pages.Count, "NewAuthor", string.Empty);

                // ------------------------------------------------------------
                // Example 2: Change color and contents of annotations on all pages
                // ------------------------------------------------------------
                // Create a dummy TextAnnotation with the desired new properties.
                // The rectangle is irrelevant for ModifyAnnotations; it just needs a valid instance.
                Aspose.Pdf.Rectangle dummyRect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);
                TextAnnotation dummyAnnotation = new TextAnnotation(doc.Pages[1], dummyRect)
                {
                    Color = Aspose.Pdf.Color.Red,          // Set new color
                    Contents = "Updated annotation"        // Set new contents
                };

                // Apply the changes to annotations on every page.
                editor.ModifyAnnotations(1, doc.Pages.Count, dummyAnnotation);

                // Save the modified PDF to the specified output file.
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Annotations have been modified and saved to '{outputPath}'.");
    }
}