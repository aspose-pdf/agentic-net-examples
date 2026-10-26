using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "original.pdf";   // input PDF
        const string xfdfPath = "backup.xfdf";    // XFDF backup containing annotations
        const string outputPath = "restored.pdf"; // PDF after re‑importing annotations

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

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Delete all annotations from every page (1‑based indexing)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];
                    page.Annotations.Clear(); // removes all annotations on the page
                }

                // Re‑import annotations from the XFDF backup
                doc.ImportAnnotationsFromXfdf(xfdfPath); // correct API call

                // Save the updated PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Annotations restored and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
