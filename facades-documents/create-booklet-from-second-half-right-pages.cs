using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public static class BookletGenerator
{
    /// <summary>
    /// Generates a booklet PDF that contains only the right‑hand pages from the second half of the source PDF.
    /// The right pages are the odd‑numbered pages (1‑based indexing) that belong to the second half.
    /// </summary>
    /// <param name="inputPdfPath">Full path to the source PDF.</param>
    /// <param name="outputPdfPath">Full path where the booklet PDF will be saved.</param>
    public static void CreateBookletFromSecondHalfRightPages(string inputPdfPath, string outputPdfPath)
    {
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Determine the total number of pages in the source document.
        int totalPages;
        using (Document srcDoc = new Document(inputPdfPath))
        {
            totalPages = srcDoc.Pages.Count;
        }

        if (totalPages == 0)
        {
            Console.Error.WriteLine("Source PDF contains no pages.");
            return;
        }

        // Calculate the start page of the second half (1‑based indexing).
        // If the page count is odd, the middle page belongs to the first half.
        int secondHalfStart = (totalPages / 2) + 1;

        // Collect odd (right‑hand) page numbers from the second half.
        var rightPagesList = new System.Collections.Generic.List<int>();
        for (int pageNum = secondHalfStart; pageNum <= totalPages; pageNum++)
        {
            // Right pages are odd numbers in a left‑to‑right reading order.
            if (pageNum % 2 == 1)
                rightPagesList.Add(pageNum);
        }

        int[] rightPages = rightPagesList.ToArray();
        int[] leftPages = new int[0]; // No left pages are required for this custom booklet.

        // Use PdfFileEditor (does NOT implement IDisposable) to create the booklet.
        PdfFileEditor editor = new PdfFileEditor();
        bool success = editor.MakeBooklet(inputPdfPath, outputPdfPath, leftPages, rightPages);

        if (success)
            Console.WriteLine($"Booklet created successfully: {outputPdfPath}");
        else
            Console.Error.WriteLine("Failed to create booklet.");
    }
}

public class Program
{
    /// <summary>
    /// Entry point required for a console application.
    /// Usage: BookletGenerator.exe <inputPdfPath> <outputPdfPath>
    /// </summary>
    public static void Main(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("Usage: BookletGenerator <inputPdfPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        BookletGenerator.CreateBookletFromSecondHalfRightPages(inputPath, outputPath);
    }
}
