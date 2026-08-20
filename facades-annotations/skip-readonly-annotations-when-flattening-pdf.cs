using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class BatchAnnotationFlattener
{
    // Configuration option – set to true to skip flattening of read‑only annotations.
    private const bool SkipReadOnlyAnnotations = true;

    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the annotation editor facade and bind the source PDF.
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputPath);

        // Access the underlying Document to work with pages and annotations.
        Document doc = editor.Document;

        // Iterate over all pages and their annotations.
        foreach (Page page in doc.Pages)
        {
            // Collect annotations to avoid modifying the collection while iterating.
            var annotations = new Annotation[page.Annotations.Count];
            for (int i = 1; i <= page.Annotations.Count; i++)
                annotations[i - 1] = page.Annotations[i];

            foreach (Annotation ann in annotations)
            {
                // If the configuration says to skip read‑only annotations, check the flag.
                if (SkipReadOnlyAnnotations && ann.Flags.HasFlag(AnnotationFlags.ReadOnly))
                {
                    // Skip this annotation – it is marked as read‑only.
                    continue;
                }

                // Flatten the annotation: its appearance becomes part of the page content
                // and the annotation object is removed.
                ann.Flatten();
            }
        }

        // Save the modified PDF. The Save method belongs to PdfAnnotationEditor.
        editor.Save(outputPath);

        // Release resources held by the facade.
        editor.Close();

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
