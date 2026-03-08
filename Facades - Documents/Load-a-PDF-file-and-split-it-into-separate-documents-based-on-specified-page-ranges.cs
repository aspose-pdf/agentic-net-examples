using System;
using System.IO;
using Aspose.Pdf.Facades;

class SplitPdfByRanges
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Directory where split documents will be saved
        const string outputDir = "SplitParts";

        // Define page ranges (start and end page, 1‑based indexing)
        // Example: split into three parts: pages 1‑3, 4‑6, and 7‑10
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

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Use PdfFileEditor (facade) to split the PDF into the specified bulks
        PdfFileEditor editor = new PdfFileEditor();

        // SplitToBulks returns an array of MemoryStream, each containing a PDF document
        MemoryStream[] splitStreams = editor.SplitToBulks(inputPdf, pageRanges);

        // Save each resulting stream to a separate file
        for (int i = 0; i < splitStreams.Length; i++)
        {
            // Reset stream position before reading
            splitStreams[i].Position = 0;

            string outPath = Path.Combine(outputDir, $"Part_{i + 1}.pdf");
            using (FileStream file = new FileStream(outPath, FileMode.Create, FileAccess.Write))
            {
                splitStreams[i].CopyTo(file);
            }

            // Dispose the memory stream after it has been written
            splitStreams[i].Dispose();

            Console.WriteLine($"Saved split part {i + 1} to '{outPath}'.");
        }
    }
}