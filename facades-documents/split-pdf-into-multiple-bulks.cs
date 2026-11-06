using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Define bulk page ranges.
        // Each inner array represents a range: {startPage, endPage}
        // Example: pages 1‑3 and pages 4‑5
        int[][] bulkRanges = new int[][]
        {
            new int[] { 1, 3 },
            new int[] { 4, 5 }
        };

        // Open the source PDF as a read‑only stream
        using (FileStream sourceStream = File.OpenRead(inputPath))
        {
            // PdfFileEditor does NOT implement IDisposable, so we instantiate it directly
            PdfFileEditor editor = new PdfFileEditor();

            // Split the PDF into bulks; each bulk is returned as a MemoryStream
            MemoryStream[] bulkStreams = editor.SplitToBulks(sourceStream, bulkRanges);

            // Save each bulk to a separate file (optional, for demonstration)
            for (int i = 0; i < bulkStreams.Length; i++)
            {
                string outputPath = $"bulk_{i + 1}.pdf";

                // Ensure the MemoryStream's position is at the beginning before writing
                bulkStreams[i].Position = 0;

                using (FileStream outFile = File.Create(outputPath))
                {
                    bulkStreams[i].CopyTo(outFile);
                }

                // Dispose the individual MemoryStream when done
                bulkStreams[i].Dispose();

                Console.WriteLine($"Saved bulk #{i + 1} to '{outputPath}'.");
            }
        }

        Console.WriteLine("PDF split into bulks completed successfully.");
    }
}