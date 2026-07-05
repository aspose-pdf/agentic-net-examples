using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string inputPdfPath = "input.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdfPath}");
            return;
        }

        // Define bulk page ranges (1‑based inclusive start and end pages)
        // Example: first bulk = pages 1‑3, second bulk = pages 4‑6
        int[][] bulkRanges = new int[][]
        {
            new int[] { 1, 3 },
            new int[] { 4, 6 }
        };

        // Open the source PDF as a read‑only stream
        using (FileStream inputStream = new FileStream(inputPdfPath, FileMode.Open, FileAccess.Read))
        {
            // Create the PdfFileEditor facade
            PdfFileEditor editor = new PdfFileEditor();

            // Split the PDF into the defined bulks; each bulk is returned as a MemoryStream
            MemoryStream[] bulkStreams = editor.SplitToBulks(inputStream, bulkRanges);

            // Example usage: save each bulk to a separate file
            for (int i = 0; i < bulkStreams.Length; i++)
            {
                // Ensure the stream position is at the beginning before copying
                bulkStreams[i].Position = 0;

                string outputPath = $"bulk_{i + 1}.pdf";
                using (FileStream outFile = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    bulkStreams[i].CopyTo(outFile);
                }

                Console.WriteLine($"Bulk {i + 1} saved to '{outputPath}'.");
            }
        }
    }
}