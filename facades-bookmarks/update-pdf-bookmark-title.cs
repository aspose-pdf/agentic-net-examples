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
    public static byte[] UpdateBookmarkTitle(byte[] pdfBytes, string sourceTitle, string destTitle)
    {
        // Input stream wrapping the original PDF bytes
        using (MemoryStream inputStream = new MemoryStream(pdfBytes))
        // PdfBookmarkEditor is a SaveableFacade; use it to edit bookmarks
        using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
        {
            // Bind the PDF stream to the editor
            bookmarkEditor.BindPdf(inputStream);

            // Modify the bookmark title
            bookmarkEditor.ModifyBookmarks(sourceTitle, destTitle);

            // Output stream to capture the modified PDF
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

// Dummy entry point – required when the project is built as an executable.
public class Program
{
    public static void Main(string[] args)
    {
        // The library method can be called from here or from other projects.
        // Example (commented out):
        // byte[] pdf = File.ReadAllBytes("input.pdf");
        // byte[] updated = PdfBookmarkHelper.UpdateBookmarkTitle(pdf, "OldTitle", "NewTitle");
        // File.WriteAllBytes("output.pdf", updated);
    }
}