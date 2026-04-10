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
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Step 1: Hide all annotations in the document.
        // Use the Document API (wrapped in a using block for proper disposal).
        string tempPath = Path.Combine(Path.GetDirectoryName(outputPath) ?? ".", "temp_hidden.pdf");
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing).
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Iterate over all annotations on the page (also 1‑based).
                for (int annIndex = 1; annIndex <= page.Annotations.Count; annIndex++)
                {
                    // Fully qualify the Annotation type to avoid ambiguity.
                    Annotation annotation = page.Annotations[annIndex];

                    // Add the Hidden flag while preserving existing flags.
                    annotation.Flags = annotation.Flags | AnnotationFlags.Hidden;
                }
            }

            // Save the modified document to a temporary file.
            doc.Save(tempPath);
        }

        // Step 2: Enable Full‑Screen viewer preference using PdfContentEditor.
        // PdfContentEditor does not implement IDisposable, so no using block is needed.
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(tempPath);
        editor.ChangeViewerPreference(ViewerPreference.PageModeFullScreen);
        editor.Save(outputPath);
        editor.Close(); // Close the facade to release resources.

        // Clean up the temporary file.
        try { File.Delete(tempPath); } catch { /* ignore cleanup errors */ }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}