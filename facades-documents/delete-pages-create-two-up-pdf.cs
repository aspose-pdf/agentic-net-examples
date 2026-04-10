using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class PdfProcessor
{
    /// <summary>
    /// Deletes specified pages from the input PDF stream, then creates a 2‑up layout
    /// (two pages per sheet) and writes the result to the output stream.
    /// </summary>
    /// <param name="inputPdf">Readable PDF stream.</param>
    /// <param name="pagesToDelete">Array of 1‑based page numbers to remove.</param>
    /// <param name="outputPdf">Writable stream that will receive the final PDF.</param>
    public static void DeletePagesAndCreateTwoUp(Stream inputPdf, int[] pagesToDelete, Stream outputPdf)
    {
        // First step: delete the unwanted pages.
        // The result is written to an intermediate memory stream.
        using (MemoryStream afterDelete = new MemoryStream())
        {
            PdfFileEditor editor = new PdfFileEditor();
            editor.Delete(inputPdf, pagesToDelete, afterDelete);

            // Reset the position so the next operation can read from the beginning.
            afterDelete.Position = 0;

            // Second step: create a 2‑up layout (2 columns, 1 row) from the
            // stream that now contains the trimmed PDF.
            PdfFileEditor nupEditor = new PdfFileEditor();
            // x = 2 columns, y = 1 row (horizontal arrangement).
            nupEditor.MakeNUp(afterDelete, outputPdf, 2, 1);
        }
    }

    // Example usage.
    static void Main()
    {
        const string inputPath = "source.pdf";
        const string outputPath = "result.pdf";

        // Ensure a source PDF exists – if not, create a simple one on‑the‑fly.
        if (!File.Exists(inputPath))
        {
            CreateSamplePdf(inputPath);
        }

        // Pages 2 and 3 will be removed as an example.
        int[] pagesToRemove = new int[] { 2, 3 };

        using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            DeletePagesAndCreateTwoUp(inputStream, pagesToRemove, outputStream);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }

    /// <summary>
    /// Creates a minimal PDF file with three pages so that the delete‑and‑2‑up demo works
    /// even when the expected input file is missing.
    /// </summary>
    private static void CreateSamplePdf(string path)
    {
        // Create a new document with three blank pages.
        using (Document doc = new Document())
        {
            doc.Pages.Add(); // page 1
            doc.Pages.Add(); // page 2
            doc.Pages.Add(); // page 3
            doc.Save(path, SaveFormat.Pdf);
        }
    }
}
