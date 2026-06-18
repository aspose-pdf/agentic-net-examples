using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input PDF, XFDF and output PDF paths
        const string pdfPath   = "input.pdf";
        const string xfdfPath  = "annotations.xfdf";
        const string outputPdf = "output.pdf";

        // Mapping: source page number -> target page number
        // Example: annotations on page 1 should be moved to page 3, page 2 stays unchanged, etc.
        var pageMapping = new Dictionary<int, int>
        {
            { 1, 3 },
            { 2, 2 },
            { 3, 1 }
        };

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF not found: {xfdfPath}");
            return;
        }

        // Load the PDF document (using ensures proper disposal)
        using (Document doc = new Document(pdfPath))
        {
            // Import all annotations from the XFDF file into the document
            // (alternatively: XfdfReader.ReadAnnotations(File.OpenRead(xfdfPath), doc);)
            doc.ImportAnnotationsFromXfdf(xfdfPath);

            // Collect annotations that need to be moved according to the mapping
            var moves = new List<(Annotation annotation, int sourcePage, int targetPage)>();

            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                // Determine if this page has a mapping entry
                if (!pageMapping.TryGetValue(pageNum, out int targetPage) || targetPage == pageNum)
                    continue; // No move required for this page

                Page srcPage = doc.Pages[pageNum];

                // Iterate through annotations on the source page (1‑based indexing)
                for (int annIdx = 1; annIdx <= srcPage.Annotations.Count; annIdx++)
                {
                    Annotation ann = srcPage.Annotations[annIdx];
                    moves.Add((ann, pageNum, targetPage));
                }
            }

            // Perform the moves: remove from source page and add to target page
            foreach (var move in moves)
            {
                Page srcPage = doc.Pages[move.sourcePage];
                Page tgtPage = doc.Pages[move.targetPage];

                // Find the index of the annotation in the source page collection
                int indexToDelete = -1;
                for (int i = 1; i <= srcPage.Annotations.Count; i++)
                {
                    if (srcPage.Annotations[i] == move.annotation)
                    {
                        indexToDelete = i;
                        break;
                    }
                }

                if (indexToDelete != -1)
                {
                    // Delete from source page (AnnotationCollection uses 1‑based Delete)
                    srcPage.Annotations.Delete(indexToDelete);
                }

                // Add to target page; consider rotation if needed (default false)
                tgtPage.Annotations.Add(move.annotation);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Annotations imported and reassigned. Output saved to '{outputPdf}'.");
    }
}