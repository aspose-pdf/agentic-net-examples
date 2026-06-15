using System;
using System.IO;
using Aspose.Pdf.Facades;

public static class PdfBookmarkHelper
{
    /// <summary>
    /// Binds a PDF from a memory stream, modifies a bookmark title, and returns the updated PDF as a byte array.
    /// </summary>
    /// <param name="pdfBytes">Input PDF data.</param>
    /// <param name="sourceTitle">Existing bookmark title to be changed.</param>
    /// <param name="destTitle">New bookmark title.</param>
    /// <returns>Byte array containing the updated PDF.</returns>
    public static byte[] ModifyBookmark(byte[] pdfBytes, string sourceTitle, string destTitle)
    {
        if (pdfBytes == null) throw new ArgumentNullException(nameof(pdfBytes));
        if (string.IsNullOrEmpty(sourceTitle)) throw new ArgumentException("Source title is required.", nameof(sourceTitle));
        if (string.IsNullOrEmpty(destTitle)) throw new ArgumentException("Destination title is required.", nameof(destTitle));

        // Input stream containing the original PDF
        using (MemoryStream inputStream = new MemoryStream(pdfBytes))
        // Output stream that will hold the modified PDF
        using (MemoryStream outputStream = new MemoryStream())
        // PdfBookmarkEditor facade for bookmark operations
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Bind the PDF from the input stream
            editor.BindPdf(inputStream);

            // Modify the bookmark title
            editor.ModifyBookmarks(sourceTitle, destTitle);

            // Save the updated PDF to the output stream
            editor.Save(outputStream);

            // Return the resulting byte array
            return outputStream.ToArray();
        }
    }
}

// Dummy entry point to satisfy the console‑application requirement.
public class Program
{
    public static void Main(string[] args)
    {
        // No operation – the library methods are intended to be called from other code.
    }
}
