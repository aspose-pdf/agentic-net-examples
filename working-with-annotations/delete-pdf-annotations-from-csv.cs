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
        const string csvPath = "ids.csv";
        const string outputPdfPath = "output.pdf";

        // Validate input files
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

        // Load identifiers (one per line) into a hash set for fast lookup
        var idsToDelete = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var line in File.ReadLines(csvPath))
        {
            var id = line.Trim();
            if (!string.IsNullOrEmpty(id))
                idsToDelete.Add(id);
        }

        // Open the PDF document (lifecycle: using block ensures disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];
                var annotationsToRemove = new List<Annotation>();

                // Collect annotations whose Name matches any identifier from the CSV
                foreach (Annotation annotation in page.Annotations)
                {
                    // Annotation.Name may be null; guard against it
                    if (!string.IsNullOrEmpty(annotation.Name) && idsToDelete.Contains(annotation.Name))
                    {
                        annotationsToRemove.Add(annotation);
                    }
                }

                // Delete the collected annotations using AnnotationCollection.Delete(Annotation)
                foreach (var annotation in annotationsToRemove)
                {
                    page.Annotations.Delete(annotation);
                }
            }

            // Save the modified PDF (lifecycle: save inside using block)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Annotations removed. Output saved to '{outputPdfPath}'.");
    }
}