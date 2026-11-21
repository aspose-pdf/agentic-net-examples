using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public class Program
{
    public static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: BookletGenerator <sourcePdfPath> <outputPdfPath>");
            return;
        }

        try
        {
            BookletGenerator.GenerateBooklet(args[0], args[1]);
            Console.WriteLine($"Booklet created at {args[1]}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}

public static class BookletGenerator
{
    /// <summary>
    /// Generates a booklet PDF that contains only the left (even‑numbered) pages
    /// from the first half of the source document.
    /// </summary>
    /// <param name="sourcePdfPath">Path to the source PDF.</param>
    /// <param name="outputPdfPath">Path where the booklet PDF will be saved.</param>
    public static void GenerateBooklet(string sourcePdfPath, string outputPdfPath)
    {
        if (!File.Exists(sourcePdfPath))
            throw new FileNotFoundException($"Source file not found: {sourcePdfPath}");

        // Obtain the total page count using the core Document API.
        // Document implements IDisposable, so wrap it in a using block.
        int totalPages;
        using (Document srcDoc = new Document(sourcePdfPath))
        {
            totalPages = srcDoc.Pages.Count;
        }

        // Determine the first half of the document (integer division).
        int halfPageCount = totalPages / 2;

        // Collect even page numbers (left pages) within the first half.
        List<int> leftPages = new List<int>();
        for (int pageNum = 2; pageNum <= halfPageCount; pageNum += 2)
        {
            leftPages.Add(pageNum);
        }

        // If there are no left pages, create an empty PDF.
        if (leftPages.Count == 0)
        {
            using (Document emptyDoc = new Document())
            {
                emptyDoc.Save(outputPdfPath);
            }
            return;
        }

        // Use the Facades API (PdfFileEditor) to extract the selected pages.
        // PdfFileEditor does NOT implement IDisposable, so no using block is needed.
        PdfFileEditor editor = new PdfFileEditor();
        // NOTE: The correct overload order is (sourcePath, pageNumbers, outputPath).
        editor.Extract(sourcePdfPath, leftPages.ToArray(), outputPdfPath);
    }
}