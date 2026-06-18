using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Define page ranges (start and end pages, 1‑based indexing)
        int[][] ranges = new int[][]
        {
            new int[] { 1, 3 },   // pages 1‑3
            new int[] { 4, 5 },   // pages 4‑5
            new int[] { 6, 10 }   // pages 6‑10
        };

        // Verify that the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Split the PDF into the defined bulks
        PdfFileEditor editor = new PdfFileEditor();
        MemoryStream[] bulks = editor.SplitToBulks(inputPdf, ranges);

        // Save each resulting MemoryStream to a separate PDF file
        for (int i = 0; i < bulks.Length; i++)
        {
            string outputPath = $"output_part_{i + 1}.pdf";

            // Ensure the stream is positioned at the beginning before copying
            using (MemoryStream ms = bulks[i])
            using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                ms.Position = 0;
                ms.CopyTo(fs);
            }

            Console.WriteLine($"Saved split part {i + 1} to '{outputPath}'.");
        }
    }
}