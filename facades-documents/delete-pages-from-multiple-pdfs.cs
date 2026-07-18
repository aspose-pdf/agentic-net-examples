using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

public static class PdfCleaner
{
    /// <summary>
    /// Deletes the specified pages from each PDF file in <paramref name="pdfPaths"/>
    /// and returns the paths of the cleaned PDFs.
    /// </summary>
    /// <param name="pdfPaths">Collection of source PDF file paths.</param>
    /// <param name="pagesToDelete">Array of page numbers to delete (1‑based indexing).</param>
    /// <returns>List of file paths pointing to the cleaned PDFs.</returns>
    public static List<string> DeletePagesFromPdfs(IEnumerable<string> pdfPaths, int[] pagesToDelete)
    {
        if (pdfPaths == null) throw new ArgumentNullException(nameof(pdfPaths));
        if (pagesToDelete == null) throw new ArgumentNullException(nameof(pagesToDelete));

        var cleanedPaths = new List<string>();
        var editor = new PdfFileEditor(); // Facade for page deletion

        foreach (var inputPath in pdfPaths)
        {
            if (string.IsNullOrWhiteSpace(inputPath))
                continue;

            if (!File.Exists(inputPath))
                throw new FileNotFoundException($"Input PDF not found: {inputPath}");

            // Build output path: original name with "_cleaned" suffix
            string directory = Path.GetDirectoryName(inputPath) ?? string.Empty; // Guard against null
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(directory, $"{fileNameWithoutExt}_cleaned.pdf");

            // Perform deletion and save the result
            bool success = editor.Delete(inputPath, pagesToDelete, outputPath);
            if (!success)
                throw new InvalidOperationException($"Failed to delete pages from {inputPath}");

            cleanedPaths.Add(outputPath);
        }

        // PdfFileEditor does not implement IDisposable; no need to dispose.
        return cleanedPaths;
    }
}

public class Program
{
    /// <summary>
    /// Minimal entry point required for compilation. It demonstrates the usage of
    /// <see cref="PdfCleaner.DeletePagesFromPdfs"/> and parses simple command‑line
    /// arguments.
    /// </summary>
    /// <param name="args">PDF file paths followed optionally by "--pages=1,2,3".</param>
    public static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: <exe> <pdfPath1> [<pdfPath2> ...] [--pages=1,2,3]");
            return;
        }

        var pdfPaths = new List<string>();
        int[] pagesToDelete = { 1 }; // default: delete first page if not supplied

        foreach (var arg in args)
        {
            if (arg.StartsWith("--pages", StringComparison.OrdinalIgnoreCase))
            {
                var split = arg.Split('=', 2);
                if (split.Length == 2 && !string.IsNullOrWhiteSpace(split[1]))
                {
                    pagesToDelete = Array.ConvertAll(split[1].Split(','), s => int.Parse(s.Trim()));
                }
            }
            else
            {
                pdfPaths.Add(arg);
            }
        }

        try
        {
            var cleaned = PdfCleaner.DeletePagesFromPdfs(pdfPaths, pagesToDelete);
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
