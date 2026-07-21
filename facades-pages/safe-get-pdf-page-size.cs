using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "sample.pdf";

        // Create a sample PDF with a known number of pages so the demo can run in an empty sandbox.
        CreateSamplePdf(pdfPath, pageCount: 3);

        // Example page numbers to request (1‑based indexing). Page 10 does not exist and will be caught.
        int[] pagesToCheck = { 1, 3, 10 };

        // Bind the PDF to a PdfPageEditor instance.
        PdfPageEditor editor = new PdfPageEditor();
        editor.BindPdf(pdfPath);

        // Iterate over the requested pages and safely obtain their sizes.
        foreach (int pageNum in pagesToCheck)
        {
            try
            {
                // GetPageSize throws if the page number does not exist.
                PageSize size = editor.GetPageSize(pageNum);
                Console.WriteLine($"Page {pageNum}: Width = {size.Width}, Height = {size.Height}");
            }
            catch (Exception ex)
            {
                // Handle non‑existent page numbers (or other errors) gracefully.
                Console.WriteLine($"Failed to retrieve size for page {pageNum}: {ex.Message}");
            }
        }

        // PdfPageEditor does not implement IDisposable; no explicit disposal required.
    }

    private static void CreateSamplePdf(string path, int pageCount)
    {
        // Generate a minimal PDF in‑memory and save it to the expected location.
        using (Document doc = new Document())
        {
            for (int i = 0; i < pageCount; i++)
            {
                doc.Pages.Add();
            }
            doc.Save(path);
        }
    }
}