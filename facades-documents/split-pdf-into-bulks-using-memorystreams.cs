using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Directory where the split bulk PDFs will be saved
        const string outputDir = "Bulks";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Define bulk page ranges (1‑based inclusive start and end pages)
        // Example: first bulk pages 1‑3, second bulk pages 4‑5, third bulk pages 6‑end
        int[][] bulkRanges = new int[][]
        {
            new int[] { 1, 3 },
            new int[] { 4, 5 },
            new int[] { 6, 0 } // 0 will be interpreted as the last page by the API
        };

        // Open the source PDF as a stream
        using (FileStream inputStream = new FileStream(inputPdfPath, FileMode.Open, FileAccess.Read))
        {
            // Create the PdfFileEditor facade
            PdfFileEditor editor = new PdfFileEditor();

            // Split the PDF into the defined bulks; each bulk is returned as a MemoryStream
            MemoryStream[] bulkStreams = editor.SplitToBulks(inputStream, bulkRanges);

            // Save each bulk stream to a separate PDF file
            for (int i = 0; i < bulkStreams.Length; i++)
            {
                // Reset the position to the beginning before writing
                bulkStreams[i].Position = 0;

                string bulkPath = Path.Combine(outputDir, $"bulk_{i + 1}.pdf");
                using (FileStream outFile = new FileStream(bulkPath, FileMode.Create, FileAccess.Write))
                {
                    bulkStreams[i].CopyTo(outFile);
                }

                // Dispose the MemoryStream after it has been written
                bulkStreams[i].Dispose();

                Console.WriteLine($"Bulk {i + 1} saved to: {bulkPath}");
            }
        }

        Console.WriteLine("Splitting into bulks completed.");
    }
}