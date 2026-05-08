using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class PdfBatchPageDeleter
{
    /// <summary>
    /// Deletes the specified pages from each PDF file in the input collection in parallel.
    /// The resulting PDFs are saved to the output directory with the same file name
    /// appended by "_deleted".
    /// </summary>
    /// <param name="inputFiles">Full paths of the source PDF files.</param>
    /// <param name="pagesToDelete">Array of page numbers to delete (1‑based indexing).</param>
    /// <param name="outputDirectory">Directory where the processed PDFs will be written.</param>
    public static void DeletePagesParallel(string[] inputFiles, int[] pagesToDelete, string outputDirectory)
    {
        if (inputFiles == null) throw new ArgumentNullException(nameof(inputFiles));
        if (pagesToDelete == null) throw new ArgumentNullException(nameof(pagesToDelete));
        if (string.IsNullOrWhiteSpace(outputDirectory)) throw new ArgumentException("Output directory must be specified.", nameof(outputDirectory));

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputDirectory);

        // Process each file concurrently.
        Parallel.ForEach(inputFiles, inputPath =>
        {
            try
            {
                if (!File.Exists(inputPath))
                {
                    Console.Error.WriteLine($"File not found: {inputPath}");
                    return;
                }

                // Build output file name.
                string fileName = Path.GetFileNameWithoutExtension(inputPath);
                string outputPath = Path.Combine(outputDirectory, $"{fileName}_deleted.pdf");

                // Each thread uses its own PdfFileEditor instance.
                PdfFileEditor editor = new PdfFileEditor();

                // Delete the pages. The Delete method returns true on success.
                bool success = editor.Delete(inputPath, pagesToDelete, outputPath);

                if (success)
                {
                    Console.WriteLine($"Deleted pages from '{inputPath}' → '{outputPath}'");
                }
                else
                {
                    Console.Error.WriteLine($"Failed to delete pages from '{inputPath}'.");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        });
    }

    // Example usage.
    static void Main()
    {
        // Example input PDFs.
        string[] pdfFiles = new string[]
        {
            "C:\\Docs\\Report1.pdf",
            "C:\\Docs\\Report2.pdf",
            "C:\\Docs\\Report3.pdf"
        };

        // Pages to delete (e.g., remove pages 2 and 5 from each document).
        int[] pagesToRemove = new int[] { 2, 5 };

        // Destination folder for the processed PDFs.
        string outputFolder = "C:\\Docs\\Processed";

        DeletePagesParallel(pdfFiles, pagesToRemove, outputFolder);
    }
}