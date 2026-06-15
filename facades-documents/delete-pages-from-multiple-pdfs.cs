using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

public static class PdfCleaner
{
    /// <summary>
    /// Deletes the specified pages from each PDF file in <paramref name="inputFiles"/>
    /// and returns the paths of the cleaned PDFs.
    /// </summary>
    /// <param name="inputFiles">Full paths of source PDF files.</param>
    /// <param name="pagesToDelete">
    /// Zero‑based page numbers to delete (e.g., new int[] { 1, 2 } will delete pages 2 and 3,
    /// because Aspose.Pdf uses 1‑based indexing internally).
    /// </param>
    /// <returns>List of file paths for the cleaned PDFs.</returns>
    public static List<string> DeletePagesFromFiles(IEnumerable<string> inputFiles, int[] pagesToDelete)
    {
        if (pagesToDelete == null || pagesToDelete.Length == 0)
            throw new ArgumentException("pagesToDelete must contain at least one page number.", nameof(pagesToDelete));

        var cleanedFiles = new List<string>();
        var editor = new PdfFileEditor(); // Facade for page deletion

        // Convert zero‑based indices to the 1‑based indices Aspose expects.
        int[] aspPages = new int[pagesToDelete.Length];
        for (int i = 0; i < pagesToDelete.Length; i++)
        {
            aspPages[i] = pagesToDelete[i] + 1;
        }

        foreach (var inputPath in inputFiles)
        {
            if (string.IsNullOrWhiteSpace(inputPath))
                continue; // skip empty entries

            if (!File.Exists(inputPath))
                throw new FileNotFoundException($"Input PDF not found: {inputPath}");

            // Build output path: same folder, same name with "_cleaned" suffix
            string directory = Path.GetDirectoryName(inputPath) ?? string.Empty;
            string filenameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(directory, $"{filenameWithoutExt}_cleaned.pdf");

            // Delete pages and save to outputPath
            bool success = editor.Delete(inputPath, aspPages, outputPath);
            if (!success)
                throw new InvalidOperationException($"Failed to delete pages from {inputPath}");

            cleanedFiles.Add(outputPath);
        }

        // PdfFileEditor does not implement IDisposable, so no using block needed.
        return cleanedFiles;
    }
}

// ---------------------------------------------------------------------------
// Minimal console entry point so the project compiles as an executable.
// ---------------------------------------------------------------------------
public class Program
{
    /// <summary>
    /// Simple demo that calls <see cref="PdfCleaner.DeletePagesFromFiles"/>.
    /// The method expects a list of PDF file paths and an array of zero‑based page numbers to delete.
    /// </summary>
    public static void Main(string[] args)
    {
        // If arguments are supplied, treat them as input file paths.
        // Otherwise, demonstrate with a placeholder (no‑op) to keep the program runnable.
        var inputFiles = new List<string>();
        if (args != null && args.Length > 0)
        {
            inputFiles.AddRange(args);
        }
        else
        {
            // Example placeholder – replace with real paths when using the library.
            // inputFiles.Add(@"C:\Temp\sample1.pdf");
            // inputFiles.Add(@"C:\Temp\sample2.pdf");
        }

        // Example: delete the first page (zero‑based index 0) from each PDF.
        int[] pagesToDelete = new[] { 0 };

        try
        {
            var cleaned = PdfCleaner.DeletePagesFromFiles(inputFiles, pagesToDelete);
            Console.WriteLine("Cleaned PDFs:");
            foreach (var path in cleaned)
            {
                Console.WriteLine(path);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
