using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "SplitPages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Open the source PDF as a read‑only stream
        using (FileStream sourceStream = new FileStream(inputPdfPath, FileMode.Open, FileAccess.Read))
        {
            // PdfFileEditor provides the SplitToPages method that works with streams
            PdfFileEditor editor = new PdfFileEditor();

            // Split the PDF into individual pages; each page is returned as a MemoryStream
            MemoryStream[] pageStreams = editor.SplitToPages(sourceStream);

            // Iterate over the resulting streams and save each page as a separate PDF file
            for (int i = 0; i < pageStreams.Length; i++)
            {
                // Reset the position to the beginning before reading
                pageStreams[i].Position = 0;

                string pagePath = Path.Combine(outputFolder, $"page_{i + 1}.pdf");
                using (FileStream outFile = new FileStream(pagePath, FileMode.Create, FileAccess.Write))
                {
                    pageStreams[i].CopyTo(outFile);
                }

                // Dispose the individual page stream after it has been saved
                pageStreams[i].Dispose();

                Console.WriteLine($"Saved page {i + 1} to '{pagePath}'.");
            }
        }

        Console.WriteLine("All pages have been split and saved.");
    }
}