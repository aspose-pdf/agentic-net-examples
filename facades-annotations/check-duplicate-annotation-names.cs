using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class AnnotationChecker
{
    // Checks for duplicate annotation names in the specified PDF file.
    // Logs each duplicate name together with the pages on which it appears.
    public static void CheckDuplicateAnnotationNames(string pdfPath)
    {
        if (string.IsNullOrEmpty(pdfPath) || !System.IO.File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // PdfAnnotationEditor implements IDisposable via SaveableFacade, so use a using block.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Load the PDF document.
            editor.BindPdf(pdfPath);

            // Dictionary to map annotation name -> list of page numbers where it occurs.
            var nameMap = new Dictionary<string, List<int>>(StringComparer.OrdinalIgnoreCase);

            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing).
            for (int pageIndex = 1; pageIndex <= editor.Document.Pages.Count; pageIndex++)
            {
                Page page = editor.Document.Pages[pageIndex];
                AnnotationCollection annotations = page.Annotations;

                // Iterate through annotations on the current page.
                for (int i = 1; i <= annotations.Count; i++)
                {
                    Annotation annot = annotations[i];
                    string name = annot.Name;

                    // Some annotations may have no name; skip them.
                    if (string.IsNullOrEmpty(name))
                        continue;

                    if (!nameMap.TryGetValue(name, out var pages))
                    {
                        pages = new List<int>();
                        nameMap[name] = pages;
                    }
                    pages.Add(pageIndex);
                }
            }

            // Identify and log duplicates.
            bool anyDuplicates = false;
            foreach (var kvp in nameMap)
            {
                if (kvp.Value.Count > 1)
                {
                    anyDuplicates = true;
                    Console.WriteLine($"Duplicate annotation name \"{kvp.Key}\" found on pages: {string.Join(", ", kvp.Value)}");
                }
            }

            if (!anyDuplicates)
            {
                Console.WriteLine("No duplicate annotation names were found.");
            }
        }
    }

    // Example usage.
    static void Main()
    {
        const string inputPdf = "sample.pdf";
        CheckDuplicateAnnotationNames(inputPdf);
    }
}