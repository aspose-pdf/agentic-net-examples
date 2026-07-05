using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF file
        const string outputDir = "SplitPages";         // folder for individual pages

        // Verify the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Create the PdfFileEditor facade
            PdfFileEditor editor = new PdfFileEditor();

            // Split the PDF into single‑page streams
            MemoryStream[] pageStreams = editor.SplitToPages(inputPdf);

            // Iterate over the returned streams and write each to a uniquely named file
            for (int i = 0; i < pageStreams.Length; i++)
            {
                // Build a file name like "page_1.pdf", "page_2.pdf", etc.
                string outPath = Path.Combine(outputDir, $"page_{i + 1}.pdf");

                // Reset the stream position before copying
                pageStreams[i].Position = 0;

                // Write the stream content to the file
                using (FileStream fileStream = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                {
                    pageStreams[i].CopyTo(fileStream);
                }

                // Dispose the individual memory stream
                pageStreams[i].Dispose();

                Console.WriteLine($"Saved page {i + 1} → {outPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during split: {ex.Message}");
        }
    }
}