using System;
using System.Collections.Generic;
using System.IO;
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

        // Load identifiers (annotation names) from CSV into a HashSet for fast lookup.
        // Assume the CSV contains one identifier per line (or first column if comma‑separated).
        var idsToDelete = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var line in File.ReadAllLines(csvPath))
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            var parts = line.Split(',');
            var id = parts[0].Trim();
            if (!string.IsNullOrEmpty(id))
                idsToDelete.Add(id);
        }

        // Open the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate through all pages (1‑based indexing).
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];
                // Work on a copy of the current annotations collection to avoid modification during enumeration.
                var annotations = new List<Annotation>();
                foreach (Annotation ann in page.Annotations)
                {
                    annotations.Add(ann);
                }

                // Delete annotations whose Name matches any identifier from the CSV.
                foreach (Annotation ann in annotations)
                {
                    // Annotation.Name may be null; guard against it.
                    if (!string.IsNullOrEmpty(ann.Name) && idsToDelete.Contains(ann.Name))
                    {
                        // Delete by passing the annotation instance.
                        page.Annotations.Delete(ann);
                    }
                }
            }

            // Save the modified PDF.
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Annotations removed and saved to '{outputPdfPath}'.");
    }
}