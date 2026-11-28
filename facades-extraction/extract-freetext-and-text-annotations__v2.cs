using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;          // PdfAnnotationEditor
using Aspose.Pdf.Annotations;      // Annotation

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfAnnotationEditor implements IDisposable, so wrap it in a using block
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Load the PDF document into the editor
            editor.BindPdf(inputPath);

            // Annotation type names as strings – this overload is available in the
            // Aspose.Pdf version that provides ExtractAnnotations(int, int, string[])
            string[] types = new[] { "FreeText", "Text" };

            // Pages are 1‑based in Aspose.Pdf
            for (int page = 1; page <= 5; page++)
            {
                // The buffer size can be larger than the expected number of annotations.
                const int bufferSize = 100;

                // Extract annotations of the specified types from the current page.
                // The method returns a list of matching Annotation objects.
                var extracted = editor.ExtractAnnotations(page, bufferSize, types);

                if (extracted == null || extracted.Count == 0)
                    continue; // no matching annotations on this page

                foreach (Annotation ann in extracted)
                {
                    string name = string.IsNullOrEmpty(ann.Name) ? "<no name>" : ann.Name;
                    Console.WriteLine($"Page {page}: Type={ann.AnnotationType}, Name={name}");
                    // Additional properties such as ann.Contents, ann.Rect, etc., are available.
                }
            }
        }
    }
}
