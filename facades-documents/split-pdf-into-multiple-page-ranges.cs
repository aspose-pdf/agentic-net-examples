using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "SplitBulks";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
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

        // PdfFileEditor does not implement IDisposable; instantiate directly
        PdfFileEditor editor = new PdfFileEditor();

        // Split the PDF into the specified bulks; each MemoryStream holds a PDF document
        MemoryStream[] bulks = editor.SplitToBulks(inputPdf, ranges);

        // Save each bulk to a separate PDF file
        for (int i = 0; i < bulks.Length; i++)
        {
            // Reset stream position before reading
            bulks[i].Position = 0;

            string outPath = Path.Combine(outputFolder, $"part{i + 1}.pdf");
            using (FileStream file = new FileStream(outPath, FileMode.Create, FileAccess.Write))
            {
                bulks[i].CopyTo(file);
            }

            // Dispose the MemoryStream after saving
            bulks[i].Dispose();

            Console.WriteLine($"Saved bulk {i + 1} to '{outPath}'.");
        }
    }
}