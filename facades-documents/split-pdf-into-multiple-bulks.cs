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

        // Define bulk page ranges (1‑based inclusive start and end pages)
        int[][] bulks = new int[][]
        {
            new int[] { 1, 3 },   // pages 1‑3
            new int[] { 4, 5 },   // pages 4‑5
            new int[] { 6, 10 }   // pages 6‑10
        };

        // Open the source PDF as a stream
        using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        {
            // Split the PDF into the defined bulks; each bulk is returned as a MemoryStream
            MemoryStream[] bulkStreams = new PdfFileEditor().SplitToBulks(inputStream, bulks);

            // Example: save each bulk to a separate file
            for (int i = 0; i < bulkStreams.Length; i++)
            {
                string outPath = $"bulk_{i + 1}.pdf";

                // Reset stream position before writing
                bulkStreams[i].Position = 0;

                using (FileStream outFile = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                {
                    bulkStreams[i].CopyTo(outFile);
                }

                Console.WriteLine($"Saved bulk {i + 1} to '{outPath}'.");
                bulkStreams[i].Dispose();
            }
        }
    }
}