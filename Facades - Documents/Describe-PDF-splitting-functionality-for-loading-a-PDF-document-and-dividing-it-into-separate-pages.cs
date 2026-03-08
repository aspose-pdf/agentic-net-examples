using System;
using System.IO;
using Aspose.Pdf.Facades;

class PdfSplitter
{
    static void Main()
    {
        // Path to the source PDF file.
        const string inputPath = "input.pdf";

        // Verify that the source file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // 1. Split the PDF into separate files using a filename template.
        // The template must contain %NUM% which will be replaced by the page number.
        SplitToFiles(inputPath, "output_page%NUM%.pdf");

        // 2. Split the PDF into memory streams (each stream contains a single‑page PDF).
        // This approach is useful when further processing is required without writing to disk.
        SplitToStreams(inputPath);
    }

    // Splits the PDF into individual files using PdfFileEditor.SplitToPages(string, string).
    private static void SplitToFiles(string sourceFile, string fileNameTemplate)
    {
        // PdfFileEditor does not implement IDisposable, so a plain instance is sufficient.
        PdfFileEditor editor = new PdfFileEditor();

        // The method creates one file per page according to the template.
        // Example: "output_page%NUM%.pdf" -> output_page1.pdf, output_page2.pdf, ...
        editor.SplitToPages(sourceFile, fileNameTemplate);

        Console.WriteLine("PDF successfully split into separate files using the template.");
    }

    // Splits the PDF into an array of MemoryStream objects.
    private static void SplitToStreams(string sourceFile)
    {
        PdfFileEditor editor = new PdfFileEditor();

        // Returns an array where each element holds a single‑page PDF.
        MemoryStream[] pageStreams = editor.SplitToPages(sourceFile);

        // Optional: write each stream to a physical file.
        for (int i = 0; i < pageStreams.Length; i++)
        {
            // Reset the stream position before reading.
            pageStreams[i].Position = 0;

            string outPath = $"page_{i + 1}.pdf";
            using (FileStream fs = new FileStream(outPath, FileMode.Create, FileAccess.Write))
            {
                pageStreams[i].CopyTo(fs);
            }

            // Dispose the stream after it has been saved.
            pageStreams[i].Dispose();
        }

        Console.WriteLine("PDF successfully split into memory streams and saved as individual files.");
    }
}