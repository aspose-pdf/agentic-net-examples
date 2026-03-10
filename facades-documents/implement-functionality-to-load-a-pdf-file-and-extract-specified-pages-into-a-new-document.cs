using System;
using System.IO;
using Aspose.Pdf.Facades;   // Facade API for PDF manipulation

class PdfPageExtractor
{
    /// <summary>
    /// Extracts a range of pages from the source PDF and saves them as a new PDF file.
    /// </summary>
    /// <param name="sourcePdf">Full path to the source PDF file.</param>
    /// <param name="startPage">1‑based start page number (inclusive).</param>
    /// <param name="endPage">1‑based end page number (inclusive).</param>
    /// <param name="outputPdf">Full path where the extracted PDF will be saved.</param>
    /// <returns>True if extraction succeeded; otherwise false.</returns>
    public static bool ExtractPageRange(string sourcePdf, int startPage, int endPage, string outputPdf)
    {
        // Validate input arguments
        if (string.IsNullOrWhiteSpace(sourcePdf) || !File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdf}");
            return false;
        }

        if (startPage < 1 || endPage < startPage)
        {
            Console.Error.WriteLine("Invalid page range specified.");
            return false;
        }

        try
        {
            // PdfFileEditor is a facade class; it does not implement IDisposable,
            // so no using block is required.
            PdfFileEditor editor = new PdfFileEditor();

            // Extract the specified page range. This method writes directly to the output file.
            bool success = editor.Extract(sourcePdf, startPage, endPage, outputPdf);

            if (!success)
                Console.Error.WriteLine("Extraction failed (method returned false).");

            return success;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Exception during extraction: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Extracts specific pages (by number) from the source PDF and saves them as a new PDF file.
    /// </summary>
    /// <param name="sourcePdf">Full path to the source PDF file.</param>
    /// <param name="pages">Array of 1‑based page numbers to extract.</param>
    /// <param name="outputPdf">Full path where the extracted PDF will be saved.</param>
    /// <returns>True if extraction succeeded; otherwise false.</returns>
    public static bool ExtractSpecificPages(string sourcePdf, int[] pages, string outputPdf)
    {
        if (string.IsNullOrWhiteSpace(sourcePdf) || !File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdf}");
            return false;
        }

        if (pages == null || pages.Length == 0)
        {
            Console.Error.WriteLine("Page number array is null or empty.");
            return false;
        }

        try
        {
            PdfFileEditor editor = new PdfFileEditor();
            bool success = editor.Extract(sourcePdf, pages, outputPdf);

            if (!success)
                Console.Error.WriteLine("Extraction failed (method returned false).");

            return success;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Exception during extraction: {ex.Message}");
            return false;
        }
    }
}

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputRangePath = "extracted_range.pdf";
        const string outputArrayPath = "extracted_pages.pdf";

        // Example 1: extract pages 3 through 7 (inclusive)
        bool rangeResult = PdfPageExtractor.ExtractPageRange(inputPath, 3, 7, outputRangePath);
        Console.WriteLine($"Range extraction {(rangeResult ? "succeeded" : "failed")}.");

        // Example 2: extract pages 2, 4, 6 using an array
        int[] pagesToExtract = new int[] { 2, 4, 6 };
        bool arrayResult = PdfPageExtractor.ExtractSpecificPages(inputPath, pagesToExtract, outputArrayPath);
        Console.WriteLine($"Array extraction {(arrayResult ? "succeeded" : "failed")}.");
    }
}