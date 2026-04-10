using System;
using System.IO;
using Aspose.Pdf.Facades;

public static class PdfBookmarkHelper
{
    /// <summary>
    /// Binds a PDF from a memory stream, modifies a bookmark title, and returns the updated PDF as a byte array.
    /// </summary>
    /// <param name="pdfBytes">Original PDF content.</param>
    /// <param name="sourceTitle">Existing bookmark title to be changed.</param>
    /// <param name="destTitle">New bookmark title.</param>
    /// <returns>Byte array containing the modified PDF.</returns>
    public static byte[] UpdateBookmark(byte[] pdfBytes, string sourceTitle, string destTitle)
    {
        // Input stream containing the original PDF
        using (MemoryStream inputStream = new MemoryStream(pdfBytes))
        // PdfBookmarkEditor facade for bookmark operations
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Bind the PDF stream to the editor
            editor.BindPdf(inputStream);

            // Modify the bookmark title
            editor.ModifyBookmarks(sourceTitle, destTitle);

            // Output stream to hold the modified PDF
            using (MemoryStream outputStream = new MemoryStream())
            {
                // Save the updated PDF into the output stream
                editor.Save(outputStream);

                // Return the resulting byte array
                return outputStream.ToArray();
            }
        }
    }
}

// Dummy entry point to satisfy the compiler when the project is built as an executable.
public class Program
{
    public static void Main(string[] args)
    {
        // No operation – the library methods are intended to be called from other code.
    }
}