using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class PdfPageRemovalUtility
{
    /// <summary>
    /// Removes the specified pages from each PDF file in <paramref name="inputFiles"/>
    /// and saves the result to <paramref name="outputDirectory"/>. Processing is done in parallel.
    /// </summary>
    /// <param name="inputFiles">Full paths of source PDF files.</param>
    /// <param name="pagesToRemove">Page numbers to delete (1‑based indexing).</param>
    /// <param name="outputDirectory">Folder where the processed PDFs will be written.</param>
    public static void RemovePagesFromPdfs(IEnumerable<string> inputFiles, IEnumerable<int> pagesToRemove, string outputDirectory)
    {
        // Ensure the output folder exists
        Directory.CreateDirectory(outputDirectory);

        // Convert the page numbers to an array once – PdfFileEditor expects int[]
        int[] pagesArray = new List<int>(pagesToRemove).ToArray();

        // Process each file concurrently
        Parallel.ForEach(inputFiles, inputFile =>
        {
            try
            {
                // Validate source file existence
                if (!File.Exists(inputFile))
                {
                    Console.Error.WriteLine($"Source file not found: {inputFile}");
                    return;
                }

                // Build output file path (e.g., "document_removed.pdf")
                string outputFile = Path.Combine(
                    outputDirectory,
                    Path.GetFileNameWithoutExtension(inputFile) + "_removed.pdf");

                // PdfFileEditor does NOT implement IDisposable – instantiate directly
                PdfFileEditor editor = new PdfFileEditor();

                // Use the Delete method that accepts file paths and an int[] of pages
                // This follows the official Aspose.Pdf.Facades rule for page removal.
                bool success = editor.Delete(inputFile, pagesArray, outputFile);

                if (success)
                {
                    Console.WriteLine($"Processed: {inputFile} → {outputFile}");
                }
                else
                {
                    Console.Error.WriteLine($"Failed to delete pages from: {inputFile}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputFile}': {ex.Message}");
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

        // Destination folder for the output PDFs
        string outputFolder = @"C:\Docs\Processed";

        RemovePagesFromPdfs(pdfFiles, pages, outputFolder);
    }
}