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
        string concatenatedPath = "combined_temp.pdf";
        // Final output PDF with page numbers
        string outputPath = "combined_with_page_numbers.pdf";

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
        // PdfFileEditor does NOT implement IDisposable, so no using block is required.
        PdfFileEditor editor = new PdfFileEditor();

        // Concatenate the input files into a single PDF.
        // The Concatenate(string[], string) overload writes directly to a file.
        bool concatSuccess = editor.Concatenate(inputFiles, concatenatedPath);
        if (!concatSuccess)
        {
            Console.Error.WriteLine("Failed to concatenate PDF files.");
            return;
        }

        // ---------- Add page numbers ----------
        // PdfFileStamp implements SaveableFacade (IDisposable), so wrap it in a using block.
        // The constructor takes the source PDF and the destination PDF.
        using (PdfFileStamp stamp = new PdfFileStamp(concatenatedPath, outputPath))
        {
            // Optional: start numbering from 1 (default) or any other number.
            stamp.StartingNumber = 1;

            // Add page numbers. The format string may contain '#' which will be replaced
            // by the actual page number. Position constants are defined in PdfFileStamp.
            stamp.AddPageNumber("Page #", PdfFileStamp.PosBottomMiddle);
            // Close() finalizes the stamping operation and writes the output file.
            stamp.Close();
        }

        // Clean up the intermediate file if desired
        try
        {
            File.Delete(concatenatedPath);
        }
        catch
        {
            // Ignored – the file may be in use or deletion may fail; not critical.
        }

        Console.WriteLine($"PDFs concatenated and page numbers added: {outputPath}");
    }
}