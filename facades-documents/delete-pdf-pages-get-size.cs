using System;
using System.IO;
using Aspose.Pdf.Facades;

public class PdfUtility
{
    /// <summary>
    /// Deletes the specified pages from a PDF file using Aspose.Pdf.Facades.PdfFileEditor
    /// and returns the size (in bytes) of the resulting PDF.
    /// </summary>
    /// <param name="pdfPath">Full path to the source PDF file.</param>
    /// <param name="pagesToDelete">Array of page numbers to delete (1‑based indexing).</param>
    /// <returns>File size of the PDF after deletion, in bytes.</returns>
    public long DeletePagesAndGetSize(string pdfPath, int[] pagesToDelete)
    {
        if (string.IsNullOrWhiteSpace(pdfPath))
            throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

        if (!File.Exists(pdfPath))
            throw new FileNotFoundException("Input PDF not found.", pdfPath);

        if (pagesToDelete == null || pagesToDelete.Length == 0)
            throw new ArgumentException("At least one page number must be specified.", nameof(pagesToDelete));

        // Resolve directory and file name safely – Path methods can return null.
        string directory = Path.GetDirectoryName(pdfPath)
            ?? throw new InvalidOperationException("Unable to determine the directory of the PDF file.");
        string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath)
            ?? throw new InvalidOperationException("Unable to determine the file name of the PDF file.");

        // Build output file name (same folder, with suffix "_deleted.pdf").
        string outputPath = Path.Combine(directory, $"{fileNameWithoutExt}_deleted.pdf");

        // Use PdfFileEditor to delete pages.
        PdfFileEditor editor = new PdfFileEditor();
        bool success = editor.Delete(pdfPath, pagesToDelete, outputPath);

        if (!success)
            throw new InvalidOperationException("Failed to delete pages from the PDF.");

        // Return the size of the resulting file.
        return new FileInfo(outputPath).Length;
    }
}

public static class Program
{
    public static void Main(string[] args)
    {
        // Placeholder entry point to satisfy the compiler.
        // Real usage would call PdfUtility.DeletePagesAndGetSize from another component.
        Console.WriteLine("PdfUtility library loaded.");
    }
}