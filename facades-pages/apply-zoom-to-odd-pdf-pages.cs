using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_zoomed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Determine the total number of pages (1‑based indexing)
        int pageCount;
        using (Document doc = new Document(inputPath))
        {
            pageCount = doc.Pages.Count;
        }

        // Build an array containing all odd‑numbered page indexes
        int oddPagesCount = (pageCount + 1) / 2;               // number of odd pages
        int[] oddPages = new int[oddPagesCount];
        for (int i = 0, page = 1; i < oddPagesCount; i++, page += 2)
        {
            oddPages[i] = page;                               // pages are 1‑based
        }

        // Use PdfPageEditor to apply the zoom factor to the selected pages
        PdfPageEditor editor = new PdfPageEditor();
        editor.BindPdf(inputPath);          // load the source PDF
        editor.ProcessPages = oddPages;     // limit editing to odd pages only
        editor.Zoom = 1.2f;                 // 1.0 = 100%, 1.2 = 120%
        editor.ApplyChanges();              // commit the changes
        editor.Save(outputPath);            // write the modified PDF
        editor.Close();                     // release resources

        Console.WriteLine($"Zoom applied to odd pages. Output saved to '{outputPath}'.");
    }
}