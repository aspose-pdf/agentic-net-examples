using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";               // source PDF file
        const string outputDir = "SplitPages";              // folder for individual pages

        // Verify source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Open the source PDF as a read‑only stream
        using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        {
            // PdfFileEditor provides the SplitToPages method for stream input
            PdfFileEditor editor = new PdfFileEditor();

            // Split the PDF; each element in the array is a MemoryStream containing one page
            MemoryStream[] pageStreams = editor.SplitToPages(inputStream);

            // Iterate over the resulting streams and write each to a separate file
            for (int i = 0; i < pageStreams.Length; i++)
            {
                // Build the output file name (pages are 1‑based)
                string outPath = Path.Combine(outputDir, $"Page_{i + 1}.pdf");

                // Reset stream position before copying
                pageStreams[i].Position = 0;

                // Write the page stream to disk
                using (FileStream outStream = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                {
                    pageStreams[i].CopyTo(outStream);
                }

                // Dispose the individual MemoryStream
                pageStreams[i].Dispose();
            }
        }

        Console.WriteLine("PDF split into individual pages successfully.");
    }
}