using System;
using System.Collections.Generic;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    // Checks for duplicate annotation names in a PDF and logs any conflicts.
    static void CheckDuplicateAnnotationNames(string pdfPath)
    {
        // Bind the PDF using PdfAnnotationEditor (load rule).
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(pdfPath);

            // Access the underlying Document.
            Document doc = editor.Document;

            // Dictionary to track annotation names and the annotations that share them.
            var nameMap = new Dictionary<string, List<Annotation>>(StringComparer.OrdinalIgnoreCase);

            // Iterate through all pages (1‑based indexing).
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                // Iterate through all annotations on the page.
                foreach (Annotation annot in page.Annotations)
                {
                    // Annotation.Name may be null or empty; skip such entries.
                    string name = annot.Name;
                    if (string.IsNullOrEmpty(name))
                        continue;

                    // Add the annotation to the list for this name.
                    if (!nameMap.TryGetValue(name, out var list))
                    {
                        list = new List<Annotation>();
                        nameMap[name] = list;
                    }
                    list.Add(annot);
                }
            }

            // Log any names that appear more than once.
            foreach (var kvp in nameMap)
            {
                if (kvp.Value.Count > 1)
                {
                    // Gather the page numbers where the duplicate annotations reside.
                    var pages = kvp.Value
                                 .Select(a => a.PageIndex) // PageIndex is 1‑based.
                                 .Distinct()
                                 .OrderBy(p => p);
                    Console.WriteLine($"Duplicate annotation name '{kvp.Key}' found {kvp.Value.Count} times on pages: {string.Join(", ", pages)}");
                }
            }

            // No modifications are made; optionally save the unchanged PDF.
            // editor.Save(pdfPath); // Uncomment if you need to rewrite the file.
        }
    }

    static void Main()
    {
        const string inputPdf = "sample.pdf";

        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        CheckDuplicateAnnotationNames(inputPdf);
    }
}