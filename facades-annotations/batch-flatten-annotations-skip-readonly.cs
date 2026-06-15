using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class BatchAnnotationFlattener
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPdf  = "batch_input.pdf";
        const string outputPdf = "batch_output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Initialize the annotation editor and bind the source PDF
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputPdf);

        // Access the underlying Document object
        Document doc = editor.Document;

        // Iterate through all pages (1‑based indexing)
        foreach (Page page in doc.Pages)
        {
            // Iterate through annotations on the current page (1‑based)
            for (int idx = 1; idx <= page.Annotations.Count; idx++)
            {
                Annotation annotation = page.Annotations[idx];

                // Skip flattening if the annotation is marked as read‑only
                // Read‑only status is indicated by the ReadOnly flag in AnnotationFlags
                bool isReadOnly = (annotation.Flags & AnnotationFlags.ReadOnly) != 0;

                if (!isReadOnly)
                {
                    // Flatten the annotation: its visual content becomes part of the page
                    // and the annotation object is removed.
                    annotation.Flatten();
                }
            }
        }

        // Save the modified PDF using the facade's Save method
        editor.Save(outputPdf);

        // Close the editor (releases the bound document)
        editor.Close();

        Console.WriteLine($"Batch processing completed. Output saved to '{outputPdf}'.");
    }
}