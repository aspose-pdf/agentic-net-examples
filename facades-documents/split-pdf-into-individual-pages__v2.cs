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

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // PdfFileEditor does NOT implement IDisposable; do NOT wrap in using
            PdfFileEditor editor = new PdfFileEditor();

            // Split the PDF into individual page streams
            MemoryStream[] pageStreams = editor.SplitToPages(inputPdfPath);

            // Save each page stream to a separate PDF file
            for (int i = 0; i < pageStreams.Length; i++)
            {
                // Page numbers are 1‑based for naming
                string outputPath = Path.Combine(outputFolder, $"page{i + 1}.pdf");

                // Ensure the memory stream position is at the beginning before writing
                pageStreams[i].Position = 0;

                // Write the stream to file
                using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    pageStreams[i].CopyTo(fileStream);
                }

                // Dispose the individual memory stream
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