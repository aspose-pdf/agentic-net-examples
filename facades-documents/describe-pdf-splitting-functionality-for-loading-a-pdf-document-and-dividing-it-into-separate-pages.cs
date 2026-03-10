using System;
using System.IO;
using Aspose.Pdf.Facades;

class PdfSplitter
{
    static void Main()
    {
        // Path to the source PDF file
        const string inputPdf = "input.pdf";

        // Directory where the split pages will be saved
        const string outputFolder = "SplitPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Create the PdfFileEditor facade (no IDisposable implementation)
            PdfFileEditor editor = new PdfFileEditor();

            // Split the PDF into single‑page documents.
            // The method returns an array of MemoryStream objects,
            // each containing a complete PDF for one page.
            MemoryStream[] pageStreams = editor.SplitToPages(inputPdf);

            // Save each page stream to a separate PDF file.
            for (int i = 0; i < pageStreams.Length; i++)
            {
                // Build the output file name (Page_1.pdf, Page_2.pdf, …)
                string outputPath = Path.Combine(outputFolder, $"Page_{i + 1}.pdf");

                // Reset the stream position before reading
                pageStreams[i].Position = 0;

                // Write the stream contents to the file
                using (FileStream file = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    pageStreams[i].CopyTo(file);
                }

                // Dispose the individual page stream
                pageStreams[i].Dispose();

                Console.WriteLine($"Saved page {i + 1} to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during splitting: {ex.Message}");
        }
    }
}