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
        const string outputPath = "output_custom_metadata.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: wrap in using)
        using (Document doc = new Document(inputPath))
        {
            // -----------------------------------------------------------------
            // 1. Create a new TextAnnotation on the first page
            // -----------------------------------------------------------------
            Page page = doc.Pages[1]; // 1‑based indexing (global rule)
            // Fully qualified rectangle to avoid ambiguity
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            TextAnnotation txtAnn = new TextAnnotation(page, rect)
            {
                Title    = "Note",
                Contents = "This annotation carries custom metadata.",
                Color    = Aspose.Pdf.Color.Yellow,
                // Use the Name property to identify the annotation later
                Name     = "CustomMetaAnn"
            };
            // Add the annotation to the page
            page.Annotations.Add(txtAnn);

            // -----------------------------------------------------------------
            // 2. Extend the annotation dictionary with custom metadata.
            //    Aspose.Pdf does not expose a direct dictionary API for annotations,
            //    but the Subject field can be used to store arbitrary key/value data.
            //    Here we store a JSON string as an example.
            // -----------------------------------------------------------------
            string customMetaJson = "{\"Author\":\"John Doe\",\"ReviewStatus\":\"Approved\"}";

            // Prepare a temporary annotation containing only the fields we want to modify.
            // The Name must match the existing annotation's Name.
            TextAnnotation modifyAnn = new TextAnnotation(page, rect)
            {
                Name    = txtAnn.Name,   // target annotation
                Subject = customMetaJson // custom metadata stored in Subject
            };

            // -----------------------------------------------------------------
            // 3. Use PdfAnnotationEditor (Facades API) to modify the annotation.
            // -----------------------------------------------------------------
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Bind the same document instance to the editor.
                editor.BindPdf(doc);
                // Modify the annotation on page 1 (page range: 1 to 1).
                editor.ModifyAnnotations(1, 1, modifyAnn);
            }

            // -----------------------------------------------------------------
            // 4. Save the updated PDF (lifecycle rule: save inside using)
            // -----------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with custom annotation metadata to '{outputPath}'.");
    }
}