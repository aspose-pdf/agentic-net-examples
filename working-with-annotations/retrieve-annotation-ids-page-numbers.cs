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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Dictionary mapping annotation identifier -> page number (1‑based)
            // Aspose.Pdf.Annotation does not expose an "Id" property. The closest
            // built‑in identifier is the "Name" property. If an annotation does not
            // have a Name we generate a temporary GUID so that every entry in the
            // dictionary has a unique key.
            Dictionary<string, int> annotationMap = new Dictionary<string, int>();

            // Iterate pages using 1‑based indexing (Aspose.Pdf uses 1‑based page numbers)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Enumerate all annotations on the current page
                foreach (Annotation annot in page.Annotations)
                {
                    // Use the Name property as the identifier; fall back to a GUID when Name is null or empty
                    string id = string.IsNullOrEmpty(annot.Name) ? Guid.NewGuid().ToString() : annot.Name;

                    // Store the mapping; later duplicates (same Name) will overwrite earlier ones
                    annotationMap[id] = i;
                }
            }

            // Example: output the collected mappings
            foreach (var kvp in annotationMap)
            {
                Console.WriteLine($"Annotation Id: {kvp.Key} -> Page: {kvp.Value}");
            }

            // The dictionary 'annotationMap' can now be used for quick lookup of page numbers by annotation Id.
        }
    }
}
