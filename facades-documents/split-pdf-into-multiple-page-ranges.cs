using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Ensure the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Define the page ranges for each output document.
        // Each inner array contains two integers: start page and end page (1‑based indexing).
        // Example: split into three documents – pages 1‑3, pages 4‑6, and pages 7‑10.
        int[][] pageRanges = new int[][]
        {
            new int[] { 1, 3 },
            new int[] { 4, 6 },
            new int[] { 7, 10 }
        };

        // Create an instance of PdfFileEditor (it does NOT implement IDisposable).
        PdfFileEditor editor = new PdfFileEditor();

        // Split the source PDF into the defined bulks.
        // The method returns an array of MemoryStream, each containing a PDF document.
        MemoryStream[] bulks = editor.SplitToBulks(inputPdf, pageRanges);

        // Output directory for the split PDFs
        string outputDir = "SplitBulks";
        Directory.CreateDirectory(outputDir);

        // Save each MemoryStream to a separate file.
        for (int i = 0; i < bulks.Length; i++)
        {
            // Construct a file name like "part1.pdf", "part2.pdf", etc.
            string outputPath = Path.Combine(outputDir, $"part{i + 1}.pdf");

            // Ensure the stream position is at the beginning before writing.
            bulks[i].Position = 0;

            // Write the stream contents to the file.
            using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                bulks[i].CopyTo(fileStream);
            }

            // Dispose the individual MemoryStream after saving.
            bulks[i].Dispose();

            Console.WriteLine($"Saved split part {i + 1} to '{outputPath}'.");
        }
    }
}