using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Array of source PDF files to process
        string[] inputFiles = { "doc1.pdf", "doc2.pdf", "doc3.pdf" };

        // Pages to delete from each source file (example: delete pages 2 and 3)
        int[] pagesToDelete = new int[] { 2, 3 };

        // Temporary folder for intermediate cleaned PDFs
        string tempDir = Path.Combine(Path.GetTempPath(), "PdfBatchTemp");
        Directory.CreateDirectory(tempDir);

        // List to collect paths of cleaned PDFs
        List<string> cleanedFiles = new List<string>();

        // PdfFileEditor does NOT implement IDisposable, so we instantiate it once
        PdfFileEditor editor = new PdfFileEditor();

        foreach (string inputPath in inputFiles)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"Input file not found: {inputPath}");
                continue;
            }

            // Build path for the cleaned version of the current PDF
            string cleanedPath = Path.Combine(
                tempDir,
                Path.GetFileNameWithoutExtension(inputPath) + "_clean.pdf");

            // Delete the specified pages and write the result to cleanedPath
            bool deleteSuccess = editor.Delete(inputPath, pagesToDelete, cleanedPath);
            if (!deleteSuccess)
            {
                Console.Error.WriteLine($"Failed to delete pages from {inputPath}");
                continue;
            }

            cleanedFiles.Add(cleanedPath);
        }

        // Concatenate all cleaned PDFs into a single output file
        if (cleanedFiles.Count > 0)
        {
            string outputPath = "merged_output.pdf";

            bool concatSuccess = editor.Concatenate(cleanedFiles.ToArray(), outputPath);
            if (concatSuccess)
            {
                Console.WriteLine($"Merged PDF saved to '{outputPath}'.");
            }
            else
            {
                Console.Error.WriteLine("Failed to concatenate cleaned PDFs.");
            }
        }

        // Clean up temporary files and directory
        foreach (string file in cleanedFiles)
        {
            try { File.Delete(file); } catch { }
        }
        try { Directory.Delete(tempDir, true); } catch { }
    }
}