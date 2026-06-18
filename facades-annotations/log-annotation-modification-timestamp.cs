using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_modified.pdf";

        // Ensure the source file exists
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF to the annotation editor facade
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);

            // Determine the total number of pages (1‑based indexing)
            int pageCount = editor.Document.Pages.Count;

            // Create a dummy rectangle – the actual rectangle is not used when
            // calling ModifyAnnotations; only the properties of the template are
            // applied to existing annotations.
            Aspose.Pdf.Rectangle dummyRect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);

            // The TextAnnotation constructor requires a Page and a Rectangle.
            // Use the first page just to satisfy the constructor; the page is not
            // stored in the template after the call to ModifyAnnotations.
            Page firstPage = editor.Document.Pages[1];
            TextAnnotation template = new TextAnnotation(firstPage, dummyRect)
            {
                Modified = DateTime.Now,          // set the modification timestamp
                Title    = "Updated",            // optional: indicate change source
                Contents = "Annotation modified" // optional: note the change
            };

            // Apply the modification to all pages.
            editor.ModifyAnnotations(1, pageCount, template);

            // Log the operation
            Console.WriteLine($"Modified annotations on pages 1‑{pageCount} with timestamp {template.Modified:u}");

            // Save the updated PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Saved modified PDF to '{outputPath}'.");
    }
}
