using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath      = "input.pdf";      // source PDF
        const string xfdfPath     = "annotations.xfdf"; // XFDF file with annotations
        const string outputPath   = "output.pdf";     // PDF after processing
        const string userRole     = "viewer";        // example role, e.g., "admin" or "viewer"

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

        // Load the PDF document
        using (Document doc = new Document(pdfPath))
        {
            // Import annotations from the XFDF file
            doc.ImportAnnotationsFromXfdf(xfdfPath);

            // Iterate over all pages (1‑based indexing)
            for (int pageIdx = 1; pageIdx <= doc.Pages.Count; pageIdx++)
            {
                Page page = doc.Pages[pageIdx];

                // Annotations collection also uses 1‑based indexing
                for (int annIdx = 1; annIdx <= page.Annotations.Count; annIdx++)
                {
                    Annotation ann = page.Annotations[annIdx];

                    // Example visibility rule:
                    // - If the user is an admin, keep annotations visible.
                    // - Otherwise, hide the annotation by setting the Hidden flag.
                    if (!string.Equals(userRole, "admin", StringComparison.OrdinalIgnoreCase))
                    {
                        // Preserve existing flags and add Hidden
                        ann.Flags |= AnnotationFlags.Hidden;
                    }
                    else
                    {
                        // Ensure the annotation is visible for admins
                        ann.Flags &= ~AnnotationFlags.Hidden;
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}