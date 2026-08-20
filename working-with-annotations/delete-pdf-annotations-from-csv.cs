using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string csvPath       = "annotations_to_delete.csv";

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

        // Read CSV lines. Expected format per line:
        //   pageNumber,annotationName
        // If pageNumber is omitted, the annotation will be searched on all pages.
        var deleteRequests = new List<(int? page, string name)>();
        foreach (var line in File.ReadAllLines(csvPath))
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            var parts = line.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 2 && int.TryParse(parts[0].Trim(), out int pageNum))
            {
                deleteRequests.Add((pageNum, parts[1].Trim()));
            }
            else if (parts.Length == 1)
            {
                deleteRequests.Add((null, parts[0].Trim()));
            }
            // Lines with unexpected format are ignored.
        }

        // Process PDF
        using (Document doc = new Document(inputPdfPath))
        {
            foreach (var (pageNumber, annotationName) in deleteRequests)
            {
                // Determine pages to search
                IEnumerable<Page> pagesToSearch;
                if (pageNumber.HasValue)
                {
                    // Ensure page number is within range (1‑based indexing)
                    if (pageNumber.Value < 1 || pageNumber.Value > doc.Pages.Count)
                        continue; // skip invalid page numbers

                    pagesToSearch = new[] { doc.Pages[pageNumber.Value] };
                }
                else
                {
                    pagesToSearch = doc.Pages; // all pages
                }

                foreach (Page page in pagesToSearch)
                {
                    // Find annotation by its name (if it has one)
                    Annotation ann = page.Annotations.FindByName(annotationName);
                    if (ann != null)
                    {
                        // Delete the found annotation
                        page.Annotations.Delete(ann);
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Annotations removed. Output saved to '{outputPdfPath}'.");
    }
}