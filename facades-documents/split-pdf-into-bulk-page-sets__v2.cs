using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string sourcePdfPath = "input.pdf";

        // Verify that the source file exists
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdfPath}");
            return;
        }

        // Define bulk page sets.
        // Each inner array contains two integers: start page (inclusive) and end page (inclusive).
        // Page numbers are 1‑based as required by Aspose.Pdf.
        int[][] bulkPageRanges = new int[][]
        {
            new int[] { 1, 3 },   // First bulk: pages 1‑3
            new int[] { 4, 6 },   // Second bulk: pages 4‑6
            new int[] { 7, 10 }   // Third bulk: pages 7‑10
        };

        // Open the source PDF as a read‑only stream.
        // The stream is closed after the split operation (PdfFileEditor does NOT close it automatically).
        using (FileStream sourceStream = new FileStream(sourcePdfPath, FileMode.Open, FileAccess.Read))
        {
            // Create the PdfFileEditor facade.
            PdfFileEditor editor = new PdfFileEditor();

            // Split the PDF into the defined bulks.
            // The method returns an array of MemoryStream objects,
            // each containing a separate PDF document for the corresponding page range.
            MemoryStream[] bulkStreams = editor.SplitToBulks(sourceStream, bulkPageRanges);

            // Optional: write each bulk to a separate file for verification.
            for (int i = 0; i < bulkStreams.Length; i++)
            {
                // Reset the position of the MemoryStream before reading.
                bulkStreams[i].Position = 0;

                string outputPath = $"bulk_{i + 1}.pdf";
                using (FileStream outFile = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    bulkStreams[i].CopyTo(outFile);
                }

                Console.WriteLine($"Bulk {i + 1} saved to '{outputPath}'.");
            }

            // The returned MemoryStream objects can be used further as needed.
            // They are left open for the caller; dispose them when finished.
            foreach (var ms in bulkStreams)
            {
                ms.Dispose();
            }
        }
    }
}