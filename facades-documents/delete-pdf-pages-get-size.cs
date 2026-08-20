using System;
using System.IO;
using Aspose.Pdf.Facades;

public static class PdfUtilities
{
    /// <summary>
    /// Deletes the specified pages from a PDF file using Aspose.Pdf.Facades.PdfFileEditor
    /// and returns the size (in bytes) of the resulting PDF.
    /// </summary>
    /// <param name="pdfPath">Full path to the source PDF file.</param>
    /// <param name="pagesToDelete">Array of page numbers to delete (1‑based indexing).</param>
    /// <returns>File size of the PDF after deletion, in bytes.</returns>
    public static long DeletePagesAndGetSize(string pdfPath, int[] pagesToDelete)
    {
        if (string.IsNullOrEmpty(pdfPath))
            throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

        if (!File.Exists(pdfPath))
            throw new FileNotFoundException("Input PDF not found.", pdfPath);

        if (pagesToDelete == null || pagesToDelete.Length == 0)
            throw new ArgumentException("At least one page number must be specified.", nameof(pagesToDelete));

        // Create a temporary file for the output PDF
        string tempOutputPath = Path.Combine(Path.GetDirectoryName(pdfPath) ?? string.Empty,
                                            Guid.NewGuid().ToString("N") + ".pdf");

        // PdfFileEditor does NOT implement IDisposable, so we do NOT use a using block.
        PdfFileEditor editor = new PdfFileEditor();

        // Delete the specified pages and write the result to the temporary file.
        // This uses the Delete(string, int[], string) overload as required.
        bool success = editor.Delete(pdfPath, pagesToDelete, tempOutputPath);

        if (!success)
            throw new InvalidOperationException("Failed to delete pages from the PDF.");

        // Get the size of the resulting PDF file.
        long resultSize = new FileInfo(tempOutputPath).Length;

        // Overwrite the original PDF with the modified version.
        File.Copy(tempOutputPath, pdfPath, overwrite: true);
        File.Delete(tempOutputPath);

        return resultSize;
    }
}

// ---------------------------------------------------------------------------
// A minimal entry point is required for a console‑application project.
// It does not interfere with the library functionality; it simply allows the
// project to compile successfully.  Users can call PdfUtilities.DeletePagesAndGetSize
// from their own code or from this demo block.
// ---------------------------------------------------------------------------
public static class Program
{
    public static void Main(string[] args)
    {
        // Demo usage (optional).  Replace the paths and page numbers with real values
        // when testing the method.
        //
        // string pdfPath = "sample.pdf";
        // int[] pages = { 2, 4 };
        // long newSize = PdfUtilities.DeletePagesAndGetSize(pdfPath, pages);
        // Console.WriteLine($"New PDF size: {newSize} bytes");
        //
        // The Main method is intentionally left empty to satisfy the compiler.
    }
}
