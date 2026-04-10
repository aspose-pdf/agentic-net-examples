using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public static class BookletGenerator
{
    /// <summary>
    /// Generates a booklet using only the right‑hand pages from the second half of the source PDF.
    /// The method extracts those pages to a temporary PDF and then creates a booklet from it.
    /// </summary>
    /// <param name="sourcePdfPath">Full path to the source PDF file.</param>
    /// <param name="outputBookletPath">Full path where the booklet PDF will be saved.</param>
    public static void CreateBookletFromRightPages(string sourcePdfPath, string outputBookletPath)
    {
        if (!File.Exists(sourcePdfPath))
            throw new FileNotFoundException($"Source PDF not found: {sourcePdfPath}");

        // Determine the right‑hand pages (odd page numbers) that belong to the second half of the document.
        int[] rightPages;
        using (Document srcDoc = new Document(sourcePdfPath))
        {
            int totalPages = srcDoc.Pages.Count;                     // 1‑based page count
            int halfStart = totalPages / 2 + 1;                     // first page of the second half
            var pages = new List<int>();

            for (int i = halfStart; i <= totalPages; i++)
            {
                // In a typical booklet layout, right pages are odd numbers.
                if (i % 2 == 1)
                    pages.Add(i);
            }

            rightPages = pages.ToArray();
        }

        if (rightPages.Length == 0)
            throw new InvalidOperationException("No right‑hand pages found in the second half of the document.");

        // Extract the selected pages to a temporary PDF file.
        string tempExtractPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");
        try
        {
            PdfFileEditor editor = new PdfFileEditor();
            // Extract does not return a value; it throws on failure.
            editor.Extract(sourcePdfPath, rightPages, tempExtractPath);

            // Create the booklet from the extracted temporary PDF.
            // MakeBooklet also does not return a value; it throws on failure.
            editor.MakeBooklet(tempExtractPath, outputBookletPath);
        }
        finally
        {
            // Clean up the temporary file if it exists.
            if (File.Exists(tempExtractPath))
            {
                try { File.Delete(tempExtractPath); } catch { /* ignore cleanup errors */ }
            }
        }
    }
}

public class Program
{
    /// <summary>
    /// Simple entry point required for compilation. It forwards arguments to the booklet generator
    /// when the correct number of parameters is supplied.
    /// </summary>
    /// <param name="args">[0] = source PDF path, [1] = output booklet path</param>
    public static void Main(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("Usage: BookletGenerator <sourcePdfPath> <outputBookletPath>");
            return;
        }

        try
        {
            BookletGenerator.CreateBookletFromRightPages(args[0], args[1]);
            Console.WriteLine("Booklet created successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
