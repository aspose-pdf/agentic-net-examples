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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // -----------------------------------------------------------------
            // 1. Add a sample annotation (TextAnnotation) to demonstrate custom
            //    metadata injection.
            // -----------------------------------------------------------------
            Page page = doc.Pages[1];
            // Fully qualified rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            TextAnnotation txtAnn = new TextAnnotation(page, rect)
            {
                Title    = "Sample Note",
                Contents = "This annotation will carry custom metadata.",
                Color    = Aspose.Pdf.Color.Yellow,
                Open     = true
            };
            page.Annotations.Add(txtAnn);

            // -----------------------------------------------------------------
            // 2. Use PdfAnnotationEditor (Facades API) to modify the annotation.
            //    Since the Facades API only supports a limited set of fields,
            //    we embed custom key/value pairs into the 'Subject' property as
            //    a JSON string. This effectively extends the annotation dictionary.
            // -----------------------------------------------------------------
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Bind the same document instance to the editor
                editor.BindPdf(doc);

                // Prepare custom metadata (key/value) as JSON
                string customMetaJson = "{\"CustomKey\":\"CustomValue\",\"Author\":\"John Doe\"}";

                // Modify annotations on page 1 (page indexes are 1‑based)
                // Parameters: startPage, endPage, Annotation object with modified fields
                // Only the fields listed in ModifyAnnotations are respected.
                // We set the Subject field to carry our custom metadata.
                txtAnn.Subject = customMetaJson; // Subject will hold the JSON payload

                // Apply the modification
                editor.ModifyAnnotations(1, 1, txtAnn);
            }

            // -----------------------------------------------------------------
            // 3. (Optional) Add a document‑level custom property using PdfFileInfo.
            //    This demonstrates how to store additional metadata at the file level.
            // -----------------------------------------------------------------
            using (PdfFileInfo fileInfo = new PdfFileInfo())
            {
                fileInfo.BindPdf(doc);
                fileInfo.SetMetaInfo("DocumentCustomKey", "DocumentCustomValue");
                // No need to call SaveNewInfo; the changes are persisted when the
                // underlying Document is saved.
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with custom annotation metadata to '{outputPath}'.");
    }
}