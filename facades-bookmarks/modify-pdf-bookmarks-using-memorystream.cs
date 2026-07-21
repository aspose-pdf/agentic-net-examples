using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace AsposePdfApi
{
    public static class PdfBookmarkHelper
    {
        /// <summary>
        /// Binds a PDF from a memory stream, renames a bookmark, and returns the updated PDF as a byte array.
        /// </summary>
        /// <param name="pdfBytes">Original PDF content.</param>
        /// <param name="sourceTitle">Existing bookmark title to be changed.</param>
        /// <param name="destTitle">New bookmark title.</param>
        /// <returns>Byte array containing the modified PDF.</returns>
        public static byte[] ModifyBookmarks(byte[] pdfBytes, string sourceTitle, string destTitle)
        {
            // Input stream containing the original PDF
            using (MemoryStream inputStream = new MemoryStream(pdfBytes))
            // Output stream that will hold the modified PDF
            using (MemoryStream outputStream = new MemoryStream())
            // Facade for bookmark operations; implements IDisposable
            using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
            {
                // Bind the PDF from the input stream
                bookmarkEditor.BindPdf(inputStream);

                // Rename the specified bookmark
                bookmarkEditor.ModifyBookmarks(sourceTitle, destTitle);

                // Save the modified PDF into the output stream
                bookmarkEditor.Save(outputStream);

                // Return the resulting bytes
                return outputStream.ToArray();
            }
        }
    }

    // Dummy entry point to satisfy the compiler when the project is built as an executable.
    internal class Program
    {
        private static void Main(string[] args)
        {
            // No operation – the library functionality is exposed via PdfBookmarkHelper.
        }
    }
}
