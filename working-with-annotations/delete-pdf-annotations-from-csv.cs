using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string csvPath = "annotations_to_delete.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        // Read annotation identifiers (assumed to be the annotation Name) from CSV.
        // CSV format: one identifier per line, optionally with a header.
        var idsToDelete = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var line in File.ReadAllLines(csvPath))
        {
            var trimmed = line.Trim();
            if (string.IsNullOrEmpty(trimmed) || trimmed.StartsWith("#")) // skip empty lines or comments
                continue;

            // If CSV has multiple columns, take the first column.
            var parts = trimmed.Split(',');
            if (parts.Length > 0)
                idsToDelete.Add(parts[0].Trim());
        }

        // Load the PDF document.
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate through all pages (1‑based indexing).
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];
                // Work on a copy of the collection to avoid modification during enumeration.
                var annotations = new List<Aspose.Pdf.Annotations.Annotation>(page.Annotations);
                foreach (var annotation in annotations)
                {
                    // Annotation.Name may be null; check before comparing.
                    if (!string.IsNullOrEmpty(annotation.Name) && idsToDelete.Contains(annotation.Name))
                    {
                        // Delete the annotation from the collection.
                        page.Annotations.Delete(annotation);
                    }
                }
            }

            // Save the modified PDF.
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Annotations deleted and PDF saved to '{outputPdfPath}'.");
    }
}