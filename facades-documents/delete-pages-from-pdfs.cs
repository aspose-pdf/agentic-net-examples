using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf; // Document class for PDF manipulation

public static class PdfCleaner
{
    /// <summary>
    /// Deletes the specified pages from each PDF file and returns the paths of the cleaned PDFs.
    /// The cleaned files are saved alongside the original files with a "_cleaned.pdf" suffix.
    /// </summary>
    /// <param name="pdfPaths">List of source PDF file paths.</param>
    /// <param name="pagesToDelete">
    /// Dictionary where the key is the source PDF path and the value is an array of 1‑based page numbers to delete.
    /// If a PDF path is not present in the dictionary, the file is copied unchanged.
    /// </param>
    /// <returns>List of file paths pointing to the cleaned PDFs.</returns>
    public static List<string> DeletePagesFromPdfs(
        List<string> pdfPaths,
        Dictionary<string, int[]> pagesToDelete)
    {
        var cleanedPaths = new List<string>();

        foreach (string srcPath in pdfPaths)
        {
            if (string.IsNullOrWhiteSpace(srcPath) || !File.Exists(srcPath))
            {
                Console.Error.WriteLine($"File not found or invalid path: {srcPath ?? "<null>"}");
                continue;
            }

            // Determine output file name
            string dir = Path.GetDirectoryName(srcPath) ?? string.Empty;
            string nameWithoutExt = Path.GetFileNameWithoutExtension(srcPath);
            string destPath = Path.Combine(dir, $"{nameWithoutExt}_cleaned.pdf");

            // Load the PDF document
            Document pdfDoc = new Document(srcPath);

            // Check if there are pages to delete for this file
            if (pagesToDelete != null && pagesToDelete.TryGetValue(srcPath, out int[] pages) && pages?.Length > 0)
            {
                // Aspose.Pdf uses 1‑based page indexing; ensure the caller follows this rule.
                // Delete pages in descending order to keep indexes stable when using Delete(pageNumber).
                // However, Document.Pages.Delete(int[]) handles the collection internally, so we can call it directly.
                if (pages.Length == 1)
                {
                    pdfDoc.Pages.Delete(pages[0]);
                }
                else
                {
                    pdfDoc.Pages.Delete(pages);
                }
            }
            // Save the (potentially modified) document to the destination path
            pdfDoc.Save(destPath);

            cleanedPaths.Add(destPath);
        }

        return cleanedPaths;
    }

    // Dummy entry point to satisfy the compiler when the project is built as an executable.
    // In a library scenario this method can be removed.
    public static void Main(string[] args)
    {
        // Example usage (can be removed or replaced by real arguments)
        var pdfs = new List<string> { "sample1.pdf", "sample2.pdf" };
        var pagesMap = new Dictionary<string, int[]>
        {
            { "sample1.pdf", new[] { 2, 4 } }, // delete pages 2 and 4 from sample1.pdf
            // sample2.pdf will be copied unchanged because it is not in the dictionary
        };
        var result = DeletePagesFromPdfs(pdfs, pagesMap);
        Console.WriteLine("Cleaned PDFs:");
        foreach (var path in result)
        {
            Console.WriteLine(path);
        }
    }
}
