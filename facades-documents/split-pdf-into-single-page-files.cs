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

        // Split the PDF into single‑page streams using PdfFileEditor
        PdfFileEditor editor = new PdfFileEditor();
        MemoryStream[] pageStreams = editor.SplitToPages(inputPath); // returns an array of MemoryStream

        // Iterate over each stream and write it to a uniquely named PDF file
        for (int i = 0; i < pageStreams.Length; i++)
        {
            // Reset stream position before copying
            MemoryStream ms = pageStreams[i];
            ms.Position = 0;

            // Build a distinct file name for each page (1‑based index)
            string outPath = Path.Combine(outputDir, $"page_{i + 1}.pdf");

            // Write the stream content to disk and dispose the file handle
            using (FileStream file = new FileStream(outPath, FileMode.Create, FileAccess.Write))
            {
                ms.CopyTo(file);
            }

            // Dispose the memory stream after it has been saved
            ms.Dispose();
        }

        Console.WriteLine($"PDF split completed. Files saved to '{outputDir}'.");
    }
}