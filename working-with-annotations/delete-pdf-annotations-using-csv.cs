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
        const string csvPath = "annotations_to_delete.csv";
        const string outputPdfPath = "output.pdf";

        // Load identifiers (annotation names) from CSV
        var idsToDelete = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        if (File.Exists(csvPath))
        {
            foreach (var line in File.ReadAllLines(csvPath))
            {
                // Assume CSV contains a single column with the annotation name
                var trimmed = line.Trim();
                if (!string.IsNullOrEmpty(trimmed))
                    idsToDelete.Add(trimmed);
            }
        }
        else
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate over each page (1‑based indexing rule)
            foreach (Page page in doc.Pages)
            {
                // For each identifier, try to find and delete the annotation on this page
                foreach (string name in idsToDelete)
                {
                    Annotation ann = page.Annotations.FindByName(name);
                    if (ann != null)
                    {
                        // Delete the specific annotation (method overload Delete(Annotation))
                        page.Annotations.Delete(ann);
                    }
                }
            }

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Annotations removed. Output saved to '{outputPdfPath}'.");
    }
}