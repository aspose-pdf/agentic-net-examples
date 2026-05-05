using System;
using System.IO;
using Aspose.Pdf.Facades;

public static class PdfUtility
{
    /// <summary>
    /// Deletes the specified pages from a PDF file using Aspose.Pdf.Facades and returns the size (in bytes) of the resulting file.
    /// </summary>
    /// <param name="pdfPath">Full path to the source PDF file.</param>
    /// <param name="pagesToDelete">Array of page numbers to delete (1‑based indexing).</param>
    /// <returns>File size of the PDF after deletion, in bytes.</returns>
    public static long DeletePagesAndGetSize(string pdfPath, int[] pagesToDelete)
    {
        if (string.IsNullOrWhiteSpace(pdfPath))
            throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

        if (!File.Exists(pdfPath))
            throw new FileNotFoundException("Input PDF not found.", pdfPath);

        if (pagesToDelete == null || pagesToDelete.Length == 0)
            throw new ArgumentException("At least one page number must be specified.", nameof(pagesToDelete));

        // Create a temporary output file path in the same directory
        string outputPath = Path.Combine(
            Path.GetDirectoryName(pdfPath) ?? string.Empty,
            Path.GetFileNameWithoutExtension(pdfPath) + "_deleted.pdf");

        // Use PdfFileEditor to delete pages; this method saves the result to outputPath
        PdfFileEditor editor = new PdfFileEditor();
        bool success = editor.Delete(pdfPath, pagesToDelete, outputPath);

        if (!success)
            throw new InvalidOperationException("Failed to delete pages from the PDF.");

        // Return the size of the resulting file
        FileInfo info = new FileInfo(outputPath);
        return info.Length;
    }
}

// A minimal entry point is required for a console‑type project.
public class Program
{
    public static void Main(string[] args)
    {
        // Optional demonstration (commented out to keep the method side‑effect free).
        // if (args.Length >= 2)
        // {
        //     string pdfPath = args[0];
        //     int[] pages = args[1].Split(',').Select(int.Parse).ToArray();
        //     long size = PdfUtility.DeletePagesAndGetSize(pdfPath, pages);
        //     Console.WriteLine($"Resulting file size: {size} bytes");
        // }
    }
}