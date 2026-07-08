using System;
using System.IO;
using Aspose.Pdf.Facades;

class PdfSplitter
{
    // Splits a PDF file into individual pages and returns an array of MemoryStream,
    // each stream contains a single‑page PDF document.
    public static MemoryStream[] SplitPdfToPages(string inputPdfPath)
    {
        if (!File.Exists(inputPdfPath))
            throw new FileNotFoundException($"Input PDF not found: {inputPdfPath}");

        // Open the source PDF as a read‑only stream.
        using (FileStream inputStream = new FileStream(inputPdfPath, FileMode.Open, FileAccess.Read))
        {
            // PdfFileEditor does NOT implement IDisposable, so we instantiate it directly.
            PdfFileEditor editor = new PdfFileEditor();

            // SplitToPages returns an array of MemoryStream, each representing one page.
            MemoryStream[] pageStreams = editor.SplitToPages(inputStream);

            // The input stream is closed by the using block; the returned MemoryStreams remain open.
            return pageStreams;
        }
    }

    // Example usage.
    static void Main()
    {
        const string sourcePdf = "sample.pdf";

        try
        {
            MemoryStream[] pages = SplitPdfToPages(sourcePdf);
            Console.WriteLine($"PDF split into {pages.Length} pages.");

            // Optionally save each page to a separate file for verification.
            for (int i = 0; i < pages.Length; i++)
            {
                string outPath = $"page_{i + 1}.pdf";
                using (FileStream outFile = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                {
                    pages[i].WriteTo(outFile);
                }

                // Reset position if the stream will be reused later.
                pages[i].Position = 0;
                Console.WriteLine($"Saved page {i + 1} to {outPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}