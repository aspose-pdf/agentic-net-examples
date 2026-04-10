using System;
using System.IO;
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

        // Define zoom factors for each page (1‑based indexing).
        // If fewer values are provided than pages, remaining pages use 1.0 (100%).
        float[] zoomPerPage = { 0.5f, 1.0f, 1.5f }; // example values

        // PdfPageEditor implements IDisposable, so use a using block.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF.
            editor.BindPdf(inputPath);

            // Iterate over all pages.
            for (int pageIndex = 1; pageIndex <= editor.GetPages(); pageIndex++)
            {
                // Choose zoom for the current page.
                float zoom = pageIndex <= zoomPerPage.Length ? zoomPerPage[pageIndex - 1] : 1.0f;

                // Restrict editing to the current page.
                editor.ProcessPages = new int[] { pageIndex };

                // Apply the zoom factor.
                editor.Zoom = zoom;

                // Commit the change for this page.
                editor.ApplyChanges();
            }

            // Save the modified PDF.
            editor.Save(outputPath);
            editor.Close();
        }

        Console.WriteLine($"Zoomed PDF saved to '{outputPath}'.");
    }
}