using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";        // original PDF
        const string xfdfBackup = "annotations.xfdf"; // XFDF file containing saved annotations
        const string outputPath = "output.pdf";       // PDF after restoration

        // Verify input files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xfdfBackup))
        {
            Console.Error.WriteLine($"XFDF not found: {xfdfBackup}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Delete all annotations from every page (pages are 1‑based)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                page.Annotations.Clear(); // remove all annotations on this page
            }

            // Re‑import annotations from the XFDF backup using the correct API
            doc.ImportAnnotationsFromXfdf(xfdfBackup);

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotations restored and saved to '{outputPath}'.");
    }
}
