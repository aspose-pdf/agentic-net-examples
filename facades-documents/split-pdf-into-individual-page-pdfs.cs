using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "SplitPages";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Create output directory if it does not exist
        Directory.CreateDirectory(outputFolder);

        // Open the source PDF as a read‑only stream
        using (FileStream inputStream = new FileStream(inputPdfPath, FileMode.Open, FileAccess.Read))
        {
            // PdfFileEditor does NOT implement IDisposable, so no using block is needed
            PdfFileEditor editor = new PdfFileEditor();

            // Split the PDF into individual page streams
            MemoryStream[] pageStreams = editor.SplitToPages(inputStream);

            // Write each page stream to a separate PDF file
            for (int i = 0; i < pageStreams.Length; i++)
            {
                // Ensure the stream position is at the beginning before copying
                pageStreams[i].Position = 0;

                string outputPath = Path.Combine(outputFolder, $"page_{i + 1}.pdf");

                // Write the memory stream to a file stream
                using (FileStream outStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    pageStreams[i].CopyTo(outStream);
                }

                // Dispose the memory stream after it has been saved
                pageStreams[i].Dispose();

                Console.WriteLine($"Saved page {i + 1} to '{outputPath}'");
            }
        }

        Console.WriteLine("PDF split operation completed.");
    }
}