using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Dictionary to map annotation IDs (Name or FullName) to their page numbers (1‑based)
            var annotationMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // Fully qualify Page to avoid ambiguity with System.Drawing.Page
                Aspose.Pdf.Page page = doc.Pages[i];

                // Iterate over each annotation on the current page
                foreach (Annotation ann in page.Annotations)
                {
                    // Prefer the user‑defined Name; fall back to FullName if Name is empty
                    string id = !string.IsNullOrEmpty(ann.Name) ? ann.Name : ann.FullName;

                    if (!string.IsNullOrEmpty(id))
                    {
                        // Annotation.PageIndex also returns a 1‑based page number
                        annotationMap[id] = ann.PageIndex;
                    }
                }
            }

            // Example output: display the mapping
            foreach (var kvp in annotationMap)
            {
                Console.WriteLine($"Annotation ID: {kvp.Key}, Page: {kvp.Value}");
            }

            // The 'annotationMap' dictionary is now ready for quick lookup elsewhere
        }
    }
}