using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public static class PdfBookletHelper
{
    /// <summary>
    /// Accepts a PDF stream, deletes a page range, resizes all pages, and returns a booklet PDF stream.
    /// </summary>
    /// <param name="pdfInput">Input PDF as a readable stream.</param>
    /// <param name="deleteStart">First page to delete (1‑based inclusive).</param>
    /// <param name="deleteEnd">Last page to delete (1‑based inclusive).</param>
    /// <param name="newWidth">New page width in points.</param>
    /// <param name="newHeight">New page height in points.</param>
    /// <returns>MemoryStream containing the booklet PDF.</returns>
    public static Stream CreateBooklet(Stream pdfInput, int deleteStart, int deleteEnd, double newWidth, double newHeight)
    {
        // Load the PDF from the incoming stream using Document (PdfFileEditor has no BindPdf).
        Document pdfDoc = new Document(pdfInput);

        // ---------------------------------------------------------------------
        // 1. Delete the requested page range (if the range is valid).
        //    Deleting from the highest index downwards prevents re‑indexing issues.
        // ---------------------------------------------------------------------
        if (deleteStart > 0 && deleteEnd >= deleteStart && deleteEnd <= pdfDoc.Pages.Count)
        {
            for (int i = deleteEnd; i >= deleteStart; i--)
            {
                pdfDoc.Pages.Delete(i);
            }
        }

        // ---------------------------------------------------------------------
        // 2. Resize every remaining page to the new dimensions.
        //    Page.SetPageSize(width, height) works with points.
        // ---------------------------------------------------------------------
        foreach (Page page in pdfDoc.Pages)
        {
            page.SetPageSize(newWidth, newHeight);
        }

        // ---------------------------------------------------------------------
        // 3. Save the modified document to a temporary file – PdfFileEditor works
        //    with file paths, not streams.
        // ---------------------------------------------------------------------
        string tempInputPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");
        string tempOutputPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");
        pdfDoc.Save(tempInputPath);

        // ---------------------------------------------------------------------
        // 4. Create the booklet using the overload that only requires input and
        //    output file paths (no PageSize enum).
        // ---------------------------------------------------------------------
        PdfFileEditor editor = new PdfFileEditor();
        editor.MakeBooklet(tempInputPath, tempOutputPath);

        // ---------------------------------------------------------------------
        // 5. Load the resulting booklet PDF into a MemoryStream for the caller.
        // ---------------------------------------------------------------------
        MemoryStream outputStream = new MemoryStream();
        using (FileStream fs = new FileStream(tempOutputPath, FileMode.Open, FileAccess.Read))
        {
            fs.CopyTo(outputStream);
        }
        outputStream.Position = 0;

        // ---------------------------------------------------------------------
        // 6. Clean up temporary files.
        // ---------------------------------------------------------------------
        try { File.Delete(tempInputPath); } catch { /* ignore */ }
        try { File.Delete(tempOutputPath); } catch { /* ignore */ }

        return outputStream;
    }

    // Dummy entry point to satisfy the compiler when building as an executable.
    public static void Main() { }
}
