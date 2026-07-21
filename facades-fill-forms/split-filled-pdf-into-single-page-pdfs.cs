using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "filled.pdf";
        const string outputDir = "Pages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Split the PDF into single‑page streams
        PdfFileEditor editor = new PdfFileEditor();
        MemoryStream[] pageStreams = editor.SplitToPages(inputPath);

        // Save each stream as an individual PDF file
        for (int i = 0; i < pageStreams.Length; i++)
        {
            // Reset stream position before reading
            pageStreams[i].Position = 0;

            string outPath = Path.Combine(outputDir, $"page_{i + 1}.pdf");
            using (FileStream fs = new FileStream(outPath, FileMode.Create, FileAccess.Write))
            {
                pageStreams[i].CopyTo(fs);
            }

            // Release the memory stream
            pageStreams[i].Dispose();
        }

        Console.WriteLine($"Successfully split into {pageStreams.Length} single‑page PDFs in '{outputDir}'.");
    }
}