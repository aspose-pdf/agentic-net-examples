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
        using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        {
            // PdfFileEditor does NOT implement IDisposable, so no using block
            PdfFileEditor editor = new PdfFileEditor();

            // Split the PDF into individual pages; each page is returned as a MemoryStream
            MemoryStream[] pageStreams = editor.SplitToPages(inputStream);

            // Write each page stream to a separate PDF file
            for (int i = 0; i < pageStreams.Length; i++)
            {
                // Reset stream position before reading
                pageStreams[i].Position = 0;

                string outPath = Path.Combine(outputDir, $"Page_{i + 1}.pdf");

                // Write the MemoryStream to a file stream
                using (FileStream outStream = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                {
                    pageStreams[i].CopyTo(outStream);
                }

                // Dispose the MemoryStream after use
                pageStreams[i].Dispose();
            }
        }

        Console.WriteLine("PDF successfully split into individual page files.");
    }
}