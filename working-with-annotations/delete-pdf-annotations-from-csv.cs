using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string csvPath = "ids.csv";
        const string outputPath = "output.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV not found: {csvPath}");
            return;
        }

        // Load identifiers from CSV (first column, ignore empty lines)
        var idsToDelete = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var line in File.ReadLines(csvPath))
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            var parts = line.Split(',');
            var id = parts[0].Trim();
            if (!string.IsNullOrEmpty(id))
                idsToDelete.Add(id);
        }

        // Open PDF with deterministic disposal
        using (var doc = new Document(pdfPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                // Collect annotations to remove (cannot modify collection while iterating)
                var toRemove = new List<Annotation>();
                foreach (Annotation annot in page.Annotations)
                {
                    // Use the Name property as the identifier; if Name is null, skip
                    string key = annot.Name;
                    if (key != null && idsToDelete.Contains(key))
                        toRemove.Add(annot);
                }
                // Remove the selected annotations
                foreach (Annotation annot in toRemove)
                {
                    page.Annotations.Delete(annot);
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotations removed. Result saved to '{outputPath}'.");
    }
}
