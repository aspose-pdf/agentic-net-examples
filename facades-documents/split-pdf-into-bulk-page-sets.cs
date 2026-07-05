using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Define bulk page sets (start page, end page). Page numbers are 1‑based.
        int[][] bulkRanges = new int[][]
        {
            new int[] { 1, 3 },   // Bulk 1: pages 1‑3
            new int[] { 4, 5 },   // Bulk 2: pages 4‑5
            new int[] { 6, 10 }   // Bulk 3: pages 6‑10 (example)
        };

        // Open the source PDF as a read‑only stream.
        using (FileStream srcStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        {
            // PdfFileEditor does not implement IDisposable, so no using block is needed.
            PdfFileEditor editor = new PdfFileEditor();

            // Split the PDF into the defined bulks.
            // Each element of the returned array is a MemoryStream containing a PDF document.
            MemoryStream[] bulkStreams = editor.SplitToBulks(srcStream, bulkRanges);

            // OPTIONAL: write each bulk to a separate file for verification.
            for (int i = 0; i < bulkStreams.Length; i++)
            {
                string outPath = $"bulk_{i + 1}.pdf";
                using (FileStream outFile = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                {
                    bulkStreams[i].WriteTo(outFile);
                }

                // Reset the stream position if further processing is required.
                bulkStreams[i].Position = 0;

                Console.WriteLine($"Bulk {i + 1} saved to {outPath}");
            }
        }
    }
}