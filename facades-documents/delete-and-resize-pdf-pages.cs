using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;

public static class PdfProcessor
{
    /// <summary>
    /// Loads a PDF, deletes the specified pages, resizes all remaining pages,
    /// and returns the result as a MemoryStream.
    /// </summary>
    /// <param name="pdfPath">Path to the source PDF file.</param>
    /// <param name="pagesToDelete">Zero‑based page numbers to delete (e.g., new[] {0,2} deletes pages 1 and 3).</param>
    /// <param name="newWidth">Desired page width in points (1 point = 1/72 inch).</param>
    /// <param name="newHeight">Desired page height in points.</param>
    /// <returns>A stream containing the processed PDF.</returns>
    public static Stream DeleteAndResize(string pdfPath, int[] pagesToDelete, double newWidth, double newHeight)
    {
        if (!File.Exists(pdfPath))
            throw new FileNotFoundException($"PDF file not found: {pdfPath}");

        // Load the PDF using the core Document API.
        Document doc = new Document(pdfPath);

        // Delete pages.  Aspose.Pdf uses 1‑based page numbers, so convert.
        // Deleting from highest index to lowest prevents index shifting.
        foreach (int pageIndex in pagesToDelete.OrderByDescending(p => p))
        {
            int pageNumber = pageIndex + 1; // 1‑based
            if (pageNumber <= doc.Pages.Count && pageNumber > 0)
                doc.Pages.Delete(pageNumber);
        }

        // Resize each remaining page.
        for (int i = 1; i <= doc.Pages.Count; i++)
        {
            Page page = doc.Pages[i];
            page.PageInfo.Width = newWidth;
            page.PageInfo.Height = newHeight;
        }

        // Save the modified document into a MemoryStream.
        MemoryStream outputStream = new MemoryStream();
        doc.Save(outputStream);
        outputStream.Position = 0; // reset for consumer
        return outputStream;
    }
}

// Dummy entry point to satisfy console‑app projects that require a Main method.
public class Program
{
    public static void Main(string[] args)
    {
        // No operation – the library methods are intended to be called from other code.
    }
}