using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_resized.pdf";

        // ---------------------------------------------------------------
        // Ensure the input PDF exists. If it does not, create a minimal
        // placeholder PDF so the example can run in a sandbox that starts
        // with an empty file system.
        // ---------------------------------------------------------------
        if (!File.Exists(inputPath))
        {
            using var placeholder = new Document();
            placeholder.Pages.Add();
            placeholder.Save(inputPath);
        }

        // Pages to delete (1‑based indexing). Adjust as needed.
        int[] pagesToDelete = new int[] { 2, 3 };

        // ---------------------------------------------------------------
        // 1. Load the PDF using the Document class (the recommended API).
        // ---------------------------------------------------------------
        Document pdfDoc = new Document(inputPath);

        // ---------------------------------------------------------------
        // 2. Delete the specified pages.
        //    Deletions must be performed in descending order so that
        //    subsequent page numbers remain valid.
        // ---------------------------------------------------------------
        Array.Sort(pagesToDelete); // ensure order
        for (int i = pagesToDelete.Length - 1; i >= 0; i--)
        {
            int pageNumber = pagesToDelete[i];
            if (pageNumber >= 1 && pageNumber <= pdfDoc.Pages.Count)
            {
                pdfDoc.Pages.Delete(pageNumber);
            }
        }

        // ---------------------------------------------------------------
        // 3. Resize each remaining page (e.g., to A4 size).
        // ---------------------------------------------------------------
        foreach (Page page in pdfDoc.Pages)
        {
            page.SetPageSize(PageSize.A4.Width, PageSize.A4.Height);
        }

        // ---------------------------------------------------------------
        // 4. Save the modified PDF.
        // ---------------------------------------------------------------
        pdfDoc.Save(outputPath);

        Console.WriteLine($"Pages deleted and document resized. Saved to '{outputPath}'.");
    }
}
