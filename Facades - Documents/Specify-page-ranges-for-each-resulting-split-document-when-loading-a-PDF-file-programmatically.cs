using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Define page ranges for each split document.
        // Each inner array contains the start page and end page (1‑based indexing).
        int[][] pageRanges = new int[][]
        {
            new int[] { 1, 3 },   // pages 1‑3
            new int[] { 4, 5 },   // pages 4‑5
            new int[] { 6, 10 }   // pages 6‑10
        };

        // Create the PdfFileEditor facade (does not implement IDisposable)
        PdfFileEditor editor = new PdfFileEditor();

        // Split the PDF into the specified bulks; each result is a MemoryStream.
        MemoryStream[] splitStreams = editor.SplitToBulks(inputPdf, pageRanges);

        // Save each resulting stream to a separate file.
        for (int i = 0; i < splitStreams.Length; i++)
        {
            // Ensure the stream is positioned at the beginning before copying.
            splitStreams[i].Position = 0;

            string outputPath = $"split_part_{i + 1}.pdf";

            // Write the memory stream to a file and dispose the stream afterwards.
            using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                splitStreams[i].CopyTo(fileStream);
            }

            // Optionally dispose the memory stream now that it's saved.
            splitStreams[i].Dispose();

            Console.WriteLine($"Saved split document #{i + 1} to '{outputPath}'.");
        }
    }
}