using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files to be concatenated
        string[] inputFiles = { "file1.pdf", "file2.pdf", "file3.pdf" };
        // Path for the intermediate concatenated PDF
        string combinedPath = "combined.pdf";
        // Path for the final PDF with page numbers
        string outputPath = "final.pdf";

        // Verify that all input files exist
        foreach (string file in inputFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Input file not found: {file}");
                return;
            }
        }

        // ---------- Concatenate PDFs ----------
        // PdfFileEditor does NOT implement IDisposable; do NOT wrap in using
        PdfFileEditor editor = new PdfFileEditor();
        try
        {
            // Concatenate all input files into a single PDF
            bool concatResult = editor.Concatenate(inputFiles, combinedPath);
            if (!concatResult)
            {
                Console.Error.WriteLine("Concatenation failed.");
                return;
            }
        }
        finally
        {
            // No resources to dispose for PdfFileEditor, but ensure any streams are closed if used
            // (none were opened explicitly here)
        }

        // ---------- Add page numbers ----------
        // PdfFileStamp implements SaveableFacade and can be used with using for deterministic disposal
        using (PdfFileStamp stamp = new PdfFileStamp(combinedPath, outputPath))
        {
            // Optional: set starting page number (default is 1)
            // stamp.StartingNumber = 1;

            // Add page numbers; "#" will be replaced by the actual page number
            stamp.AddPageNumber("Page #");

            // Close finalizes the output file
            stamp.Close();
        }

        // Clean up intermediate file if desired
        try
        {
            if (File.Exists(combinedPath))
                File.Delete(combinedPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Could not delete intermediate file: {ex.Message}");
        }

        Console.WriteLine($"PDFs concatenated and numbered successfully: {outputPath}");
    }
}