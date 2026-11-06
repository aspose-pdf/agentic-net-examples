using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "SplitBulks";

        // Verify the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Create the output directory if it does not exist
        Directory.CreateDirectory(outputDir);

        // Define page ranges for each bulk (start page, end page).
        // Example: pages 1‑3, 4‑6, 7‑end will be created as separate PDFs.
        int[][] pageRanges = new int[][]
        {
            new int[] { 1, 3 },   // first bulk: pages 1‑3
            new int[] { 4, 6 },   // second bulk: pages 4‑6
            new int[] { 7, -1 }   // third bulk: pages 7‑end (‑1 means till the last page)
        };

        // PdfFileEditor does not implement IDisposable, so no using block is required.
        PdfFileEditor editor = new PdfFileEditor();

        try
        {
            // Split the PDF into multiple MemoryStreams based on the specified page ranges.
            MemoryStream[] bulks = editor.SplitToBulks(inputPdf, pageRanges);

            // Save each bulk to a separate file in the output directory.
            for (int i = 0; i < bulks.Length; i++)
            {
                string outputPath = Path.Combine(outputDir, $"bulk_{i + 1}.pdf");
                using (FileStream file = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    bulks[i].WriteTo(file);
                }
                // Dispose the MemoryStream now that it has been written to disk.
                bulks[i].Dispose();
            }

            Console.WriteLine($"PDF successfully split into {bulks.Length} bulks in folder: {outputDir}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during split operation: {ex.Message}");
        }
    }
}
