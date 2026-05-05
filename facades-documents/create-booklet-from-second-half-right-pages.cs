using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public static class BookletGenerator
{
    /// <summary>
    /// Creates a booklet PDF using only the right‑hand pages from the second half of the source PDF.
    /// </summary>
    /// <param name="inputPath">Full path to the source PDF.</param>
    /// <param name="outputPath">Full path where the booklet PDF will be saved.</param>
    /// <returns>True if the booklet was created successfully; otherwise false.</returns>
    public static bool CreateBookletFromSecondHalfRight(string inputPath, string outputPath)
    {
        if (string.IsNullOrWhiteSpace(inputPath) || string.IsNullOrWhiteSpace(outputPath))
        {
            Console.Error.WriteLine("Input and output paths must be provided.");
            return false;
        }

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return false;
        }

        try
        {
            // Load the source document only to obtain the total page count.
            int totalPages;
            using (Document srcDoc = new Document(inputPath))
            {
                totalPages = srcDoc.Pages.Count;
            }

            if (totalPages == 0)
            {
                Console.Error.WriteLine("Source PDF contains no pages.");
                return false;
            }

            // Determine the start of the second half (1‑based indexing).
            int secondHalfStart = (totalPages / 2) + 1; // e.g., for 10 pages start = 6

            // Build the right‑pages array: all pages from the second half.
            int rightCount = totalPages - secondHalfStart + 1;
            int[] rightPages = new int[rightCount];
            for (int i = 0; i < rightCount; i++)
            {
                rightPages[i] = secondHalfStart + i; // pages are 1‑based
            }

            // Left pages array is empty because we only want right pages.
            int[] leftPages = new int[0];

            // Use PdfFileEditor to create the customized booklet.
            PdfFileEditor editor = new PdfFileEditor();
            bool success = editor.MakeBooklet(inputPath, outputPath, leftPages, rightPages);

            if (!success)
            {
                Console.Error.WriteLine("MakeBooklet operation reported failure.");
            }

            return success;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error while creating booklet: {ex.Message}");
            return false;
        }
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

        bool result = BookletGenerator.CreateBookletFromSecondHalfRight(inputPath, outputPath);
        Console.WriteLine(result ? "Booklet created successfully." : "Failed to create booklet.");
    }
}
