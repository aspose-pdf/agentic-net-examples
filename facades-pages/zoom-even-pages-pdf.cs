using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_even_zoom.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // PdfPageEditor implements IDisposable, so use a using block.
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Load the source PDF.
                editor.BindPdf(inputPath);

                // Get total number of pages (1‑based indexing).
                int totalPages = editor.GetPages();

                // Build an array containing all even page numbers.
                int evenCount = totalPages / 2;
                int[] evenPages = new int[evenCount];
                int idx = 0;
                for (int i = 2; i <= totalPages; i += 2)
                {
                    evenPages[idx++] = i;
                }

                // Restrict editing to the even pages.
                editor.ProcessPages = evenPages;

                // Apply a zoom factor of 1.2 (120%).
                editor.Zoom = 1.2f;

                // Commit the changes to the document.
                editor.ApplyChanges();

                // Save the modified PDF.
                editor.Save(outputPath);
            }

            Console.WriteLine($"Even pages zoomed to 1.2x and saved as '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}