using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files and the pages that should be removed from each file.
        // Page numbers are 1‑based as required by Aspose.Pdf.
        var filesToProcess = new Dictionary<string, int[]>
        {
            { "input1.pdf", new int[] { 2, 3 } },          // delete pages 2 and 3 from input1.pdf
            { "input2.pdf", new int[] { 1 } },             // delete page 1 from input2.pdf
            { "input3.pdf", new int[] { 5, 6, 7 } }        // delete pages 5‑7 from input3.pdf
        };

        // Folder for intermediate cleaned PDFs.
        string tempFolder = Path.Combine(Path.GetTempPath(), "PdfBatchTemp");
        Directory.CreateDirectory(tempFolder);

        // List that will hold the paths of the cleaned PDFs.
        var cleanedFiles = new List<string>();

        // PdfFileEditor does NOT implement IDisposable – do NOT wrap it in a using block.
        PdfFileEditor editor = new PdfFileEditor();

        foreach (var kvp in filesToProcess)
        {
            string sourcePath = kvp.Key;
            int[] pagesToDelete = kvp.Value;

            if (!File.Exists(sourcePath))
            {
                Console.Error.WriteLine($"Source file not found: {sourcePath}");
                continue;
            }

            // Build a temporary file name for the cleaned PDF.
            string cleanedPath = Path.Combine(tempFolder,
                Path.GetFileNameWithoutExtension(sourcePath) + "_cleaned.pdf");

            // Delete the specified pages and write the result to the temporary file.
            // Delete returns true on success; we ignore the return value here but could check it.
            editor.Delete(sourcePath, pagesToDelete, cleanedPath);

            cleanedFiles.Add(cleanedPath);
        }

        // Final concatenated output.
        string outputPath = "merged_output.pdf";

        if (cleanedFiles.Count > 0)
        {
            // Concatenate all cleaned PDFs into a single document.
            editor.Concatenate(cleanedFiles.ToArray(), outputPath);
            Console.WriteLine($"Merged PDF created at: {outputPath}");
        }
        else
        {
            Console.Error.WriteLine("No cleaned files were generated; concatenation skipped.");
        }

        // Optional: clean up temporary files.
        foreach (string file in cleanedFiles)
        {
            try { File.Delete(file); } catch { /* ignore cleanup errors */ }
        }

        // Remove the temporary folder if it is empty.
        try { Directory.Delete(tempFolder, true); } catch { /* ignore */ }
    }
}