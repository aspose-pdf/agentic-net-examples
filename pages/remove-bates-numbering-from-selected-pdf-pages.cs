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
        const string outputPath = "output.pdf";

        // The Bates number stamp text to remove
        const string targetBatesNumber = "BATES-00123";

        // Pages (1‑based) from which the stamp should be removed
        List<int> pagesToProcess = new List<int> { 2, 3, 5 };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Document must be wrapped in a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                foreach (int pageIndex in pagesToProcess)
                {
                    // Ensure the page index is within the document range
                    if (pageIndex < 1 || pageIndex > doc.Pages.Count)
                        continue; // skip invalid page numbers

                    Page page = doc.Pages[pageIndex];

                    // Iterate backwards when removing items from the Annotations collection
                    for (int i = page.Annotations.Count - 1; i >= 0; i--)
                    {
                        Annotation ann = page.Annotations[i];
                        // StampAnnotation stores its visible text in the Contents property
                        if (ann is StampAnnotation stamp &&
                            string.Equals(stamp.Contents, targetBatesNumber, StringComparison.Ordinal))
                        {
                            // Remove the matching stamp annotation from the page
                            page.Annotations.Delete(i);
                        }
                    }
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Bates number '{targetBatesNumber}' removed. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
