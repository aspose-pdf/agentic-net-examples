using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Output directory where split PDFs will be saved
        const string outputDir = "BulkSplits";

        // Define start‑end page pairs (1‑based indexing)
        // Example: split into pages 1‑3, 4‑6, and 7‑10
        int[][] pageRanges = new int[][]
        {
            new int[] { 1, 3 },
            new int[] { 4, 6 },
            new int[] { 7, 10 }
        };

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Create PdfFileEditor instance (does NOT implement IDisposable)
        PdfFileEditor editor = new PdfFileEditor();

        // Split the PDF into the defined bulks; each bulk is returned as a MemoryStream
        // The SplitToBulks overload that accepts (string, int[][]) is the correct one.
        MemoryStream[] bulkStreams = editor.SplitToBulks(inputPdf, pageRanges);

        // Save each bulk stream to a separate file
        for (int i = 0; i < bulkStreams.Length; i++)
        {
            // Construct output file name, e.g., Bulk_1.pdf, Bulk_2.pdf, ...
            string outputPath = Path.Combine(outputDir, $"Bulk_{i + 1}.pdf");

            // Reset stream position before reading
            bulkStreams[i].Position = 0;

            // Write the stream content to the file
            using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                bulkStreams[i].CopyTo(fileStream);
            }

            // Dispose the memory stream after saving
            bulkStreams[i].Dispose();

            Console.WriteLine($"Saved bulk {i + 1} to '{outputPath}'.");
        }
    }
}
