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
            // PdfFileEditor does NOT implement IDisposable – do NOT wrap it in a using block
            PdfFileEditor editor = new PdfFileEditor();

            // Split the PDF into an array of MemoryStream, each containing a single page PDF
            MemoryStream[] pageStreams = editor.SplitToPages(sourceStream);

            // Write each page stream to a separate file
            for (int i = 0; i < pageStreams.Length; i++)
            {
                // Reset position to the beginning before reading
                pageStreams[i].Position = 0;

                string outputPath = Path.Combine(outputFolder, $"Page_{i + 1}.pdf");

                // Write the MemoryStream to a file using a FileStream
                using (FileStream outFile = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    pageStreams[i].CopyTo(outFile);
                }

                // Dispose the individual MemoryStream now that it's been saved
                pageStreams[i].Dispose();

                Console.WriteLine($"Saved page {i + 1} to '{outputPath}'");
            }
        }

        Console.WriteLine("All pages have been split and saved.");
    }
}