using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class BatchAnnotationProcessor
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        // Configuration: set to true to skip flattening of read‑only annotations
        const bool skipReadOnlyAnnotations = true;

        // ------------------------------------------------------------
        // Create a self‑contained input PDF so the example runs in a sandbox
        // ------------------------------------------------------------
        using (Document seed = new Document())
        {
            // Add a single page
            Page seedPage = seed.Pages.Add();

            // Add a regular (editable) annotation
            TextAnnotation editableAnn = new TextAnnotation(seedPage, new Aspose.Pdf.Rectangle(100, 600, 200, 650))
            {
                Title = "Editable",
                Contents = "This annotation can be flattened."
            };
            seedPage.Annotations.Add(editableAnn);

            // Add a read‑only annotation
            TextAnnotation readOnlyAnn = new TextAnnotation(seedPage, new Aspose.Pdf.Rectangle(100, 500, 200, 550))
            {
                Title = "ReadOnly",
                Contents = "This annotation should be skipped when flattening."
            };
            // Mark it as read‑only
            readOnlyAnn.Flags = AnnotationFlags.ReadOnly;
            seedPage.Annotations.Add(readOnlyAnn);

            // Save the seed PDF to the expected input path
            seed.Save(inputPath);
        }

        // Initialize the annotation editor and bind the source PDF
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);

            // Access the underlying Document to iterate pages and annotations
            Document doc = editor.Document;

            // Iterate through all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Iterate through all annotations on the current page
                // Note: Annotations collection is 1‑based as well
                for (int annIndex = 1; annIndex <= page.Annotations.Count; annIndex++)
                {
                    Annotation annotation = page.Annotations[annIndex];

                    // Skip flattening if the annotation is marked as read‑only and the option is enabled
                    if (skipReadOnlyAnnotations && annotation.Flags.HasFlag(AnnotationFlags.ReadOnly))
                        continue;

                    // Flatten the annotation (places its appearance directly on the page)
                    annotation.Flatten();
                }
            }

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Processing complete. Output saved to '{outputPath}'.");
    }
}
