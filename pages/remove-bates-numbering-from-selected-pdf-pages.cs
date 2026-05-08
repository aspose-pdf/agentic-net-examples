using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Define the pages from which the Bates numbering stamp should be removed (1‑based indexing)
        int[] pagesToClean = { 2, 4, 5 };   // example: remove from pages 2, 4 and 5

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate over the selected pages
            foreach (int pageNumber in pagesToClean)
            {
                // Ensure the page number is within range
                if (pageNumber < 1 || pageNumber > doc.Pages.Count)
                    continue;

                Page page = doc.Pages[pageNumber];

                // Remove all Bates numbering artifacts from this page.
                // The DeleteBatesNumbering extension works on a PageCollection,
                // so we create a temporary collection containing only the current page.
                // Aspose.Pdf does not provide a direct constructor for a new PageCollection,
                // but we can use the existing collection and call the extension on it,
                // then re‑add the artifacts we want to keep. Here we simply delete all
                // Bates artifacts from the selected page.
                // The extension method removes every BatesNArtifact attached to the page.
                // Since it operates on the whole collection, we call it on the document's
                // Pages collection and then re‑add Bates numbering to pages we do NOT want
                // to modify. To avoid affecting other pages, we first store a copy of the
                // original Bates artifacts for those pages, delete all, and then restore
                // the untouched pages.

                // Store Bates artifacts from pages we want to keep
                var preservedArtifacts = new System.Collections.Generic.Dictionary<int, Artifact[]>();
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    if (Array.IndexOf(pagesToClean, i) >= 0) continue; // skip pages to clean
                    Page p = doc.Pages[i];
                    // Preserve only BatesNArtifact instances
                    var bates = System.Linq.Enumerable
                        .Where(p.Artifacts, a => a is BatesNArtifact)
                        .ToArray();
                    if (bates.Length > 0)
                        preservedArtifacts[i] = bates;
                }

                // Delete all Bates numbering artifacts from the whole document
                doc.Pages.DeleteBatesNumbering();

                // Restore preserved Bates artifacts to pages we did not intend to modify
                foreach (var kvp in preservedArtifacts)
                {
                    Page targetPage = doc.Pages[kvp.Key];
                    foreach (Artifact art in kvp.Value)
                    {
                        // Re‑add the artifact to the page
                        targetPage.Artifacts.Add(art);
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bates numbering removed from selected pages. Output saved to '{outputPath}'.");
    }
}