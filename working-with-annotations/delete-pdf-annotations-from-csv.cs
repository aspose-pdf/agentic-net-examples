using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string csvPath       = "annotations_to_remove.csv";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV not found: {csvPath}");
            return;
        }

        // Load identifiers (annotation names) from CSV – assume one identifier per line or first column
        var idsToRemove = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var line in File.ReadLines(csvPath))
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            // Split by comma, take first column as identifier
            var parts = line.Split(',');
            var id = parts[0].Trim();
            if (!string.IsNullOrEmpty(id))
                idsToRemove.Add(id);
        }

        // Open PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                // Collect annotations that match any identifier
                var toDelete = new List<Annotation>();
                foreach (Annotation annot in page.Annotations)
                {
                    // Annotation.Name holds the identifier set when the annotation was created
                    if (!string.IsNullOrEmpty(annot.Name) && idsToRemove.Contains(annot.Name))
                        toDelete.Add(annot);
                }

                // Remove the matched annotations
                foreach (Annotation annot in toDelete)
                {
                    page.Annotations.Delete(annot);
                }
            }

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Annotations removed. Output saved to '{outputPdfPath}'.");
    }
}