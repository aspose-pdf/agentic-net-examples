using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public static class PdfBookletHelper
{
    /// <summary>
    /// Accepts a PDF stream, deletes the specified page range, resizes all pages,
    /// and returns a new stream containing the booklet version of the document.
    /// </summary>
    /// <param name="pdfInput">Input PDF as a readable stream (position will be reset).</param>
    /// <param name="deleteStartPage">First page (1‑based) of the range to delete.</param>
    /// <param name="deleteEndPage">Last page (1‑based) of the range to delete.</param>
    /// <param name="newPageWidth">Desired page width (points).</param>
    /// <param name="newPageHeight">Desired page height (points).</param>
    /// <returns>A MemoryStream containing the booklet PDF.</returns>
    public static Stream CreateBookletFromPdf(
        Stream pdfInput,
        int deleteStartPage,
        int deleteEndPage,
        double newPageWidth,
        double newPageHeight)
    {
        if (pdfInput == null) throw new ArgumentNullException(nameof(pdfInput));
        if (!pdfInput.CanRead) throw new ArgumentException("Input stream must be readable.", nameof(pdfInput));
        if (deleteStartPage < 1 || deleteEndPage < deleteStartPage)
            throw new ArgumentException("Invalid page range to delete.");
        if (newPageWidth <= 0 || newPageHeight <= 0)
            throw new ArgumentException("Page dimensions must be positive numbers.");

        // Ensure the input stream is positioned at the beginning.
        pdfInput.Position = 0;

        // Create temporary files for each processing step.
        string tempOriginalPath = Path.GetTempFileName();
        string tempAfterDeletePath = Path.GetTempFileName();
        string tempBookletPath = Path.GetTempFileName();

        try
        {
            // 0. Write the incoming stream to a temporary file so that Aspose can work with it.
            using (var file = new FileStream(tempOriginalPath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                pdfInput.CopyTo(file);
            }

            // 1. Load the PDF with Document (Aspose.Pdf) to manipulate pages.
            var doc = new Document(tempOriginalPath);

            // 2. Delete the specified page range (iterate backwards to keep indexes stable).
            for (int page = deleteEndPage; page >= deleteStartPage; page--)
            {
                if (page <= doc.Pages.Count)
                    doc.Pages.Delete(page);
            }

            // 3. Resize all remaining pages.
            foreach (Page page in doc.Pages)
            {
                page.PageInfo.Width = newPageWidth;
                page.PageInfo.Height = newPageHeight;
            }

            // 4. Save the intermediate result (after delete & resize).
            doc.Save(tempAfterDeletePath);

            // 5. Create the booklet layout using PdfFileEditor.MakeBooklet overload without PageSize.
            var editor = new PdfFileEditor();
            bool success = editor.MakeBooklet(tempAfterDeletePath, tempBookletPath);
            if (!success)
                throw new InvalidOperationException("Failed to create booklet using PdfFileEditor.MakeBooklet.");

            // 6. Load the booklet into a MemoryStream to return to the caller.
            var bookletStream = new MemoryStream();
            using (var file = new FileStream(tempBookletPath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                file.CopyTo(bookletStream);
            }
            bookletStream.Position = 0;
            return bookletStream; // Caller is responsible for disposing.
        }
        finally
        {
            // Clean up all temporary files.
            foreach (var path in new[] { tempOriginalPath, tempAfterDeletePath, tempBookletPath })
            {
                try { if (File.Exists(path)) File.Delete(path); } catch { /* ignore cleanup errors */ }
            }
        }
    }
}

// Dummy entry point to satisfy the compiler when the project is built as an executable.
public static class Program
{
    public static void Main(string[] args)
    {
        // No operation – the library methods are intended to be called by external code.
    }
}