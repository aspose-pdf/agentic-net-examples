using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // -----------------------------------------------------------------
        // Step 1: Hide all annotations in the document.
        // -----------------------------------------------------------------
        // Load the PDF with the core API, iterate all pages and annotations,
        // and set the Hidden flag (AnnotationFlags.Hidden).
        // Save the modified document to a temporary file.
        // -----------------------------------------------------------------
        string tempPath = Path.GetTempFileName();

        using (Document doc = new Document(inputPdf))
        {
            foreach (Page page in doc.Pages)
            {
                // Annotations collection uses 1‑based indexing.
                for (int i = 1; i <= page.Annotations.Count; i++)
                {
                    Annotation ann = page.Annotations[i];
                    // Preserve existing flags and add the Hidden flag.
                    ann.Flags |= AnnotationFlags.Hidden;
                }
            }

            // Save the document with hidden annotations.
            doc.Save(tempPath);
        }

        // -----------------------------------------------------------------
        // Step 2: Enable FullScreen viewer preference using PdfContentEditor.
        // -----------------------------------------------------------------
        // PdfContentEditor works with file paths, not with Document objects.
        // Bind the temporary file, set the FullScreen flag, and save the final output.
        // -----------------------------------------------------------------
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(tempPath);
        editor.ChangeViewerPreference(ViewerPreference.PageModeFullScreen);
        editor.Save(outputPdf);
        editor.Close(); // explicit close; PdfContentEditor does not implement IDisposable.

        // Clean up the temporary file.
        try { File.Delete(tempPath); } catch { /* ignore cleanup errors */ }

        Console.WriteLine($"Processed PDF saved to '{outputPdf}'.");
    }
}