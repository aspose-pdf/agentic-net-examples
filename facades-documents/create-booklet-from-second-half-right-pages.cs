using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public static class BookletGenerator
{
    /// <summary>
    /// Generates a booklet using only the right‑hand pages from the second half of the source PDF.
    /// </summary>
    /// <param name="inputPdfPath">Full path to the source PDF.</param>
    /// <param name="outputPdfPath">Full path where the booklet PDF will be saved.</param>
    /// <returns>True if the booklet was created successfully; otherwise false.</returns>
    public static bool CreateBookletFromSecondHalfRightPages(string inputPdfPath, string outputPdfPath)
    {
        if (string.IsNullOrWhiteSpace(inputPdfPath) || !File.Exists(inputPdfPath))
            throw new FileNotFoundException("Input PDF not found.", inputPdfPath);

        // Determine total number of pages using the core Document API.
        int totalPages;
        using (Document doc = new Document(inputPdfPath))
        {
            totalPages = doc.Pages.Count;
        }

        if (totalPages == 0)
            throw new InvalidOperationException("The source PDF contains no pages.");

        // Calculate the first page index of the second half (1‑based indexing).
        int secondHalfStart = (totalPages / 2) + 1; // e.g., for 10 pages start = 6

        // Collect right‑hand pages (odd numbers) from the second half.
        List<int> rightPagesList = new List<int>();
        for (int i = secondHalfStart; i <= totalPages; i++)
        {
            if (i % 2 == 1) // odd page numbers are considered right pages
                rightPagesList.Add(i);
        }

        // Left pages are not used in this custom booklet; provide an empty array.
        int[] leftPages = new int[0];
        int[] rightPages = rightPagesList.ToArray();

        // Use PdfFileEditor (does NOT implement IDisposable) to create the booklet.
        PdfFileEditor editor = new PdfFileEditor();
        // MakeBooklet returns void, so we just call it and assume success if no exception is thrown.
        editor.MakeBooklet(inputPdfPath, outputPdfPath, leftPages, rightPages);

        return true;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // Simple demo: expects two arguments – input PDF path and output PDF path.
        if (args.Length != 2)
        {
            Console.WriteLine("Usage: BookletGenerator <inputPdfPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        try
        {
            bool success = BookletGenerator.CreateBookletFromSecondHalfRightPages(inputPath, outputPath);
            Console.WriteLine(success ? "Booklet created successfully." : "Booklet creation failed.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}