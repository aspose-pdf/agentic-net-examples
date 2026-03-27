using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Define page ranges (start and end inclusive, 1‑based)
        int[][] pageRanges = new int[][]
        {
            new int[] { 1, 2 },
            new int[] { 3, 5 },
            new int[] { 6, 6 }
        };

        PdfFileEditor editor = new PdfFileEditor();
        MemoryStream[] splitStreams = editor.SplitToBulks(inputPath, pageRanges);

        for (int i = 0; i < splitStreams.Length; i++)
        {
            string outputPath = $"output_part{i + 1}.pdf";
            using (MemoryStream ms = splitStreams[i])
            {
                ms.Position = 0;
                using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    ms.CopyTo(fs);
                }
            }
            Console.WriteLine($"Saved split PDF: {outputPath}");
        }
    }
}