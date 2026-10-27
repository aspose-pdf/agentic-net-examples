using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // Define bulk page counts (e.g., first bulk 2 pages, second bulk 3 pages, etc.)
        int[] bulkCounts = new int[] { 2, 3 };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Convert the page‑count array into an array of page‑range arrays required by SplitToBulks
        List<int[]> rangeList = new List<int[]>();
        int startPage = 1;
        foreach (int count in bulkCounts)
        {
            int endPage = startPage + count - 1;
            rangeList.Add(new int[] { startPage, endPage });
            startPage = endPage + 1;
        }
        int[][] bulkRanges = rangeList.ToArray();

        // Split the PDF into bulks; each bulk is returned as a MemoryStream
        PdfFileEditor editor = new PdfFileEditor();
        MemoryStream[] bulks = editor.SplitToBulks(inputPath, bulkRanges);

        // Save each bulk to a separate file and dispose the streams
        for (int i = 0; i < bulks.Length; i++)
        {
            string outPath = $"bulk_{i + 1}.pdf";

            // Ensure the MemoryStream is positioned at the beginning before copying
            bulks[i].Position = 0;

            using (FileStream outStream = File.Create(outPath))
            {
                bulks[i].CopyTo(outStream);
            }

            Console.WriteLine($"Saved bulk {i + 1} to {outPath}");

            // Release the MemoryStream resources
            bulks[i].Dispose();
        }
    }
}
