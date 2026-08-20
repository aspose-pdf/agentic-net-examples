using System;
using System.IO;
using Aspose.Pdf.Facades;

public static class PdfProcessor
{
    /// <summary>
    /// Deletes the specified pages from the input PDF stream, then creates a 2‑up layout
    /// (two pages per sheet, side‑by‑side) and writes the result to the output stream.
    /// </summary>
    /// <param name="inputPdf">Stream containing the source PDF. Must be positioned at the start.</param>
    /// <param name="pagesToDelete">Array of 1‑based page numbers to remove from the source PDF.</param>
    /// <param name="outputPdf">Stream that will receive the final 2‑up PDF. Caller is responsible for disposing it.</param>
    public static void DeletePagesAndCreate2Up(Stream inputPdf, int[] pagesToDelete, Stream outputPdf)
    {
        // PdfFileEditor does NOT implement IDisposable; instantiate once and reuse.
        PdfFileEditor editor = new PdfFileEditor();

        // Intermediate memory stream holds the PDF after the deletion step.
        using (MemoryStream afterDelete = new MemoryStream())
        {
            // Delete the unwanted pages. The method writes the result into afterDelete.
            editor.Delete(inputPdf, pagesToDelete, afterDelete);

            // Reset the position so the next operation can read from the beginning.
            afterDelete.Position = 0;

            // Create a 2‑up layout: 2 columns, 1 row (pages placed horizontally).
            // The result is written directly to the caller‑provided output stream.
            editor.MakeNUp(afterDelete, outputPdf, 2, 1);
        }
    }
}

// Dummy entry point to satisfy the compiler when the project is built as an executable.
public class Program
{
    public static void Main(string[] args)
    {
        // Intentionally left blank. The library functionality is exposed via PdfProcessor.
    }
}
