using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    // Holds information about a source PDF and the page range to insert.
    class SourceInfo
    {
        public string FilePath;      // Path to the source PDF.
        public int StartPage;        // First page of the range (1‑based).
        public int EndPage;          // Last page of the range (1‑based).
        public int InsertLocation;   // Position in the destination where pages will be inserted (1‑based).
    }

    static void Main()
    {
        // Destination PDF that will receive the inserted pages.
        const string destinationPdf = "base.pdf";

        // Final output PDF after all insertions.
        const string finalOutputPdf = "merged_result.pdf";

        // Define the source PDFs and the ranges to insert.
        var sources = new List<SourceInfo>
        {
            new SourceInfo { FilePath = "source1.pdf", StartPage = 2, EndPage = 5, InsertLocation = 1 },
            new SourceInfo { FilePath = "source2.pdf", StartPage = 1, EndPage = 3, InsertLocation = 4 },
            new SourceInfo { FilePath = "source3.pdf", StartPage = 7, EndPage = 9, InsertLocation = 10 }
        };

        // Validate that the destination file exists.
        if (!File.Exists(destinationPdf))
        {
            Console.Error.WriteLine($"Destination file not found: {destinationPdf}");
            return;
        }

        // The file that will be used as input for the next iteration.
        string currentInput = destinationPdf;

        // Temporary file used for the output of each insertion step.
        string tempOutput = Path.GetTempFileName();

        try
        {
            foreach (var src in sources)
            {
                // Ensure the source file exists.
                if (!File.Exists(src.FilePath))
                {
                    Console.Error.WriteLine($"Source file not found: {src.FilePath}");
                    break;
                }

                // Perform the insertion using PdfFileEditor.
                PdfFileEditor editor = new PdfFileEditor();
                bool success = editor.Insert(
                    currentInput,               // Input PDF (current state)
                    src.InsertLocation,        // Insert position (1‑based)
                    src.FilePath,              // PDF to take pages from
                    src.StartPage,             // Start page in source
                    src.EndPage,               // End page in source
                    tempOutput);               // Output PDF for this step

                if (!success)
                {
                    Console.Error.WriteLine($"Failed to insert pages from {src.FilePath}");
                    break;
                }

                // Delete the previous intermediate file if it was a temp file.
                if (currentInput != destinationPdf && File.Exists(currentInput))
                {
                    File.Delete(currentInput);
                }

                // Prepare for the next iteration.
                currentInput = tempOutput;
                tempOutput = Path.GetTempFileName();
            }

            // After processing all sources, move the final intermediate file to the desired output.
            if (File.Exists(currentInput))
            {
                // Overwrite if the final output already exists.
                if (File.Exists(finalOutputPdf))
                {
                    File.Delete(finalOutputPdf);
                }
                File.Move(currentInput, finalOutputPdf);
                Console.WriteLine($"All insertions completed. Result saved to '{finalOutputPdf}'.");
            }
        }
        finally
        {
            // Clean up any leftover temporary file.
            if (File.Exists(tempOutput))
            {
                File.Delete(tempOutput);
            }
        }
    }
}