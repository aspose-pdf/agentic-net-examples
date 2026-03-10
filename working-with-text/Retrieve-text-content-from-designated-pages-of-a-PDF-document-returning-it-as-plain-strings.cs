using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

public static class PdfTextExtractor
{
    /// <summary>
    /// Extracts plain text from the specified pages of a PDF file.
    /// </summary>
    /// <param name="pdfPath">Full path to the source PDF document.</param>
    /// <param name="pageNumbers">
    /// Collection of 1‑based page numbers to extract.
    /// Values outside the document range are ignored.
    /// </param>
    /// <returns>
    /// A dictionary where the key is the page number and the value is the extracted text.
    /// </returns>
    public static Dictionary<int, string> ExtractTextFromPages(string pdfPath, IEnumerable<int> pageNumbers)
    {
        if (string.IsNullOrEmpty(pdfPath))
            throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

        if (!File.Exists(pdfPath))
            throw new FileNotFoundException("PDF file not found.", pdfPath);

        var result = new Dictionary<int, string>();

        // Use the recommended lifecycle pattern: wrap Document in a using block.
        using (Document doc = new Document(pdfPath))
        {
            // Ensure the document has pages.
            if (doc.Pages == null || doc.Pages.Count == 0)
                return result; // empty PDF

            foreach (int pageNum in pageNumbers)
            {
                // Page indexing in Aspose.Pdf is 1‑based (rule: page-indexing-one-based).
                if (pageNum < 1 || pageNum > doc.Pages.Count)
                    continue; // skip invalid page numbers

                // Create a fresh TextAbsorber for each page to get isolated text.
                TextAbsorber absorber = new TextAbsorber();

                // Extract text from the specific page (rule: text-extraction-use-textabsorber-not-page-extracttext).
                doc.Pages[pageNum].Accept(absorber);

                // Store the extracted text.
                result[pageNum] = absorber.Text ?? string.Empty;
            }
        }

        return result;
    }
}

// Entry point required for a console‑application build.
public class Program
{
    public static void Main(string[] args)
    {
        // Simple demonstration when the program is run from the command line.
        // Expected usage: dotnet run <pdfPath> <page1> <page2> ...
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: <pdfPath> [pageNumbers...]");
            return;
        }

        string pdfPath = args[0];
        var pages = new List<int>();
        for (int i = 1; i < args.Length; i++)
        {
            if (int.TryParse(args[i], out int p))
                pages.Add(p);
        }

        var texts = PdfTextExtractor.ExtractTextFromPages(pdfPath, pages);
        foreach (var kvp in texts)
        {
            Console.WriteLine($"--- Page {kvp.Key} ---");
            Console.WriteLine(kvp.Value);
        }
    }
}
