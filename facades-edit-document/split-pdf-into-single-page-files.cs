using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "edited.pdf";
        const string outputDir = "SplitPages";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create the output folder if it does not exist
        Directory.CreateDirectory(outputDir);

        try
        {
            // PdfFileEditor does NOT implement IDisposable, so instantiate it directly
            PdfFileEditor editor = new PdfFileEditor();

            // SplitToPages returns an array of MemoryStream, each containing a single page (with its annotations)
            MemoryStream[] pageStreams = editor.SplitToPages(inputPath);

            for (int i = 0; i < pageStreams.Length; i++)
            {
                string pagePath = Path.Combine(outputDir, $"output_{i + 1}.pdf");
                using (FileStream fs = new FileStream(pagePath, FileMode.Create, FileAccess.Write))
                {
                    pageStreams[i].WriteTo(fs);
                }
                // Dispose the individual MemoryStream after it has been written
                pageStreams[i].Dispose();
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during split: {ex.Message}");
        }

        Console.WriteLine($"PDF successfully split into individual pages with annotations in folder '{outputDir}'.");
    }
}
