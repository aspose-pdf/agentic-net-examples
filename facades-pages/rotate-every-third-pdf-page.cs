using System;
using System.IO;
using System.Collections.Generic;
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

        // Rotate every third page (3,6,9,…) by 270° using PdfPageEditor
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF
            editor.BindPdf(inputPath);

            // Prepare per‑page rotation dictionary (1‑based page numbers)
            var pageRotations = new Dictionary<int, int>();

            int pageCount = editor.GetPages();

            for (int page = 3; page <= pageCount; page += 3)
            {
                pageRotations[page] = 270; // valid values: 0, 90, 180, 270
            }

            // Assign the rotation dictionary
            editor.PageRotations = pageRotations;

            // Apply changes and save the result
            editor.ApplyChanges();
            editor.Save(outputPath);
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}
