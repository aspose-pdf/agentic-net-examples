using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfPageEditor is a facade that allows page‑level editing (zoom, rotation, etc.).
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF.
            editor.BindPdf(inputPath);

            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing).
            for (int i = 1; i <= editor.GetPages(); i++)
            {
                // Example zoom logic: 0.5× for page 1, 1.0× for page 2, 1.5× for page 3, …
                float zoomFactor = 0.5f + 0.5f * (i - 1);

                // Restrict the operation to the current page.
                editor.ProcessPages = new int[] { i };

                // Apply the zoom factor to the selected page.
                editor.Zoom = zoomFactor;

                // Commit the change for this page.
                editor.ApplyChanges();
            }

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Zoom‑adjusted PDF saved to '{outputPath}'.");
    }
}