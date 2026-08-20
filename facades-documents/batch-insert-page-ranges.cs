using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string basePdfPath   = "base.pdf";      // Existing destination PDF
        const string resultPdfPath = "result.pdf";    // Final output PDF

        // Ensure the result file starts as a copy of the base PDF
        if (!File.Exists(basePdfPath))
        {
            Console.Error.WriteLine($"Base PDF not found: {basePdfPath}");
            return;
        }
        File.Copy(basePdfPath, resultPdfPath, true);

        // Define source PDFs and the page numbers to insert from each
        // Example data – replace with real file names and page arrays
        var sources = new (string filePath, int[] pages)[]
        {
            ("source1.pdf", new int[] { 2, 4, 5 }),   // insert pages 2,4,5 from source1.pdf
            ("source2.pdf", new int[] { 1, 3 }),      // insert pages 1,3 from source2.pdf
            ("source3.pdf", new int[] { 6 })          // insert page 6 from source3.pdf
        };

        // PdfFileEditor does NOT implement IDisposable – do NOT wrap in using
        PdfFileEditor editor = new PdfFileEditor();

        // Insert position is 1‑based. Start after the first page of the current result PDF.
        int insertPosition = 2; // insert after page 1

        foreach (var src in sources)
        {
            if (!File.Exists(src.filePath))
            {
                Console.Error.WriteLine($"Source PDF not found: {src.filePath}");
                continue;
            }

            // Temporary file to hold the intermediate result
            string tempPath = Path.GetTempFileName();

            // TryInsert returns false instead of throwing if the operation fails
            bool success = editor.TryInsert(
                resultPdfPath,          // current destination PDF
                insertPosition,         // where to insert pages (1‑based)
                src.filePath,           // source PDF
                src.pages,              // page numbers to insert
                tempPath);              // output PDF

            if (!success)
            {
                Console.Error.WriteLine($"Failed to insert pages from {src.filePath}");
                // Clean up temporary file and abort the batch
                File.Delete(tempPath);
                break;
            }

            // Replace the previous result with the new intermediate file
            File.Delete(resultPdfPath);
            File.Move(tempPath, resultPdfPath);

            // Update the insert position for the next iteration:
            // inserted pages occupy the range we just added
            insertPosition += src.pages.Length;
        }

        Console.WriteLine($"Batch insertion completed. Output saved to '{resultPdfPath}'.");
    }
}