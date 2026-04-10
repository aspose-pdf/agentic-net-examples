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
        const string combinedPath = "combined.pdf";

        // Final output PDF with page numbers
        const string outputPath = "final_with_page_numbers.pdf";

        // Verify that all input files exist
        foreach (var file in inputFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Input file not found: {file}");
                return;
            }
        }

        // ---------- Concatenate PDFs ----------
        // PdfFileEditor implements concatenation without requiring a Document instance
        PdfFileEditor editor = new PdfFileEditor
        {
            // Preserve outlines and logical structure if present
            CopyOutlines = true,
            CopyLogicalStructure = true
        };

        // Concatenate the input files into a single PDF
        bool concatSuccess = editor.Concatenate(inputFiles, combinedPath);
        if (!concatSuccess)
        {
            Console.Error.WriteLine("Failed to concatenate PDF files.");
            return;
        }

        // ---------- Add page numbers ----------
        // Initialize PdfFileStamp with the concatenated PDF as source and the final file as destination
        PdfFileStamp stamp = new PdfFileStamp(combinedPath, outputPath)
        {
            // Optional: set the starting page number (default is 1)
            StartingNumber = 1
        };

        // Add page numbers at the bottom middle of each page.
        // The format string may contain '#' which will be replaced by the actual page number.
        stamp.AddPageNumber("Page #", PdfFileStamp.PosBottomMiddle);

        // Close the stamp facade to write the output file
        stamp.Close();

        // Clean up the intermediate file (optional)
        try { File.Delete(combinedPath); } catch { }

        Console.WriteLine($"Successfully created '{outputPath}' with concatenated content and page numbers.");
    }
}