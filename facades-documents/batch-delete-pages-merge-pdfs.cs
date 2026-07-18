using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class BatchPdfProcessor
{
    static void Main()
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
            new int[] { 2, 3 },   // for doc1.pdf
            new int[] { 5 },      // for doc2.pdf
            new int[0]            // for doc3.pdf (no deletion)
        };

        // Validate inputs
        if (inputFiles.Length != pagesToDelete.Length)
        {
            Console.Error.WriteLine("Each input file must have a corresponding pages‑to‑delete entry.");
            return;
        }

        // Temporary folder for intermediate cleaned PDFs
        string tempFolder = Path.Combine(Path.GetTempPath(), "PdfBatchProcessing_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        List<string> cleanedFiles = new List<string>();

        try
        {
            // Step 1: Delete specified pages from each PDF
            for (int i = 0; i < inputFiles.Length; i++)
            {
                string sourcePath = inputFiles[i];
                if (!File.Exists(sourcePath))
                {
                    Console.Error.WriteLine($"Source file not found: {sourcePath}");
                    continue; // skip missing files
                }

                string cleanedPath = Path.Combine(tempFolder, $"cleaned_{i}.pdf");
                Aspose.Pdf.Facades.PdfFileEditor editor = new Aspose.Pdf.Facades.PdfFileEditor();

                // Delete pages; if the array is empty the method still creates a copy
                editor.Delete(sourcePath, pagesToDelete[i], cleanedPath);
                cleanedFiles.Add(cleanedPath);
                Console.WriteLine($"Created cleaned file: {cleanedPath}");
            }

            // Step 2: Concatenate all cleaned PDFs into a single document
            if (cleanedFiles.Count == 0)
            {
                Console.Error.WriteLine("No cleaned files were generated; aborting concatenation.");
                return;
            }

            string outputPath = "merged_output.pdf";
            Aspose.Pdf.Facades.PdfFileEditor concatEditor = new Aspose.Pdf.Facades.PdfFileEditor();
            concatEditor.Concatenate(cleanedFiles.ToArray(), outputPath);
            Console.WriteLine($"Merged PDF saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
        finally
        {
            // Clean up temporary files
            try
            {
                if (Directory.Exists(tempFolder))
                {
                    Directory.Delete(tempFolder, true);
                }
            }
            catch
            {
                // Suppress any cleanup errors
            }
        }
    }
}