using System;
using System.IO;
using Aspose.Pdf.Facades;

public static class PdfProcessor
{
    /// <summary>
    /// Deletes specified pages from a PDF, resizes the remaining pages, and returns the result as a MemoryStream.
    /// </summary>
    /// <param name="inputPath">Full path to the source PDF file.</param>
    /// <param name="pagesToDelete">Array of 1‑based page numbers to remove.</param>
    /// <param name="newWidth">New width for page contents (default units, 1 unit = 1/72 inch).</param>
    /// <param name="newHeight">New height for page contents (default units, 1 unit = 1/72 inch).</param>
    /// <returns>A MemoryStream containing the processed PDF. Caller is responsible for disposing the stream.</returns>
    public static MemoryStream DeleteAndResizePdf(string inputPath, int[] pagesToDelete, double newWidth, double newHeight)
    {
        if (string.IsNullOrEmpty(inputPath))
            throw new ArgumentException("Input path must be provided.", nameof(inputPath));

        if (!File.Exists(inputPath))
            throw new FileNotFoundException("Input PDF not found.", inputPath);

        // Stream for the intermediate PDF after page deletion
        using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        using (MemoryStream afterDeleteStream = new MemoryStream())
        {
            // Delete the unwanted pages
            PdfFileEditor editor = new PdfFileEditor();
            editor.Delete(inputStream, pagesToDelete, afterDeleteStream);

            // Prepare the stream for the final resized PDF
            afterDeleteStream.Position = 0; // reset for reading
            MemoryStream resizedStream = new MemoryStream();

            // Resize contents of all remaining pages (pages parameter = null)
            editor.ResizeContents(afterDeleteStream, resizedStream, null, newWidth, newHeight);

            // Ensure the returned stream is positioned at the beginning
            resizedStream.Position = 0;
            return resizedStream;
        }
    }
}

// Minimal entry point to satisfy a console‑application project.
public class Program
{
    public static void Main(string[] args)
    {
        // No operation – the library method can be called from elsewhere.
        // This stub exists solely to provide a valid static Main entry point.
    }
}
