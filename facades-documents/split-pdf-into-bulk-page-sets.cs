using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    // Splits the input PDF stream into multiple bulk documents.
    // Each bulk is defined by a start and end page (1‑based indexing).
    // Returns an array of MemoryStream objects, each containing a PDF document.
    static MemoryStream[] SplitPdfToBulks(Stream inputPdfStream, int[][] pageRanges)
    {
        // PdfFileEditor does NOT implement IDisposable, so no using block is required.
        PdfFileEditor editor = new PdfFileEditor();

        // The SplitToBulks method returns an array of MemoryStream objects.
        // The input and output streams remain open after the call (per API remarks).
        MemoryStream[] result = editor.SplitToBulks(inputPdfStream, pageRanges);

        return result;
    }

    static void Main()
    {
        // Example: split a PDF into two bulks – pages 1‑3 and pages 4‑6.
        const string inputPath = "sample.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Define page ranges (start, end) for each bulk.
        int[][] ranges = new int[][]
        {
            new int[] { 1, 3 }, // first bulk: pages 1 to 3
            new int[] { 4, 6 }  // second bulk: pages 4 to 6
        };

        // Open the source PDF as a read‑only stream.
        using (FileStream sourceStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        {
            MemoryStream[] bulks = SplitPdfToBulks(sourceStream, ranges);

            // Save each resulting bulk to a separate file for demonstration.
            for (int i = 0; i < bulks.Length; i++)
            {
                string outPath = $"bulk_{i + 1}.pdf";
                // Ensure the MemoryStream position is at the beginning before saving.
                bulks[i].Position = 0;
                using (FileStream outFile = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                {
                    bulks[i].CopyTo(outFile);
                }
                Console.WriteLine($"Saved bulk #{i + 1} to '{outPath}'.");
                // Dispose the MemoryStream when done.
                bulks[i].Dispose();
            }
        }
    }
}