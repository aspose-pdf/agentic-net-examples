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

        // Load the PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPath))
        {
            // -----------------------------------------------------------------
            // 1. Add a new TextAnnotation with a unique name
            // -----------------------------------------------------------------
            // Fully qualified rectangle to avoid ambiguity
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            TextAnnotation txtAnn = new TextAnnotation(doc.Pages[1], rect)
            {
                Name     = "CustomMetaAnn",          // unique identifier
                Title    = "Note Title",
                Contents = "This annotation carries custom metadata.",
                // Use the Subject property to store a custom key/value pair
                Subject  = "CustomKey=CustomValue"
            };
            // Add the annotation to the page
            doc.Pages[1].Annotations.Add(txtAnn);

            // -----------------------------------------------------------------
            // 2. Use PdfAnnotationEditor to modify the annotation dictionary
            //    (the editor works with the underlying PDF structure)
            // -----------------------------------------------------------------
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Bind the same document instance to the editor
                editor.BindPdf(doc);
                // Modify the annotation on page 1 (page indexes are 1‑based)
                // The editor will update the fields present in the provided annotation object.
                editor.ModifyAnnotations(1, 1, txtAnn);
                // No explicit close needed; using will dispose.
            }

            // -----------------------------------------------------------------
            // 3. Add custom document‑level metadata (optional, demonstrates Facades usage)
            // -----------------------------------------------------------------
            using (PdfFileInfo fileInfo = new PdfFileInfo())
            {
                fileInfo.BindPdf(doc);
                fileInfo.SetMetaInfo("CustomDocMeta", "DocMetaValue");
                // Save changes to the same document instance
                fileInfo.SaveNewInfo(outputPath);
            }

            // -----------------------------------------------------------------
            // 4. Save the modified PDF (lifecycle rule: save inside using)
            // -----------------------------------------------------------------
            // If the document was already saved via PdfFileInfo, this call will
            // simply write any remaining changes.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with custom annotation metadata to '{outputPath}'.");
    }
}