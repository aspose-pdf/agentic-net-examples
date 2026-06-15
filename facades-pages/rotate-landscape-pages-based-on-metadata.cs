using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Retrieve custom metadata that lists pages needing landscape rotation.
        // Expected format: "2,4,5" (comma‑separated page numbers, 1‑based).
        var landscapePages = new HashSet<int>();
        using (PdfFileInfo fileInfo = new PdfFileInfo(inputPath))
        {
            string meta = fileInfo.GetMetaInfo("LandscapePages");
            if (!string.IsNullOrWhiteSpace(meta))
            {
                foreach (string part in meta.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (int.TryParse(part.Trim(), out int pageNum) && pageNum > 0)
                        landscapePages.Add(pageNum);
                }
            }
        }

        // If no pages are marked, simply copy the file.
        if (landscapePages.Count == 0)
        {
            File.Copy(inputPath, outputPath, true);
            Console.WriteLine("No landscape pages detected – file copied unchanged.");
            return;
        }

        // Use PdfPageEditor to rotate the specified pages.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the source PDF.
            editor.BindPdf(inputPath);

            // Prepare a dictionary where key = page number (1‑based) and value = rotation in degrees.
            var rotations = new Dictionary<int, int>();
            foreach (int pageNum in landscapePages)
                rotations[pageNum] = 90; // rotate 90° to landscape

            editor.PageRotations = rotations;

            // Apply the rotation changes.
            editor.ApplyChanges();

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}
