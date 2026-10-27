using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public class BookletGenerator
{
    /// <summary>
    /// Generates a booklet PDF that contains only the right‑hand (odd‑numbered) pages
    /// from the second half of the source PDF.
    /// </summary>
    /// <param name="sourcePath">Full path to the source PDF.</param>
    /// <param name="outputPath">Full path where the booklet PDF will be saved.</param>
    public static void GenerateBooklet(string sourcePath, string outputPath)
    {
        if (!File.Exists(sourcePath))
            throw new FileNotFoundException("Source PDF not found.", sourcePath);

        // Determine the page range that represents the second half of the document.
        int totalPages;
        using (Document srcDoc = new Document(sourcePath))
        {
            totalPages = srcDoc.Pages.Count;
        }

        if (totalPages < 2)
            throw new InvalidOperationException("Source PDF must contain at least two pages.");

        int halfStart = (totalPages / 2) + 1; // first page of the second half (1‑based)

        // Extract the second half into a temporary PDF using the Facades API.
        string tempHalfPath = Path.Combine(Path.GetTempPath(),
                                           Guid.NewGuid().ToString("N") + "_secondHalf.pdf");

        PdfFileEditor editor = new PdfFileEditor();
        // NOTE: In older Aspose.Pdf versions the Extract method signature is
        // Extract(string sourceFile, int startPage, int endPage, string outputFile).
        // The parameters are therefore reordered to match that signature.
        editor.Extract(sourcePath, halfStart, totalPages, tempHalfPath);

        // Build the booklet by selecting only the right‑hand (odd) pages from the extracted half.
        using (Document halfDoc = new Document(tempHalfPath))
        using (Document booklet = new Document())
        {
            // Pages collection is 1‑based.
            for (int i = 1; i <= halfDoc.Pages.Count; i++)
            {
                // Right pages are odd‑numbered.
                if (i % 2 == 1)
                {
                    // Add the page to the booklet.
                    booklet.Pages.Add(halfDoc.Pages[i]);
                }
            }

            // Save the resulting booklet.
            booklet.Save(outputPath);
        }

        // Clean up the temporary file.
        try { File.Delete(tempHalfPath); } catch { /* ignore cleanup errors */ }
    }

    // ---------------------------------------------------------------------
    // Entry point required for a console application (CS5001 fix).
    // ---------------------------------------------------------------------
    public static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: BookletGenerator <sourcePdfPath> <outputPdfPath>");
            return;
        }

        try
        {
            GenerateBooklet(args[0], args[1]);
            Console.WriteLine($"Booklet created successfully at '{args[1]}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
