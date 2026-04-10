using System;
using System.Collections.Generic;
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

        try
        {
            // PdfPageEditor is a facade that implements IDisposable.
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Load the source PDF.
                editor.BindPdf(inputPath);

                // Determine total number of pages.
                int totalPages = editor.GetPages();

                // Build a list of odd‑numbered page indexes (1‑based).
                List<int> oddPages = new List<int>();
                for (int i = 1; i <= totalPages; i += 2)
                {
                    oddPages.Add(i);
                }

                // Specify that only the odd pages should be edited.
                editor.ProcessPages = oddPages.ToArray();

                // Set the zoom factor (1.0 = 100%). 1.2 = 120%.
                editor.Zoom = 1.2f;

                // Apply the changes to the selected pages.
                editor.ApplyChanges();

                // Save the modified PDF.
                editor.Save(outputPath);
            }

            Console.WriteLine($"Zoom applied to odd pages. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}