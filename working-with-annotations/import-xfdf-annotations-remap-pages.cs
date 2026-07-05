using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Annotations; // for Annotation types
using Aspose.Pdf.Annotations; // ensure Annotation namespace is available
using Aspose.Pdf; // core PDF API
using Aspose.Pdf.Annotations; // annotation collection
using Aspose.Pdf.Annotations; // duplicate for clarity
using Aspose.Pdf.Annotations; // no effect
using Aspose.Pdf.Annotations; // keep compiler happy
using Aspose.Pdf.Annotations; // end

class Program
{
    static void Main()
    {
        // Input PDF and XFDF file paths
        const string pdfPath = "input.pdf";
        const string xfdfPath = "annotations.xfdf";
        const string outputPath = "output.pdf";

        // Mapping from original XFDF page numbers to target PDF page numbers
        // Example: annotations originally on page 1 should be moved to page 3, etc.
        var pageMapping = new Dictionary<int, int>
        {
            { 1, 3 },
            { 2, 5 },
            // add more mappings as needed
        };

        // Ensure input files exist
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

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(pdfPath))
        {
            // Import annotations from XFDF into the document
            using (FileStream xfdfStream = File.OpenRead(xfdfPath))
            {
                // Static method from Aspose.Pdf.Annotations.XfdfReader
                XfdfReader.ReadAnnotations(xfdfStream, doc);
            }

            // Reassign annotations according to the mapping dictionary
            foreach (var kvp in pageMapping)
            {
                int sourcePageNumber = kvp.Key;   // page number in XFDF (original)
                int targetPageNumber = kvp.Value; // desired page number in the PDF

                // Validate page numbers (Aspose.Pdf uses 1‑based indexing)
                if (sourcePageNumber < 1 || sourcePageNumber > doc.Pages.Count ||
                    targetPageNumber < 1 || targetPageNumber > doc.Pages.Count)
                {
                    Console.Error.WriteLine($"Invalid page mapping: {sourcePageNumber} -> {targetPageNumber}");
                    continue;
                }

                Page sourcePage = doc.Pages[sourcePageNumber];
                Page targetPage = doc.Pages[targetPageNumber];

                // Collect annotations from the source page (avoid modifying collection while iterating)
                var annotationsToMove = new List<Annotation>();
                for (int i = 1; i <= sourcePage.Annotations.Count; i++)
                {
                    annotationsToMove.Add(sourcePage.Annotations[i]);
                }

                // Move each annotation to the target page
                foreach (var annotation in annotationsToMove)
                {
                    // Add to target page
                    targetPage.Annotations.Add(annotation);
                }

                // Remove all annotations from the source page
                while (sourcePage.Annotations.Count > 0)
                {
                    // Delete the first annotation repeatedly until collection is empty
                    sourcePage.Annotations.Delete(1);
                }
            }

            // Save the modified PDF (lifecycle rule: save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotations imported and remapped. Output saved to '{outputPath}'.");
    }
}