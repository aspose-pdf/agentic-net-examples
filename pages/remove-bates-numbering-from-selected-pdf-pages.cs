using System;
using System.IO;
using Aspose.Pdf; // Core PDF API
using Aspose.Pdf.Annotations; // Needed for StampAnnotation

class Program
{
    static void Main()
    {
        // Input / output files
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Text of the Bates number stamp to remove
        const string stampText = "Bates 001";

        // Pages (1‑based) from which the stamp should be removed
        int[] pagesToProcess = { 2, 3, 5 }; // example page numbers

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block (ensures deterministic disposal)
            using (Document doc = new Document(inputPath))
            {
                // Iterate using 1‑based indexing (Aspose.Pdf requirement)
                foreach (int pageNumber in pagesToProcess)
                {
                    // Guard against invalid page numbers
                    if (pageNumber < 1 || pageNumber > doc.Pages.Count)
                        continue;

                    Page page = doc.Pages[pageNumber];

                    // Remove any StampAnnotation whose title matches the Bates text.
                    // The Annotations collection is 1‑based, so we iterate backwards
                    // to safely delete items while enumerating.
                    for (int i = page.Annotations.Count; i >= 1; i--)
                    {
                        var annotation = page.Annotations[i];
                        if (annotation is StampAnnotation stampAnn)
                        {
                            // Some versions store the stamp text in the Title property.
                            // Fall back to Subject if Title is empty.
                            string candidate = stampAnn.Title ?? stampAnn.Subject ?? string.Empty;
                            if (candidate.Equals(stampText, StringComparison.OrdinalIgnoreCase))
                            {
                                page.Annotations.Delete(i);
                            }
                        }
                    }
                }

                // Save the modified PDF.
                doc.Save(outputPath);
            }

            Console.WriteLine($"Bates stamp removed. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
