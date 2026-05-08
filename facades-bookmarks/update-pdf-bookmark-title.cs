using System;
using System.IO;
using Aspose.Pdf.Facades;

public static class PdfBookmarkHelper
{
    /// <summary>
    /// Binds a PDF from a byte array, modifies a bookmark title, and returns the updated PDF as a byte array.
    /// </summary>
    /// <param name="pdfBytes">Input PDF data.</param>
    /// <param name="sourceTitle">Existing bookmark title to be changed.</param>
    /// <param name="destTitle">New bookmark title.</param>
    /// <returns>Updated PDF data.</returns>
    public static byte[] UpdateBookmarkTitle(byte[] pdfBytes, string sourceTitle, string destTitle)
    {
        // Input stream containing the original PDF
        using (MemoryStream inputStream = new MemoryStream(pdfBytes))
        {
            // Facade for bookmark editing
            using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
            {
                // Bind the PDF stream to the editor
                bookmarkEditor.BindPdf(inputStream);

                // Modify the bookmark title
                bookmarkEditor.ModifyBookmarks(sourceTitle, destTitle);

                // Output stream to hold the modified PDF
                using (MemoryStream outputStream = new MemoryStream())
                {
                    // Save the edited PDF into the output stream
                    bookmarkEditor.Save(outputStream);

                    // Return the resulting byte array
                    return outputStream.ToArray();
                }
            }
        }
    }
}

// Dummy entry point to satisfy the compiler when the project is built as an executable.
public class Program
{
    public static void Main(string[] args)
    {
        // Intentionally left blank – the library methods are intended to be called from other code.
    }
}