using System;
using System.IO;
using Aspose.Pdf.Facades;

public static class PdfProcessor
{
    /// <summary>
    /// Deletes specified pages from a PDF stream, then creates a 2‑up layout (2 pages per sheet)
    /// and writes the result to the output stream.
    /// </summary>
    /// <param name="inputPdf">Stream containing the source PDF (must be readable).</param>
    /// <param name="pagesToDelete">
    /// Zero‑based page numbers to remove (e.g., new int[] { 2, 3 } removes pages 2 and 3).
    /// The PdfFileEditor API expects 1‑based page numbers, so the caller should provide 1‑based values.
    /// </param>
    /// <param name="outputPdf">Stream that will receive the final 2‑up PDF (must be writable).</param>
    public static void DeletePagesAndCreateTwoUp(Stream inputPdf, int[] pagesToDelete, Stream outputPdf)
    {
        // First step: delete the unwanted pages.
        // PdfFileEditor.Delete works with streams and returns true on success.
        // It writes the intermediate result to a temporary memory stream.
        using (MemoryStream intermediate = new MemoryStream())
        {
            PdfFileEditor editor = new PdfFileEditor();

            // Delete the pages; the method returns a bool indicating success.
            bool deleteSuccess = editor.Delete(inputPdf, pagesToDelete, intermediate);
            if (!deleteSuccess)
                throw new InvalidOperationException("Failed to delete pages from the PDF.");

            // Reset the position of the intermediate stream before the next operation.
            intermediate.Position = 0;

            // Second step: create a 2‑up layout.
            // x = 2 columns, y = 1 row (2 pages per output page, placed side‑by‑side).
            bool nupSuccess = editor.MakeNUp(intermediate, outputPdf, 2, 1);
            if (!nupSuccess)
                throw new InvalidOperationException("Failed to create a 2‑up layout.");
        }
    }
}

// ---------------------------------------------------------------------------
// Entry point required for a console‑type project.
// ---------------------------------------------------------------------------
public class Program
{
    public static void Main(string[] args)
    {
        // The Main method is only required to satisfy the compiler when the
        // project is built as an executable.  Real usage should call the
        // PdfProcessor.DeletePagesAndCreateTwoUp method from your own code.
        //
        // Example (uncomment to test locally):
        // if (args.Length == 2)
        // {
        //     using (FileStream input = File.OpenRead(args[0]))
        //     using (FileStream output = File.Create(args[1]))
        //     {
        //         int[] pagesToRemove = new int[] { 2, 3 }; // 1‑based page numbers
        //         PdfProcessor.DeletePagesAndCreateTwoUp(input, pagesToRemove, output);
        //     }
        // }
    }
}