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
            // Use PdfFileEditor to split the PDF into individual page streams
            PdfFileEditor editor = new PdfFileEditor();
            MemoryStream[] pageStreams = editor.SplitToPages(inputStream);

            // Write each page stream to a separate PDF file
            for (int i = 0; i < pageStreams.Length; i++)
            {
                // Reset position to the beginning before copying
                pageStreams[i].Position = 0;

                string outPath = Path.Combine(outputDir, $"Page_{i + 1}.pdf");
                using (FileStream outFile = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                {
                    pageStreams[i].CopyTo(outFile);
                }

                // Dispose the MemoryStream after it has been written
                pageStreams[i].Dispose();
            }
        }

        Console.WriteLine("PDF successfully split into individual page files.");
    }
}