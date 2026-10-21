using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "edited.pdf";          // PDF that already contains annotations
        const string outputDir = "SplitPages";          // folder for the single‑page PDFs

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Split the source PDF into an array of MemoryStream, one per page.
            // PdfFileEditor does not have a Split method; use SplitToPages instead.
            PdfFileEditor editor = new PdfFileEditor();
            MemoryStream[] pageStreams = editor.SplitToPages(inputPath);

            for (int i = 0; i < pageStreams.Length; i++)
            {
                // Reset stream position before reading
                pageStreams[i].Position = 0;

                // Load the individual page stream into a Document.
                // This preserves any annotations that were on the page.
                using (Document pageDoc = new Document(pageStreams[i]))
                {
                    string outPath = Path.Combine(outputDir, $"Page_{i + 1}.pdf");
                    pageDoc.Save(outPath);
                    Console.WriteLine($"Saved page {i + 1} → {outPath}");
                }

                // Dispose the stream now that we're done with it
                pageStreams[i].Dispose();
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}