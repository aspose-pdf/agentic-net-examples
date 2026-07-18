using System;
using System.IO;
using System.Linq;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the PdfPageEditor facade and bind the source PDF
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);

            // Determine total number of pages (1‑based indexing)
            int totalPages = editor.GetPages();

            // Select every third page (3, 6, 9, …)
            int[] thirdPages = Enumerable.Range(1, totalPages)
                                         .Where(p => p % 3 == 0)
                                         .ToArray();

            // Apply a 270° rotation to the selected pages
            editor.ProcessPages = thirdPages;   // pages to edit
            editor.Rotation     = 270;          // allowed values: 0, 90, 180, 270

            // Commit the changes and save the result
            editor.ApplyChanges();
            editor.Save(outputPath);
            editor.Close();
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}