using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF to be split
        const string inputPdf = "input.pdf";

        // Folder where the split PDFs will be saved
        const string outputFolder = "SplitBulks";

        // Define page ranges for each bulk file (inclusive, 1‑based indexing)
        // Example: first bulk contains pages 1‑3, second bulk 4‑6, third bulk 7‑end
        string[] pageRangeStrings = { "1-3", "4-6", "7-" };

        // Validate input file
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Convert the string ranges to the int[][] format required by SplitToBulks
        int[][] pageRanges = ParsePageRanges(pageRangeStrings);

        // PdfFileEditor does NOT implement IDisposable – do NOT wrap in using
        PdfFileEditor editor = new PdfFileEditor();

        try
        {
            // Split the source PDF into multiple MemoryStreams according to the defined ranges
            MemoryStream[] bulks = editor.SplitToBulks(inputPdf, pageRanges);

            // Save each bulk to a file in the output folder
            for (int i = 0; i < bulks.Length; i++)
            {
                string outputPath = Path.Combine(outputFolder, $"input_{i + 1}.pdf");
                using (FileStream file = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    bulks[i].Position = 0; // reset stream position before copying
                    bulks[i].CopyTo(file);
                }
            }

            Console.WriteLine($"PDF split into bulks successfully. Files are in '{outputFolder}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during split: {ex.Message}");
        }
    }

    /// <summary>
    /// Parses an array of page‑range strings (e.g., "1-3", "7-") into the int[][] format
    /// required by PdfFileEditor.SplitToBulks. A missing end page is represented by -1.
    /// </summary>
    private static int[][] ParsePageRanges(string[] ranges)
    {
        var result = new List<int[]>();
        foreach (var r in ranges)
        {
            if (string.IsNullOrWhiteSpace(r))
                continue;

            string[] parts = r.Split('-');
            if (parts.Length != 2)
                throw new ArgumentException($"Invalid page range format: '{r}'. Expected 'start-end' or 'start-'.");

            int start = int.Parse(parts[0]);
            int end = string.IsNullOrEmpty(parts[1]) ? -1 : int.Parse(parts[1]);
            result.Add(new[] { start, end });
        }
        return result.ToArray();
    }
}
