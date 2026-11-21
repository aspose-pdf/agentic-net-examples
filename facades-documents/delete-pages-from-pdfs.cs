using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Pdf;

public static class PdfCleaner
{
    /// <summary>
    /// Deletes the specified pages from each PDF file and returns the paths of the cleaned PDFs.
    /// </summary>
    /// <param name="pdfFilePaths">List of source PDF file paths.</param>
    /// <param name="pagesToDeletePerFile">
    /// List of integer arrays where each array contains the 1‑based page numbers to delete
    /// from the PDF at the same index in <paramref name="pdfFilePaths"/>.
    /// </param>
    /// <returns>List of file paths pointing to the cleaned PDFs.</returns>
    public static List<string> DeletePagesFromPdfs(
        List<string> pdfFilePaths,
        List<int[]> pagesToDeletePerFile)
    {
        if (pdfFilePaths == null) throw new ArgumentNullException(nameof(pdfFilePaths));
        if (pagesToDeletePerFile == null) throw new ArgumentNullException(nameof(pagesToDeletePerFile));
        if (pdfFilePaths.Count != pagesToDeletePerFile.Count)
            throw new ArgumentException("The number of PDF files must match the number of page‑deletion arrays.");

        var cleanedPaths = new List<string>(pdfFilePaths.Count);

        for (int i = 0; i < pdfFilePaths.Count; i++)
        {
            string sourcePath = pdfFilePaths[i];
            int[] pagesToDelete = pagesToDeletePerFile[i] ?? Array.Empty<int>();

            if (!File.Exists(sourcePath))
                throw new FileNotFoundException($"Source PDF not found: {sourcePath}");

            // Build output path – insert "_cleaned" before the extension.
            string dir = Path.GetDirectoryName(sourcePath) ?? string.Empty;
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(sourcePath);
            string ext = Path.GetExtension(sourcePath);
            string outputPath = Path.Combine(dir, $"{fileNameWithoutExt}_cleaned{ext}");

            if (pagesToDelete.Length > 0)
            {
                // Load the PDF with Document (Aspose.Pdf) – PdfFileEditor has no DeletePages method.
                using (var doc = new Document(sourcePath))
                {
                    // Delete pages in descending order to keep indexes stable.
                    var distinctPages = pagesToDelete
                        .Where(p => p >= 1 && p <= doc.Pages.Count) // guard against out‑of‑range values
                        .Distinct()
                        .OrderByDescending(p => p);

                    foreach (int pageNumber in distinctPages)
                    {
                        doc.Pages.Delete(pageNumber);
                    }

                    doc.Save(outputPath);
                }
            }
            else
            {
                // No pages to delete – copy the original file.
                File.Copy(sourcePath, outputPath, overwrite: true);
            }

            cleanedPaths.Add(outputPath);
        }

        return cleanedPaths;
    }
}

public class Program
{
    /// <summary>
    /// Entry point required for a console application. The implementation is minimal –
    /// it simply demonstrates how the <see cref="PdfCleaner.DeletePagesFromPdfs"/> method
    /// could be invoked when command‑line arguments are supplied.
    /// </summary>
    public static void Main(string[] args)
    {
        // If no arguments are provided, exit silently. This keeps the method usable as a library.
        if (args.Length == 0)
            return;

        // Expected argument format per PDF: "sourcePath|page1,page2,..."
        // Example: "C:\Docs\file1.pdf|2,5,7"
        var pdfPaths = new List<string>();
        var pagesPerFile = new List<int[]>();

        foreach (var arg in args)
        {
            var parts = arg.Split('|');
            if (parts.Length != 2)
                continue; // skip malformed argument

            var source = parts[0];
            var pageTokens = parts[1].Split(',', StringSplitOptions.RemoveEmptyEntries);
            var pageNumbers = Array.ConvertAll(pageTokens, t => int.Parse(t.Trim()));

            pdfPaths.Add(source);
            pagesPerFile.Add(pageNumbers);
        }

        // Perform the cleaning operation.
        var cleaned = PdfCleaner.DeletePagesFromPdfs(pdfPaths, pagesPerFile);

        // Output the resulting file paths – useful for scripts.
        foreach (var path in cleaned)
        {
            Console.WriteLine(path);
        }
    }
}
