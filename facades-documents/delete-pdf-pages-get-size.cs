using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;

public static class PdfUtilities
{
    /// <summary>
    /// Deletes the specified pages from a PDF file and returns the size (in bytes) of the resulting file.
    /// </summary>
    /// <param name="pdfPath">Full path to the source PDF.</param>
    /// <param name="pageNumbers">Array of 1‑based page numbers to delete.</param>
    /// <returns>File size of the PDF after deletion, in bytes.</returns>
    public static long DeletePagesAndGetSize(string pdfPath, int[] pageNumbers)
    {
        if (string.IsNullOrWhiteSpace(pdfPath))
            throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

        if (!File.Exists(pdfPath))
            throw new FileNotFoundException("Source PDF not found.", pdfPath);

        if (pageNumbers == null || pageNumbers.Length == 0)
            throw new ArgumentException("At least one page number must be specified.", nameof(pageNumbers));

        // Load the PDF using Aspose.Pdf.Document (PdfFileEditor does not expose DeletePages).
        Document pdfDoc = new Document(pdfPath);

        // Delete pages in descending order to keep indexes valid.
        foreach (int pageNum in pageNumbers.OrderByDescending(p => p))
        {
            // Guard against out‑of‑range page numbers.
            if (pageNum < 1 || pageNum > pdfDoc.Pages.Count)
                throw new ArgumentOutOfRangeException(nameof(pageNumbers), $"Page number {pageNum} is out of range.");

            pdfDoc.Pages.Delete(pageNum);
        }

        // Prepare a temporary file path.
        string directory = Path.GetDirectoryName(pdfPath) ?? string.Empty;
        string tempPath = Path.Combine(directory,
            Path.GetFileNameWithoutExtension(pdfPath) + "_temp.pdf");

        // Save the modified document to the temporary file.
        pdfDoc.Save(tempPath);

        // Get the size of the resulting PDF.
        long resultSize = new FileInfo(tempPath).Length;

        // Replace the original file with the modified one.
        File.Delete(pdfPath);
        File.Move(tempPath, pdfPath);

        return resultSize;
    }

    // Simple entry point to satisfy the compiler when the project is built as an executable.
    // This can be removed if the project is changed to a class‑library.
    public static void Main(string[] args)
    {
        // Example usage (not required for the library functionality).
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <pdfPath> <page1> [<page2> ...]");
            return;
        }

        string pdfPath = args[0];
        int[] pagesToDelete = args.Skip(1).Select(arg => int.Parse(arg)).ToArray();
        long newSize = DeletePagesAndGetSize(pdfPath, pagesToDelete);
        Console.WriteLine($"New PDF size: {newSize} bytes");
    }
}
