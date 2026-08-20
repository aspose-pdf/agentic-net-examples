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

        // Define bulk page ranges (1‑based inclusive)
        int[][] bulkRanges = new int[][]
        {
            new int[] { 1, 3 }, // pages 1 to 3
            new int[] { 4, 5 }  // pages 4 to 5
        };

        // Open source PDF as a read‑only stream
        using (FileStream sourceStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        {
            // Split the PDF into the defined bulks; each bulk is returned as a MemoryStream
            PdfFileEditor editor = new PdfFileEditor();
            MemoryStream[] bulkStreams = editor.SplitToBulks(sourceStream, bulkRanges);

            // Save each bulk stream to a separate file
            for (int i = 0; i < bulkStreams.Length; i++)
            {
                // Reset position to the beginning before reading
                bulkStreams[i].Position = 0;

                string outPath = $"bulk_{i + 1}.pdf";
                using (FileStream outFile = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                {
                    bulkStreams[i].CopyTo(outFile);
                }

                // Dispose the memory stream after it has been written
                bulkStreams[i].Dispose();

                Console.WriteLine($"Bulk {i + 1} saved to '{outPath}'.");
            }
        }
    }
}