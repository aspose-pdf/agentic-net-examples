using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        int[] pagesToDelete = new int[] { 2, 3 };

        // Ensure the output directory exists (create it if necessary)
        EnsureOutputDirectory(outputPath);

        try
        {
            // Perform the delete operation with robust error handling for missing input files
            DeletePages(inputPath, pagesToDelete, outputPath);

            Console.WriteLine($"Pages {string.Join(", ", pagesToDelete)} deleted successfully.");
            Console.WriteLine($"Result saved to '{outputPath}'.");
        }
        catch (FileNotFoundException ex)
        {
            // This can be thrown by Delete if the input file disappears after the existence check
            Console.Error.WriteLine($"File not found during processing: {ex.Message}");
        }
        catch (DirectoryNotFoundException ex)
        {
            // Thrown if the output directory cannot be resolved
            Console.Error.WriteLine($"Output directory not found: {ex.Message}");
        }
        catch (InvalidPdfFileFormatException ex)
        {
            // Handles cases where the input file is not a valid PDF
            Console.Error.WriteLine($"Invalid PDF format: {ex.Message}");
        }
        catch (ArgumentException ex)
        {
            // Handles invalid path arguments, nulls, etc.
            Console.Error.WriteLine($"Invalid argument: {ex.Message}");
        }
        catch (Exception ex)
        {
            // General fallback for any other unexpected errors
            Console.Error.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }

    /// <summary>
    /// Deletes the specified pages from a PDF file using the file‑path overload of PdfFileEditor.Delete.
    /// Includes explicit handling for a missing input file.
    /// </summary>
    /// <param name="inputPath">Full path to the source PDF.</param>
    /// <param name="pages">Array of page numbers to delete (1‑based).</param>
    /// <param name="outputPath">Full path where the resulting PDF should be saved.</param>
    private static void DeletePages(string inputPath, int[] pages, string outputPath)
    {
        // Verify that the input file exists before invoking Delete.
        if (!File.Exists(inputPath))
        {
            // Throw a FileNotFoundException that will be caught by the caller.
            throw new FileNotFoundException($"Input file not found – '{inputPath}'.");
        }

        // PdfFileEditor does not implement IDisposable, so no using block is required.
        var editor = new PdfFileEditor();
        // Delete the specified pages and save to the output file using the file‑path overload.
        editor.Delete(inputPath, pages, outputPath);
    }

    /// <summary>
    /// Ensures that the directory for the given output path exists.
    /// If the directory cannot be created, an exception is thrown and handled by the caller.
    /// </summary>
    /// <param name="outputPath">The full path of the output file.</param>
    private static void EnsureOutputDirectory(string outputPath)
    {
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }
    }
}
