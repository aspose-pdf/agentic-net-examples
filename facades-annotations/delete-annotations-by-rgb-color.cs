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
        const string outputPath = "filtered_output.pdf";

        // Target RGB color to match (pure red). Aspose.Pdf.Color expects values in the range 0..1.
        Aspose.Pdf.Color targetColor = Aspose.Pdf.Color.FromRgb(1.0, 0.0, 0.0);

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF to the annotation editor (facade)
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputPath);

        // Access the underlying Document to work with pages and annotations
        Document doc = editor.Document;

        // Iterate through all pages (1‑based indexing)
        for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
        {
            Page page = doc.Pages[pageIndex];
            AnnotationCollection annCollection = page.Annotations;

            // Iterate backwards so that deletions do not affect the loop index
            for (int i = annCollection.Count; i >= 1; i--)
            {
                Annotation annotation = annCollection[i];

                // Compare the annotation's Color with the target RGB value
                if (annotation.Color != null && annotation.Color.Equals(targetColor))
                {
                    // Delete the matching annotation by index
                    annCollection.Delete(i);
                }
            }
        }

        // Save the modified PDF
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Annotations with the specified color have been removed. Saved to '{outputPath}'.");
    }
}
