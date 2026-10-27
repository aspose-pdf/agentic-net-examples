using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Output folder where each page PDF will be saved
        const string outputFolder = "SplitPages";

        // Verify the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // PdfFileEditor does NOT implement IDisposable – do NOT use a using block
            var editor = new PdfFileEditor();

            // Split the source PDF; each page is returned as a MemoryStream
            MemoryStream[] pageStreams = editor.SplitToPages(inputPdfPath);

            // Save each page stream to an individual PDF file
            for (int i = 0; i < pageStreams.Length; i++)
            {
                string outPath = Path.Combine(outputFolder, $"input_page_{i + 1}.pdf");
                // Reset stream position before copying
                pageStreams[i].Position = 0;
                using (FileStream file = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                {
                    pageStreams[i].CopyTo(file);
                }
            }

            Console.WriteLine($"PDF split completed. Pages saved to folder: {outputFolder}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during split operation: {ex.Message}");
        }
    }
}
