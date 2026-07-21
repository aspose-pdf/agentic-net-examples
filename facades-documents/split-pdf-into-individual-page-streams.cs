using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "SplitPages";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Open the source PDF as a read‑only stream
        using (FileStream sourceStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        {
            // PdfFileEditor provides the SplitToPages method that returns an array of MemoryStream,
            // each containing a single‑page PDF document.
            PdfFileEditor editor = new PdfFileEditor();
            MemoryStream[] pageStreams = editor.SplitToPages(sourceStream);

            // Iterate over the returned streams and write each page to a separate file.
            for (int i = 0; i < pageStreams.Length; i++)
            {
                // Reset the position to the beginning before copying.
                pageStreams[i].Position = 0;

                string outPath = Path.Combine(outputDir, $"page_{i + 1}.pdf");
                using (FileStream outFile = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                {
                    pageStreams[i].CopyTo(outFile);
                }

                // Dispose the individual page stream after it has been saved.
                pageStreams[i].Dispose();

                Console.WriteLine($"Saved page {i + 1} to {outPath}");
            }
        }
    }
}