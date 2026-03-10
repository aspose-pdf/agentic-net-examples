using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Define page ranges (start and end page, 1‑based indexing)
        // Example: split into two documents – pages 1‑3 and pages 4‑5
        int[][] pageRanges = new int[][]
        {
            new int[] { 1, 3 },
            new int[] { 4, 5 }
        };

        // Validate input file existence
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Create PdfFileEditor instance (no IDisposable)
            PdfFileEditor editor = new PdfFileEditor();

            // Split the PDF into separate streams according to the ranges
            MemoryStream[] splitStreams = editor.SplitToBulks(inputPdf, pageRanges);

            // Save each resulting stream to a separate PDF file
            for (int i = 0; i < splitStreams.Length; i++)
            {
                // Ensure the stream is positioned at the beginning
                splitStreams[i].Position = 0;

                // Build output file name (e.g., output_part1.pdf, output_part2.pdf)
                string outputPath = $"output_part{i + 1}.pdf";

                // Write the memory stream to disk
                using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    splitStreams[i].CopyTo(fileStream);
                }

                // Dispose the memory stream after saving
                splitStreams[i].Dispose();

                Console.WriteLine($"Saved split document: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during split operation: {ex.Message}");
        }
    }
}