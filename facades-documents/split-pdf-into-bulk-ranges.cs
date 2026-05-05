using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Define bulk page ranges (1‑based inclusive). 
        // Example: first bulk = pages 1‑3, second bulk = pages 4‑5, etc.
        int[][] bulkRanges = new int[][]
        {
            new int[] { 1, 3 },
            new int[] { 4, 5 }
            // Add more ranges as needed
        };

        // Open the source PDF as a read‑only stream
        using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        {
            // PdfFileEditor does NOT implement IDisposable, so we instantiate it directly
            PdfFileEditor editor = new PdfFileEditor();

            // Split the PDF into the defined bulks; each bulk is returned as a MemoryStream
            MemoryStream[] bulkStreams = editor.SplitToBulks(inputStream, bulkRanges);

            // Save each bulk stream to a separate file (optional, for demonstration)
            for (int i = 0; i < bulkStreams.Length; i++)
            {
                string outputPath = $"bulk_{i + 1}.pdf";

                // Ensure the MemoryStream position is at the beginning before writing
                bulkStreams[i].Position = 0;

                using (FileStream outFile = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    bulkStreams[i].CopyTo(outFile);
                }

                // Dispose the MemoryStream after it has been written
                bulkStreams[i].Dispose();

                Console.WriteLine($"Bulk {i + 1} saved to '{outputPath}'.");
            }
        }
    }
}