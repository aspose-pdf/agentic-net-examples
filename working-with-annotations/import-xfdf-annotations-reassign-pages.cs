using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input PDF and XFDF files
        const string pdfPath   = "input.pdf";
        const string xfdfPath  = "annotations.xfdf";
        const string outputPdf = "output.pdf";

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

        // Mapping of source page numbers (as stored in XFDF) to target page numbers in the PDF
        // Example: annotations originally on page 1 should be moved to page 2, etc.
        var pageMapping = new Dictionary<int, int>
        {
            { 1, 2 },
            { 2, 3 },
            // add more mappings as needed
        };

        // Load the PDF, import XFDF annotations, re‑assign them according to the mapping, and save.
        using (Document doc = new Document(pdfPath))
        {
            // Import all annotations from the XFDF file into the document.
            doc.ImportAnnotationsFromXfdf(xfdfPath);

            // Collect annotations that need to be moved.
            var moves = new List<(Annotation annotation, int targetPage)>();

            foreach (var kvp in pageMapping)
            {
                int srcPageNum = kvp.Key;
                int tgtPageNum = kvp.Value;

                // Ensure both source and target pages exist.
                if (srcPageNum < 1 || srcPageNum > doc.Pages.Count ||
                    tgtPageNum < 1 || tgtPageNum > doc.Pages.Count)
                {
                    continue; // skip invalid mapping entries
                }

                Page srcPage = doc.Pages[srcPageNum];
                // Copy annotations to the move list.
                foreach (Annotation ann in srcPage.Annotations)
                {
                    moves.Add((ann, tgtPageNum));
                }

                // Remove all annotations from the source page (iterate backwards to avoid index shift).
                for (int i = srcPage.Annotations.Count; i >= 1; i--)
                {
                    srcPage.Annotations.Delete(i);
                }
            }

            // Add the collected annotations to their new target pages.
            foreach (var (annotation, targetPage) in moves)
            {
                Page tgtPage = doc.Pages[targetPage];
                tgtPage.Annotations.Add(annotation);
            }

            // Save the modified PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Annotations imported and reassigned. Saved to '{outputPdf}'.");
    }
}