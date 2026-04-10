using System;
using System.IO;
using Aspose.Pdf.Facades;

public static class PdfUtilities
{
    /// <summary>
    /// Deletes the specified pages from a PDF file using Aspose.Pdf.Facades.PdfFileEditor
    /// and returns the size (in bytes) of the resulting PDF.
    /// </summary>
    /// <param name="inputPdfPath">Full path to the source PDF.</param>
    /// <param name="pagesToDelete">Array of page numbers to delete (1‑based indexing).</param>
    /// <returns>Size of the PDF after deletion, in bytes.</returns>
    public static long DeletePagesAndGetSize(string inputPdfPath, int[] pagesToDelete)
    {
        // Validate input file existence
        if (string.IsNullOrWhiteSpace(inputPdfPath))
            throw new ArgumentException("Input PDF path must be provided.", nameof(inputPdfPath));

        if (!File.Exists(inputPdfPath))
            throw new FileNotFoundException("Input PDF file not found.", inputPdfPath);

        if (pagesToDelete == null || pagesToDelete.Length == 0)
            throw new ArgumentException("At least one page number must be specified for deletion.", nameof(pagesToDelete));

        // Prepare a temporary output file path
        string directory = Path.GetDirectoryName(inputPdfPath) ?? string.Empty;
        string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPdfPath);
        string tempOutputPath = Path.Combine(directory, $"{fileNameWithoutExt}_deleted.pdf");

        // Use PdfFileEditor (does NOT implement IDisposable) to delete pages
        PdfFileEditor editor = new PdfFileEditor();

        // Delete returns true on success; otherwise throw
        bool success = editor.Delete(inputPdfPath, pagesToDelete, tempOutputPath);
        if (!success)
            throw new InvalidOperationException("Failed to delete pages from the PDF.");

        // Get the size of the resulting file
        long resultSize = new FileInfo(tempOutputPath).Length;

        // Clean up the temporary file
        File.Delete(tempOutputPath);

        return resultSize;
    }
}

// Minimal entry point to satisfy the compiler when building an executable.
public static class Program
{
    public static void Main(string[] args)
    {
        // No operation – the library method can be called from other code or tests.
    }
}
