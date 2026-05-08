using System;
using System.IO;
using Aspose.Pdf.Facades;

class BatchPdfProcessor
{
    // Entry point must be public static for the compiler to recognize it.
    public static void Main(string[] args)
    {
        // Input PDF files to process
        string[] inputFiles = new string[]
        {
            "doc1.pdf",
            "doc2.pdf",
            "doc3.pdf"
        };

        // Pages to delete from each corresponding PDF (1‑based indexing)
        // Example: delete pages 2 and 3 from the first file, page 5 from the second, none from the third
        int[][] pagesToDelete = new int[][]
        {
            new int[] { 2, 3 },
            new int[] { 5 },
            new int[] { } // no pages to delete
        };

        // Validate that the arrays match
        if (inputFiles.Length != pagesToDelete.Length)
        {
            Console.Error.WriteLine("The number of input files must match the number of page‑deletion specifications.");
            return;
        }

        // Create a temporary folder to store the intermediate cleaned PDFs
        string tempFolder = Path.Combine(Path.GetTempPath(), "PdfBatchProcess_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Array to hold paths of the cleaned PDFs
        string[] cleanedFiles = new string[inputFiles.Length];

        // Delete the specified pages from each input PDF
        for (int i = 0; i < inputFiles.Length; i++)
        {
            string inputPath = inputFiles[i];
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"Input file not found: {inputPath}");
                continue;
            }

            string cleanedPath = Path.Combine(tempFolder, $"cleaned_{i}.pdf");
            cleanedFiles[i] = cleanedPath;

            // PdfFileEditor does NOT implement IDisposable; instantiate directly.
            var editor = new PdfFileEditor();
            try
            {
                // Delete the pages; Delete returns void, so we just call it.
                editor.Delete(inputPath, pagesToDelete[i], cleanedPath);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to delete pages from {inputPath}: {ex.Message}. Copying original file instead.");
                // If deletion fails, fall back to copying the original file
                File.Copy(inputPath, cleanedPath, overwrite: true);
            }
        }

        // Define the final merged output file
        string outputPath = "merged_output.pdf";

        // Concatenate all cleaned PDFs into a single document
        var concatEditor = new PdfFileEditor();
        try
        {
            // Concatenate also returns void.
            concatEditor.Concatenate(cleanedFiles, outputPath);
            Console.WriteLine($"Successfully created merged PDF: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to concatenate the cleaned PDF files: {ex.Message}");
        }

        // Optional: clean up temporary files
        try
        {
            foreach (var file in cleanedFiles)
            {
                if (File.Exists(file))
                    File.Delete(file);
            }
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, recursive: true);
        }
        catch (Exception ex)
        {
            // Suppress any cleanup errors but log them for diagnostics
            Console.Error.WriteLine($"Cleanup error: {ex.Message}");
        }
    }
}