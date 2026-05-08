using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Open the source PDF as a read‑only stream
        using (FileStream sourceStream = new FileStream(inputPdfPath, FileMode.Open, FileAccess.Read))
        {
            // Create the PdfFileEditor facade (does not implement IDisposable)
            PdfFileEditor pdfEditor = new PdfFileEditor();

            // Split the PDF into individual pages; each page is returned as a MemoryStream
            MemoryStream[] pageStreams = pdfEditor.SplitToPages(sourceStream);

            // Example: write each page stream to a separate file (optional)
            for (int i = 0; i < pageStreams.Length; i++)
            {
                // Reset position to the beginning before reading
                pageStreams[i].Position = 0;

                string outputPath = $"page_{i + 1}.pdf";
                using (FileStream outFile = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    pageStreams[i].CopyTo(outFile);
                }

                Console.WriteLine($"Saved page {i + 1} to '{outputPath}'.");
                // Dispose the individual MemoryStream after use
                pageStreams[i].Dispose();
            }
        }
    }
}