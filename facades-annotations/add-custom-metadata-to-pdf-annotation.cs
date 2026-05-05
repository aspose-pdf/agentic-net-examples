using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_custom_meta.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // -----------------------------------------------------------------
            // 1. Add a sample annotation (TextAnnotation) to the first page.
            // -----------------------------------------------------------------
            Page page = doc.Pages[1];
            // Fully qualified rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            TextAnnotation txtAnn = new TextAnnotation(page, rect)
            {
                Title    = "Sample Note",
                Contents = "This annotation will carry custom metadata.",
                Color    = Aspose.Pdf.Color.Yellow
            };
            page.Annotations.Add(txtAnn);

            // -----------------------------------------------------------------
            // 2. Use PdfAnnotationEditor (Facade) to extend the annotation
            //    dictionary with a custom key/value pair.
            //    The facade does not expose a direct method for arbitrary
            //    dictionary entries, but we can embed custom data in the
            //    Subject property (or any other supported field) as a JSON
            //    string. This demonstrates the concept of extending the
            //    annotation metadata before saving.
            // -----------------------------------------------------------------
            // Prepare custom metadata as a JSON string (key/value pairs)
            string customMetaJson = "{\"CustomKey\":\"CustomValue\",\"AuthorId\":\"12345\"}";

            // Store the custom JSON in the Subject property of the annotation we just added
            txtAnn.Subject = customMetaJson;

            using (PdfAnnotationEditor editor = new PdfAnnotationEditor(doc))
            {
                // ModifyAnnotations expects positional arguments: pageIndex (1‑based), pageCount, and the annotation to update.
                // No named parameters are defined for this overload.
                editor.ModifyAnnotations(1, 1, txtAnn);
            }

            // -----------------------------------------------------------------
            // 3. Save the modified PDF (lifecycle rule: use Document.Save)
            // -----------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with custom annotation metadata to '{outputPath}'.");
    }
}
