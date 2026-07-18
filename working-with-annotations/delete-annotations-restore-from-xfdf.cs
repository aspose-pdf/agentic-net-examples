using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";      // original PDF with annotations
        const string xfdfPath      = "backup.xfdf";    // XFDF file containing saved annotations
        const string outputPdfPath = "output.pdf";     // PDF after re‑importing annotations

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF backup not found: {xfdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdfPath))
            {
                // Delete all annotations on each page
                for (int i = 1; i <= doc.Pages.Count; i++)          // 1‑based page indexing
                {
                    // AnnotationCollection.Delete() removes every annotation on the page
                    doc.Pages[i].Annotations.Delete();
                }

                // Re‑import annotations from the XFDF file
                doc.ImportAnnotationsFromXfdf(xfdfPath);

                // Save the modified document
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"Annotations cleared and restored. Saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}