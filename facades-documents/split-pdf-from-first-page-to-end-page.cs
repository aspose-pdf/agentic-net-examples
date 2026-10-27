using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    // Splits the input PDF stream from page 1 to the specified endPage (inclusive)
    // and returns a new MemoryStream containing the resulting PDF.
    static MemoryStream SplitPdf(Stream inputPdfStream, int endPage)
    {
        if (inputPdfStream == null) throw new ArgumentNullException(nameof(inputPdfStream));
        if (endPage < 1) throw new ArgumentOutOfRangeException(nameof(endPage), "End page must be >= 1.");

        // PdfFileEditor does NOT implement IDisposable; instantiate directly.
        PdfFileEditor editor = new PdfFileEditor();

        // Output stream will receive the extracted pages.
        MemoryStream outputStream = new MemoryStream();

        // Use the stream‑based overload of SplitFromFirst to extract pages 1..endPage.
        // The method returns a bool indicating success; we ignore it here but could check.
        editor.SplitFromFirst(inputPdfStream, endPage, outputStream);

        // Reset position so the caller can read from the beginning.
        outputStream.Position = 0;
        return outputStream;
    }

    static void Main()
    {
        const string inputPath = "input.pdf";      // source PDF file
        const string outputPath = "split_output.pdf"; // destination for the split PDF
        const int endPage = 5;                     // split up to this page (inclusive)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the source PDF as a read‑only stream.
        using (FileStream sourceStream = File.OpenRead(inputPath))
        {
            // Perform the in‑memory split.
            using (MemoryStream splitStream = SplitPdf(sourceStream, endPage))
            {
                // Write the resulting PDF to a file (or further process the stream).
                using (FileStream destStream = File.Create(outputPath))
                {
                    splitStream.CopyTo(destStream);
                }
            }
        }

        Console.WriteLine($"PDF split completed. Pages 1‑{endPage} saved to '{outputPath}'.");
    }
}
