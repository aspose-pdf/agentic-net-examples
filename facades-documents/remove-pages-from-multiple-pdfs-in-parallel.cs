using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf.Facades; // PdfFileEditor resides here

class PdfPageRemovalUtility
{
    /// <summary>
    /// Removes the specified pages from each PDF file in the input collection.
    /// The operation is performed in parallel.
    /// </summary>
    /// <param name="inputPdfPaths">Full paths of the source PDF files.</param>
    /// <param name="pagesToRemove">Page numbers (1‑based) that should be deleted from each file.</param>
    /// <param name="outputDirectory">Directory where the processed PDFs will be saved.</param>
    public static void RemovePagesFromPdfs(IEnumerable<string> inputPdfPaths, IEnumerable<int> pagesToRemove, string outputDirectory)
    {
        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Convert the page numbers to an array once (PdfFileEditor expects int[])
        int[] pagesArray = new List<int>(pagesToRemove).ToArray();

        // Process each PDF file in parallel
        Parallel.ForEach(inputPdfPaths, inputPath =>
        {
            try
            {
                // Validate source file
                if (!File.Exists(inputPath))
                {
                    Console.Error.WriteLine($"Source file not found: {inputPath}");
                    return;
                }

                // Determine output file name (original name with "_modified" suffix)
                string fileName = Path.GetFileNameWithoutExtension(inputPath);
                string outputPath = Path.Combine(outputDirectory, $"{fileName}_modified.pdf");

                // Use PdfFileEditor.Delete(string, int[], string) to remove pages and save
                PdfFileEditor editor = new PdfFileEditor();
                bool success = editor.Delete(inputPath, pagesArray, outputPath);

                if (success)
                {
                    Console.WriteLine($"Processed: {inputPath} -> {outputPath}");
                }
                else
                {
                    Console.Error.WriteLine($"Failed to delete pages from: {inputPath}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        });
    }

    // Example usage
    static void Main()
    {
        // List of PDF files to process
        var pdfFiles = new List<string>
        {
            @"C:\Docs\Report1.pdf",
            @"C:\Docs\Report2.pdf",
            @"C:\Docs\Report3.pdf"
        };

        // Pages to remove (example: remove pages 2 and 5)
        var pages = new List<int> { 2, 5 };

        // Output folder
        string outFolder = @"C:\Docs\Processed";

        // Execute the removal
        RemovePagesFromPdfs(pdfFiles, pages, outFolder);
    }
}