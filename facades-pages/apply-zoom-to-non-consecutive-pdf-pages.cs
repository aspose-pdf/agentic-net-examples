using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "selected_zoomed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Non‑consecutive pages to keep (1‑based indexing)
        int[] pagesToSelect = new int[] { 1, 3, 5, 7 };

        // Common zoom factor (e.g., 150 % = 1.5)
        float zoomFactor = 1.5f;

        // ------------------------------------------------------------
        // 1. Extract the required pages using the Document API.
        // ------------------------------------------------------------
        Document sourceDoc = new Document(inputPath);
        Document selectedDoc = new Document();
        foreach (int pageNumber in pagesToSelect)
        {
            // Add a copy of the source page to the new document.
            selectedDoc.Pages.Add(sourceDoc.Pages[pageNumber]);
        }

        // ------------------------------------------------------------
        // 2. Apply the same zoom to all pages with PdfPageEditor.
        // ------------------------------------------------------------
        // Save the selected pages to a memory stream so that PdfPageEditor can bind to it.
        using (MemoryStream ms = new MemoryStream())
        {
            selectedDoc.Save(ms);
            ms.Position = 0; // reset stream position for reading

            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(ms);
                // ProcessPages expects 1‑based page numbers; we want all pages in the temporary PDF.
                editor.ProcessPages = Enumerable.Range(1, selectedDoc.Pages.Count).ToArray();
                editor.Zoom = zoomFactor; // Apply the zoom factor
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Pages {string.Join(", ", pagesToSelect)} saved to '{outputPath}' with zoom factor {zoomFactor}.");
    }
}
