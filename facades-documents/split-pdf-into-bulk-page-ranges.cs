using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Directory where the split PDFs will be saved
        const string outputDir = "BulkSplits";

        // Define start‑end page pairs (1‑based indexing)
        // Example: split pages 1‑3, 4‑6, and 7‑10 into separate PDFs
        int[][] pageRanges = new int[][]
        {
            new int[] { 1, 3 },
            new int[] { 4, 6 },
            new int[] { 7, 10 }
        };

        // Validate input file existence
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Use PdfFileEditor to split the PDF into bulk page sets
        PdfFileEditor editor = new PdfFileEditor();
        MemoryStream[] bulkStreams = editor.SplitToBulks(inputPdf, pageRanges);

        // Save each resulting MemoryStream to a separate PDF file
        for (int i = 0; i < bulkStreams.Length; i++)
        {
            string outputPath = Path.Combine(outputDir, $"bulk_{i + 1}.pdf");

            // Reset stream position before copying
            bulkStreams[i].Position = 0;

            using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                bulkStreams[i].CopyTo(fileStream);
            }

            Console.WriteLine($"Saved bulk {i + 1} to '{outputPath}'.");
        }
    }
}