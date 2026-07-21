using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

public static class AnnotationReporter
{
    // Returns a dictionary where the key is the page number (1‑based) and the value is the total
    // number of annotations on that page.
    public static Dictionary<int, int> GetAnnotationsCountPerPage(string pdfPath)
    {
        if (string.IsNullOrEmpty(pdfPath))
            throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

        var counts = new Dictionary<int, int>();

        // PdfAnnotationEditor is a Facades class that works with annotations.
        // It implements IDisposable, so we can use a using block for deterministic cleanup.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the PDF file to the editor.
            editor.BindPdf(pdfPath);

            // Access the underlying Document to obtain the page count.
            Document doc = editor.Document;
            int pageCount = doc.Pages.Count; // Pages are 1‑based.

            // Loop through each page and extract all annotations.
            for (int page = 1; page <= pageCount; page++)
            {
                // ExtractAnnotations returns an IList<Annotation> for the specified page range.
                // Passing null for the AnnotationType[] parameter retrieves all annotation types.
                // The cast removes the ambiguity between the overloads that accept string[]
                // and AnnotationType[].
                IList<Annotation> pageAnnotations = editor.ExtractAnnotations(page, page, (AnnotationType[])null);
                counts[page] = pageAnnotations?.Count ?? 0;
            }

            // Close the editor (optional because of using, but explicit for clarity).
            editor.Close();
        }

        return counts;
    }

    // Dummy entry point to satisfy the compiler when the project is built as a console app.
    // In a real library this method can be removed or the project type changed to a class library.
    public static void Main(string[] args)
    {
        // No operation – the API is intended to be called from other code.
    }

    // Example usage (commented out for library builds):
    // static void Main()
    // {
    //     var result = GetAnnotationsCountPerPage("sample.pdf");
    //     foreach (var kvp in result)
    //         Console.WriteLine($"Page {kvp.Key}: {kvp.Value} annotation(s)");
    // }
}