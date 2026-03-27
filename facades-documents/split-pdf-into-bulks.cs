using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        string inputPath = "input.pdf";
        // Define bulk page ranges (start and end pages, 1‑based inclusive)
        int[][] pageRanges = new int[][]
        {
            new int[] { 1, 3 },
            new int[] { 4, 5 },
            new int[] { 6, 10 }
        };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfFileEditor does NOT implement IDisposable; do NOT wrap in using
        PdfFileEditor editor = new PdfFileEditor();
        MemoryStream[] resultStreams = editor.SplitToBulks(inputPath, pageRanges);

        for (int i = 0; i < resultStreams.Length; i++)
        {
            string outputFile = $"output_part{i + 1}.pdf";
            using (FileStream fileStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
            {
                resultStreams[i].WriteTo(fileStream);
            }
            Console.WriteLine($"Saved bulk {i + 1} to '{outputFile}'.");
        }
    }
}