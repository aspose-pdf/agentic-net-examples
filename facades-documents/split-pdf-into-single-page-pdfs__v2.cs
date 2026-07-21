using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file to be split
        const string inputPdf = "input.pdf";

        // Directory where the individual page PDFs will be written
        const string outputFolder = "SplitPages";

        // Verify the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // PdfFileEditor does NOT implement IDisposable – instantiate directly
        PdfFileEditor editor = new PdfFileEditor();

        // Split the document into single‑page streams
        MemoryStream[] pageStreams = editor.SplitToPages(inputPdf);

        // Iterate over the returned streams and write each to a uniquely named file
        for (int i = 0; i < pageStreams.Length; i++)
        {
            // Reset stream position before reading
            MemoryStream ms = pageStreams[i];
            ms.Position = 0;

            // Build a file name like "page_1.pdf", "page_2.pdf", …
            string outPath = Path.Combine(outputFolder, $"page_{i + 1}.pdf");

            // Write the stream content to the file
            using (FileStream fs = new FileStream(outPath, FileMode.Create, FileAccess.Write))
            {
                ms.CopyTo(fs);
            }

            // Dispose the individual MemoryStream now that it is no longer needed
            ms.Dispose();
        }

        Console.WriteLine($"PDF split into {pageStreams.Length} pages and saved to '{outputFolder}'.");
    }
}