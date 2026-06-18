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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Open the source PDF as a read‑only stream
        using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        {
            // PdfFileEditor provides the SplitToPages method that returns an array of MemoryStream,
            // each containing a single‑page PDF.
            PdfFileEditor editor = new PdfFileEditor();
            MemoryStream[] pageStreams = editor.SplitToPages(inputStream);

            // Write each MemoryStream to a separate file
            for (int i = 0; i < pageStreams.Length; i++)
            {
                // Reset the stream position before copying
                pageStreams[i].Position = 0;

                string outPath = Path.Combine(outputDir, $"Page_{i + 1}.pdf");
                using (FileStream outFile = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                {
                    pageStreams[i].CopyTo(outFile);
                }

                // Dispose the individual MemoryStream after it has been saved
                pageStreams[i].Dispose();

                Console.WriteLine($"Saved page {i + 1} to {outPath}");
            }
        }
    }
}