using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "SplitPages";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Open the source PDF as a read‑only stream
        using (FileStream sourceStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        {
            // PdfFileEditor provides the SplitToPages method that returns an array of MemoryStream,
            // each containing a single‑page PDF document.
            PdfFileEditor editor = new PdfFileEditor();
            MemoryStream[] pageStreams = editor.SplitToPages(sourceStream);

            // Iterate over the returned streams and save each page to a separate file.
            for (int i = 0; i < pageStreams.Length; i++)
            {
                // Reset stream position before copying
                pageStreams[i].Position = 0;

                string outPath = Path.Combine(outputDir, $"Page_{i + 1}.pdf");
                using (FileStream outFile = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                {
                    pageStreams[i].CopyTo(outFile);
                }

                Console.WriteLine($"Saved page {i + 1} to {outPath}");

                // Dispose the individual MemoryStream to free resources
                pageStreams[i].Dispose();
            }
        }
    }
}