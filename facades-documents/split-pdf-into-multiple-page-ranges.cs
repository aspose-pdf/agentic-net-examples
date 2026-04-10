using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string inputPath = "input.pdf";

        // Define the page ranges to split.
        // Each inner array contains the start page and end page (1‑based indexing).
        int[][] pageRanges = new int[][]
        {
            new int[] { 1, 3 },   // pages 1‑3
            new int[] { 4, 5 },   // pages 4‑5
            new int[] { 6, 10 }   // pages 6‑10
        };

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfFileEditor does NOT implement IDisposable, so do NOT wrap it in a using block
        PdfFileEditor editor = new PdfFileEditor();

        // Split the PDF into the defined bulks.
        // The method returns an array of MemoryStream objects, each containing a PDF document.
        MemoryStream[] splitStreams = editor.SplitToBulks(inputPath, pageRanges);

        // Save each resulting stream to a separate PDF file
        for (int i = 0; i < splitStreams.Length; i++)
        {
            string outputPath = $"output_part_{i + 1}.pdf";

            // Ensure the stream is positioned at the beginning before copying
            splitStreams[i].Position = 0;

            using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                splitStreams[i].CopyTo(fileStream);
            }

            Console.WriteLine($"Saved split part {i + 1} to '{outputPath}'.");
        }
    }
}