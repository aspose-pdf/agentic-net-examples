using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Map each input PDF to the pages that should be removed (1‑based indexing)
        var inputs = new Dictionary<string, int[]>
        {
            { "doc1.pdf", new int[] { 2, 3 } }, // delete pages 2 and 3
            { "doc2.pdf", new int[] { 1 } },    // delete page 1
            { "doc3.pdf", new int[0] }          // no pages to delete
        };

        // Temporary folder for intermediate cleaned PDFs
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposePdfBatch");
        Directory.CreateDirectory(tempFolder);

        var cleanedFiles = new List<string>();
        PdfFileEditor editor = new PdfFileEditor();

        foreach (var kvp in inputs)
        {
            string inputPath = kvp.Key;
            int[] pagesToDelete = kvp.Value;

            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"Input file not found: {inputPath}");
                continue;
            }

            // If there are no pages to delete, use the original file directly
            if (pagesToDelete == null || pagesToDelete.Length == 0)
            {
                cleanedFiles.Add(inputPath);
                continue;
            }

            // Create a cleaned version of the PDF
            string cleanedPath = Path.Combine(
                tempFolder,
                Path.GetFileNameWithoutExtension(inputPath) + "_cleaned.pdf");

            bool deleteSuccess = editor.Delete(inputPath, pagesToDelete, cleanedPath);
            if (!deleteSuccess)
            {
                Console.Error.WriteLine($"Failed to delete pages from {inputPath}");
                continue;
            }

            cleanedFiles.Add(cleanedPath);
        }

        // Concatenate all cleaned PDFs into a single document
        string outputPath = "merged_output.pdf";

        if (cleanedFiles.Count == 0)
        {
            Console.Error.WriteLine("No files available for concatenation.");
            return;
        }

        bool concatSuccess = editor.Concatenate(cleanedFiles.ToArray(), outputPath);
        if (!concatSuccess)
        {
            Console.Error.WriteLine("Concatenation failed.");
        }
        else
        {
            Console.WriteLine($"Merged PDF saved to '{outputPath}'.");
        }

        // Optional cleanup of temporary cleaned files
        foreach (string file in cleanedFiles)
        {
            if (file.EndsWith("_cleaned.pdf", StringComparison.OrdinalIgnoreCase) && File.Exists(file))
            {
                try { File.Delete(file); } catch { }
            }
        }
    }
}