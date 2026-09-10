using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Directory where split PDFs will be saved
        const string outputDir = "SplitBulks";

        // Ensure the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Create output directory if it does not exist
        Directory.CreateDirectory(outputDir);

        // Define page ranges (start and end page, 1‑based indexing)
        // Example: split into three documents: pages 1‑3, 4‑6, and 7‑end
        int[][] pageRanges = new int[][]
        {
            new int[] { 1, 3 },
            new int[] { 4, 6 },
            new int[] { 7, 10 } // adjust the end page as needed
        };

        // PdfFileEditor does NOT implement IDisposable – do NOT wrap in using
        PdfFileEditor editor = new PdfFileEditor();

        // Split the PDF into the defined bulks; each result is a MemoryStream
        MemoryStream[] splitStreams = editor.SplitToBulks(inputPdf, pageRanges);

        // Save each MemoryStream to a separate PDF file
        for (int i = 0; i < splitStreams.Length; i++)
        {
            // Reset stream position before reading
            splitStreams[i].Position = 0;

            // Build output file name, e.g., "output_1.pdf", "output_2.pdf", ...
            string outputPath = Path.Combine(outputDir, $"output_{i + 1}.pdf");

            // Write the stream contents to the file
            using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                splitStreams[i].CopyTo(fileStream);
            }

            // Dispose the individual MemoryStream after saving
            splitStreams[i].Dispose();

            Console.WriteLine($"Saved split document: {outputPath}");
        }

        // No need to dispose PdfFileEditor (it has no IDisposable implementation)
    }
}