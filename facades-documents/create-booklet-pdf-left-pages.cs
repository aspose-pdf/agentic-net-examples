using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public static class BookletGenerator
{
    /// <summary>
    /// Generates a booklet PDF using the left pages from the first half of the source PDF.
    /// </summary>
    /// <param name="inputPath">Path to the source PDF file.</param>
    /// <param name="outputPath">Path where the booklet PDF will be saved.</param>
    /// <returns>True if the booklet was created successfully; otherwise false.</returns>
    public static bool CreateBooklet(string inputPath, string outputPath)
    {
        if (string.IsNullOrEmpty(inputPath) || string.IsNullOrEmpty(outputPath))
            throw new ArgumentException("Input and output paths must be provided.");

        if (!File.Exists(inputPath))
            throw new FileNotFoundException($"Source file not found: {inputPath}");

        // Load the source PDF to determine the total number of pages.
        using (Document doc = new Document(inputPath))
        {
            int totalPages = doc.Pages.Count;

            // Determine the midpoint (first half of the document).
            int half = totalPages / 2;

            // Left pages: pages from 1 to half (inclusive).
            int[] leftPages = Enumerable.Range(1, half).ToArray();

            // Right pages: remaining pages.
            int[] rightPages = Enumerable.Range(half + 1, totalPages - half).ToArray();

            // Create the booklet using the specified left and right page sequences.
            PdfFileEditor editor = new PdfFileEditor();
            // MakeBooklet returns void, so we just invoke it and consider the operation successful
            // if no exception is thrown.
            editor.MakeBooklet(inputPath, outputPath, leftPages, rightPages);
            return true;
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // Expecting two arguments: input PDF path and output PDF path.
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <inputPdfPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        try
        {
            bool success = BookletGenerator.CreateBooklet(inputPath, outputPath);
            Console.WriteLine(success ? "Booklet created successfully." : "Booklet creation failed.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}