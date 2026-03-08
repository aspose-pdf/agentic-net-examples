using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf;   // Required for PageSize enum

class Program
{
    static void Main()
    {
        // Input PDF file (the source document)
        const string inputPath = "input.pdf";

        // Output PDF file (the N‑up result)
        const string outputPath = "nup_output.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the source and destination streams inside using blocks.
        // This guarantees that the file handles are released promptly.
        using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            // PdfFileEditor provides the N‑up functionality.
            // It does NOT implement IDisposable, so we do not wrap it in a using block.
            PdfFileEditor editor = new PdfFileEditor();

            // N‑up parameters:
            //   x (columns) – number of pages placed horizontally on each sheet.
            //   y (rows)    – number of pages placed vertically on each sheet.
            // The resulting sheet contains a grid of (x * y) original pages.
            // Pages are tiled left‑to‑right, then top‑to‑bottom.
            int columns = 2; // e.g., two pages side‑by‑side horizontally
            int rows    = 2; // e.g., two rows vertically

            // Optional: specify the size of the output sheet.
            // If omitted, the default page size of the source document is used.
            // Uncomment the following line to use A4 as the sheet size:
            // bool success = editor.MakeNUp(inputStream, outputStream, columns, rows, PageSize.A4);

            // Perform the N‑up operation using the overload without explicit page size.
            bool success = editor.MakeNUp(inputStream, outputStream, columns, rows);

            // Report the outcome.
            Console.WriteLine(success
                ? $"N‑up PDF created successfully: {outputPath}"
                : "N‑up operation failed.");
        }
    }
}