using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Directory where split documents will be saved
        const string outputDir = "SplitRanges";
        Directory.CreateDirectory(outputDir);

        // Define page ranges for each resulting document.
        // Each inner array contains two integers: start page and end page (1‑based, inclusive).
        // Example: first document pages 1‑3, second document pages 4‑5, third document pages 6‑10.
        int[][] pageRanges = new int[][]
        {
            new int[] { 1, 3 },
            new int[] { 4, 5 },
            new int[] { 6, 10 }
        };

        // Ensure the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Use PdfFileEditor (does NOT implement IDisposable) to split the PDF into the defined ranges.
        PdfFileEditor editor = new PdfFileEditor();
        MemoryStream[] splitStreams = editor.SplitToBulks(inputPdf, pageRanges);

        // Save each resulting stream to a separate PDF file.
        for (int i = 0; i < splitStreams.Length; i++)
        {
            // Reset stream position before reading
            splitStreams[i].Position = 0;

            string outPath = Path.Combine(outputDir, $"Part_{i + 1}.pdf");
            using (FileStream file = new FileStream(outPath, FileMode.Create, FileAccess.Write))
            {
                splitStreams[i].CopyTo(file);
            }

            // Dispose the memory stream after saving
            splitStreams[i].Dispose();

            Console.WriteLine($"Saved split document {i + 1} to '{outPath}'");
        }
    }
}