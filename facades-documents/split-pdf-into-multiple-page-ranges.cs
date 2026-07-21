using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputFolder = "Bulks";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Define page ranges (start and end pages are 1‑based)
        int[][] ranges = new int[][]
        {
            new int[] { 1, 3 },   // pages 1‑3
            new int[] { 4, 5 },   // pages 4‑5
            new int[] { 6, 10 }   // pages 6‑10
        };

        // Split the PDF into the specified bulks
        PdfFileEditor editor = new PdfFileEditor();
        MemoryStream[] bulks = editor.SplitToBulks(inputPath, ranges);

        // Save each resulting MemoryStream to a separate PDF file
        for (int i = 0; i < bulks.Length; i++)
        {
            string outPath = Path.Combine(outputFolder, $"part_{i + 1}.pdf");
            using (MemoryStream ms = bulks[i])
            using (FileStream fs = new FileStream(outPath, FileMode.Create, FileAccess.Write))
            {
                ms.Position = 0; // reset stream position before copying
                ms.CopyTo(fs);
            }
            Console.WriteLine($"Saved bulk {i + 1} to '{outPath}'");
        }
    }
}