using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public class Program
{
    public static void Main(string[] args)
    {
        // Simple demonstration of the booklet generator.
        // Expected arguments: <inputPdfPath> <outputPdfPath>
        if (args.Length >= 2)
        {
            try
            {
                bool success = BookletGenerator.CreateBookletFromSecondHalf(args[0], args[1]);
                Console.WriteLine(success ? "Booklet created successfully." : "Failed to create booklet.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Usage: <inputPdfPath> <outputPdfPath>");
        }
    }
}

public static class BookletGenerator
{
    /// <summary>
    /// Creates a booklet PDF where the left pages come from the first half of the source
    /// and the right pages come from the second half of the source PDF.
    /// </summary>
    /// <param name="inputFile">Path to the source PDF.</param>
    /// <param name="outputFile">Path where the booklet PDF will be saved.</param>
    /// <returns>True if the operation succeeded; otherwise false.</returns>
    public static bool CreateBookletFromSecondHalf(string inputFile, string outputFile)
    {
        if (string.IsNullOrWhiteSpace(inputFile) || string.IsNullOrWhiteSpace(outputFile))
            throw new ArgumentException("Input and output file paths must be provided.");

        // Determine the total number of pages in the source PDF.
        int totalPages;
        using (Document srcDoc = new Document(inputFile))
        {
            totalPages = srcDoc.Pages.Count;
        }

        if (totalPages == 0)
            throw new InvalidOperationException("Source PDF contains no pages.");

        // Split the document into two halves.
        int half = totalPages / 2; // integer division; if odd, the extra page goes to the right side.

        // Left pages: 1 .. half
        int[] leftPages = Enumerable.Range(1, half).ToArray();

        // Right pages: (half + 1) .. totalPages
        int[] rightPages = Enumerable.Range(half + 1, totalPages - half).ToArray();

        // Use PdfFileEditor (Facades) to create the booklet with the specified page arrays.
        PdfFileEditor editor = new PdfFileEditor();
        bool result = editor.MakeBooklet(inputFile, outputFile, leftPages, rightPages);

        return result;
    }
}