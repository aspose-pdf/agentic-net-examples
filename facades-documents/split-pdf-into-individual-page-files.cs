using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "SplitPages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Split the PDF into individual page streams
        PdfFileEditor editor = new PdfFileEditor();
        MemoryStream[] pageStreams = editor.SplitToPages(inputPath);

        // Write each stream to a uniquely named PDF file
        for (int i = 0; i < pageStreams.Length; i++)
        {
            MemoryStream ms = pageStreams[i];
            ms.Position = 0; // Reset position before copying

            string outPath = Path.Combine(outputDir, $"page_{i + 1}.pdf");
            using (FileStream file = new FileStream(outPath, FileMode.Create, FileAccess.Write))
            {
                ms.CopyTo(file);
            }

            ms.Dispose(); // Release the memory stream
            Console.WriteLine($"Saved page {i + 1} → {outPath}");
        }
    }
}