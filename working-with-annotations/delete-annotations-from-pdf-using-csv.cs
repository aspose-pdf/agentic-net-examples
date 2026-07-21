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
        const string csvPath = "annotations_to_delete.csv"; // CSV with a single column: Name

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

        // Read annotation identifiers (names) from CSV
        var namesToDelete = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var line in File.ReadLines(csvPath))
        {
            var trimmed = line.Trim();
            if (!string.IsNullOrEmpty(trimmed))
                namesToDelete.Add(trimmed);
        }

        // Load PDF, delete matching annotations, and save
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];
                // Collect annotations to delete to avoid modifying collection while iterating
                var toRemove = new List<Annotation>();

                foreach (Annotation ann in page.Annotations)
                {
                    // Annotation name can be set via its Name property
                    if (!string.IsNullOrEmpty(ann.Name) && namesToDelete.Contains(ann.Name))
                    {
                        toRemove.Add(ann);
                    }
                }

                // Delete collected annotations
                foreach (var ann in toRemove)
                {
                    page.Annotations.Delete(ann);
                }
            }

            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Annotations deleted and PDF saved to '{outputPdfPath}'.");
    }
}