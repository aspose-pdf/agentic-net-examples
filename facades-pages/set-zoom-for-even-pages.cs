using System;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the input file exists
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor to modify page zoom
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the PDF document
            editor.BindPdf(inputPath);

            // Determine total number of pages
            int totalPages = editor.GetPages();

            // Build an array containing only even‑numbered pages
            List<int> evenPages = new List<int>();
            for (int page = 2; page <= totalPages; page += 2)
            {
                evenPages.Add(page);
            }

            // Restrict processing to the even pages (int[] required)
            editor.ProcessPages = evenPages.ToArray();

            // Set zoom factor to 0.8 (80%)
            editor.Zoom = 0.8f;

            // Apply the changes to the selected pages
            editor.ApplyChanges();

            // Save the modified PDF
            editor.Save(outputPath);

            // Release resources associated with the editor
            editor.Close();
        }

        Console.WriteLine($"Zoom applied to even pages. Output saved to '{outputPath}'.");
    }
}
