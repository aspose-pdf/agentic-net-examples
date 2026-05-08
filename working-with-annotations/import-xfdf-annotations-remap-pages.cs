using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xfdfPath = "annotations.xfdf";
        const string outputPath = "output.pdf";

        // Mapping from XFDF page numbers (source) to target PDF page numbers
        var pageMapping = new Dictionary<int, int>
        {
            { 1, 3 }, // annotations from XFDF page 1 go to PDF page 3
            { 2, 5 }  // annotations from XFDF page 2 go to PDF page 5
            // add additional mappings as needed
        };

        // ------------------------------------------------------------
        // Ensure the source PDF exists – create a placeholder if it does not.
        // ------------------------------------------------------------
        if (!File.Exists(pdfPath))
        {
            // Determine the highest page number we might need (target pages).
            int maxTargetPage = 0;
            foreach (var kvp in pageMapping)
                if (kvp.Value > maxTargetPage) maxTargetPage = kvp.Value;
            // Create a new PDF with enough blank pages.
            using (var placeholder = new Document())
            {
                // Aspose.Pdf starts with one page by default; add the rest.
                for (int i = 1; i < maxTargetPage; i++)
                    placeholder.Pages.Add();
                placeholder.Save(pdfPath);
            }
        }

        // ------------------------------------------------------------
        // Ensure the XFDF file exists – create an empty XFDF if it does not.
        // ------------------------------------------------------------
        if (!File.Exists(xfdfPath))
        {
            // Minimal valid XFDF structure (no annotations).
            string emptyXfdf = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<xfdf xmlns=\"http://ns.adobe.com/xfdf/\"></xfdf>";
            File.WriteAllText(xfdfPath, emptyXfdf);
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(pdfPath))
        {
            // Import all annotations from the XFDF file into the document
            doc.ImportAnnotationsFromXfdf(xfdfPath);

            // Re‑assign annotations according to the mapping dictionary
            foreach (var kvp in pageMapping)
            {
                int srcPageNum = kvp.Key;
                int tgtPageNum = kvp.Value;

                // Ensure both source and target pages exist (1‑based indexing)
                if (srcPageNum < 1 || srcPageNum > doc.Pages.Count ||
                    tgtPageNum < 1 || tgtPageNum > doc.Pages.Count)
                {
                    continue; // skip invalid entries
                }

                Page srcPage = doc.Pages[srcPageNum];
                Page tgtPage = doc.Pages[tgtPageNum];

                // Move all annotations from the source page to the target page
                while (srcPage.Annotations.Count > 0)
                {
                    // Annotations collection is 1‑based
                    Annotation ann = srcPage.Annotations[1];
                    srcPage.Annotations.Delete(1);   // remove from source
                    tgtPage.Annotations.Add(ann);    // add to target
                }
            }

            // Save the modified PDF (PDF format, no SaveOptions needed)
            doc.Save(outputPath);
        }
    }
}
