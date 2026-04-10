using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
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

    // Checks for duplicate annotation names in a PDF and logs any conflicts.
    static void CheckDuplicateAnnotationNames(string pdfPath)
    {
        // Use PdfAnnotationEditor (Facade) to work with annotations.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Load the PDF document.
            editor.BindPdf(pdfPath);

            // Access the underlying Document.
            Document doc = editor.Document;

            // Dictionary to collect annotations by their Name.
            var nameMap = new Dictionary<string, List<Annotation>>(StringComparer.OrdinalIgnoreCase);

            // Iterate through all pages (1‑based indexing).
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Iterate through all annotations on the page.
                foreach (Annotation annot in page.Annotations)
                {
                    // Annotation.Name may be null or empty; skip such entries.
                    string name = annot.Name;
                    if (string.IsNullOrEmpty(name))
                        continue;

                    if (!nameMap.TryGetValue(name, out var list))
                    {
                        list = new List<Annotation>();
                        nameMap[name] = list;
                    }
                    list.Add(annot);
                }
            }

            // Report duplicates.
            bool anyDuplicates = false;
            foreach (var kvp in nameMap)
            {
                if (kvp.Value.Count > 1)
                {
                    anyDuplicates = true;
                    Console.WriteLine($"Duplicate annotation name '{kvp.Key}' found {kvp.Value.Count} times on pages:");
                    foreach (Annotation a in kvp.Value)
                    {
                        // Annotation.PageIndex gives the page number (1‑based).
                        Console.WriteLine($"  - Page {a.PageIndex}, Type {a.AnnotationType}");
                    }
                }
            }

            if (!anyDuplicates)
            {
                Console.WriteLine("No duplicate annotation names were found.");
            }
        }
    }
}