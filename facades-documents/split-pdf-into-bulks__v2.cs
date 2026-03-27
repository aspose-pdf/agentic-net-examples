using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Define bulk page ranges (1‑based inclusive). Each inner array contains start and end page numbers.
        int[][] pageRanges = new int[][]
        {
            new int[] { 1, 3 },
            new int[] { 4, 5 }
        };

        using (FileStream inputStream = new FileStream(inputPdfPath, FileMode.Open, FileAccess.Read))
        {
            PdfFileEditor editor = new PdfFileEditor();
            MemoryStream[] bulks = editor.SplitToBulks(inputStream, pageRanges);

            for (int i = 0; i < bulks.Length; i++)
            {
                string outputPath = $"bulk_{i + 1}.pdf";
                using (FileStream outputFile = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    bulks[i].WriteTo(outputFile);
                }

                // Reset position if the stream will be used later.
                bulks[i].Position = 0;
                bulks[i].Dispose();
                Console.WriteLine($"Saved bulk document {i + 1} to '{outputPath}'.");
            }
        }
    }
}