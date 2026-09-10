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
            // 1. Create a new TextAnnotation and add it to the first page
            // -----------------------------------------------------------------
            Page page = doc.Pages[1];

            // Fully qualified rectangle to avoid ambiguity
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the annotation
            TextAnnotation annotation = new TextAnnotation(page, rect)
            {
                Name     = "CustomMetaAnnotation",   // Unique name for later reference
                Title    = "Sample Annotation",
                Contents = "This annotation carries custom metadata.",
                Color    = Aspose.Pdf.Color.Yellow,
                Open     = true
            };

            // Add the annotation to the page
            page.Annotations.Add(annotation);

            // -----------------------------------------------------------------
            // 2. Extend the annotation dictionary with a custom key/value pair
            //    (Aspose.Pdf does not expose a direct API for arbitrary entries,
            //     so we store the custom data as a document‑level meta‑info entry
            //     that is tied to the annotation name.)
            // -----------------------------------------------------------------
            const string customKey   = "CustomMetaAnnotation.CustomField";
            const string customValue = "CustomValue123";

            // Use PdfFileInfo (facade) to set a custom meta‑info entry
            PdfFileInfo fileInfo = new PdfFileInfo();
            fileInfo.BindPdf(doc);
            fileInfo.SetMetaInfo(customKey, customValue);
            fileInfo.Close(); // Close the facade (does not dispose the Document)

            // -----------------------------------------------------------------
            // 3. Optionally modify the annotation via PdfAnnotationEditor
            //    (e.g., change its title or contents after creation)
            // -----------------------------------------------------------------
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(doc);
                // Modify the annotation identified by its Name
                // The ModifyAnnotations method updates standard fields; custom data
                // remains in the document meta‑info set above.
                editor.ModifyAnnotations(1, 1, annotation);
                editor.Close();
            }

            // -----------------------------------------------------------------
            // 4. Save the updated PDF
            // -----------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with custom annotation metadata to '{outputPath}'.");
    }
}