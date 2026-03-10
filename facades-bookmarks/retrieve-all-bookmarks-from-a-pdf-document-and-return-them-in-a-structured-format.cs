using System;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

namespace BookmarkExtractor
{
    // Simple POCO to hold bookmark information in a hierarchical way
    public class BookmarkInfo
    {
        public string Title { get; set; }
        public int PageNumber { get; set; }          // 1‑based page index as stored in the PDF
        public List<BookmarkInfo> Children { get; } = new List<BookmarkInfo>();
    }

    public static class PdfBookmarkHelper
    {
        /// <summary>
        /// Retrieves all bookmarks from the specified PDF file.
        /// </summary>
        /// <param name="pdfPath">Full path to the source PDF.</param>
        /// <returns>Root level list of <see cref="BookmarkInfo"/> objects representing the bookmark tree.</returns>
        public static List<BookmarkInfo> GetBookmarks(string pdfPath)
        {
            if (string.IsNullOrWhiteSpace(pdfPath))
                throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

            // PdfBookmarkEditor does not implement IDisposable, so we manually close it after use.
            PdfBookmarkEditor editor = new PdfBookmarkEditor();
            try
            {
                // Bind the PDF file to the editor.
                editor.BindPdf(pdfPath);

                // Extract the complete bookmark collection (recursive).
                Bookmarks rawBookmarks = editor.ExtractBookmarks();

                // Convert the Aspose.Pdf.Facades.Bookmark objects into our POCO hierarchy.
                List<BookmarkInfo> result = new List<BookmarkInfo>();
                foreach (Bookmark bm in rawBookmarks)
                {
                    result.Add(ConvertToBookmarkInfo(bm));
                }

                return result;
            }
            finally
            {
                // Ensure resources are released.
                editor.Close();
            }
        }

        // Recursive conversion from Aspose.Pdf.Facades.Bookmark to BookmarkInfo.
        private static BookmarkInfo ConvertToBookmarkInfo(Bookmark source)
        {
            var info = new BookmarkInfo
            {
                Title = source.Title,
                PageNumber = source.PageNumber   // 1‑based page number; 0 if not linked to a page
            };

            // ChildItem holds nested bookmarks (may be null).
            if (source.ChildItem != null)
            {
                foreach (Bookmark child in source.ChildItem)
                {
                    info.Children.Add(ConvertToBookmarkInfo(child));
                }
            }

            return info;
        }
    }

    // Example usage
    class Program
    {
        static void Main()
        {
            const string inputPdf = "sample.pdf";

            try
            {
                List<BookmarkInfo> bookmarks = PdfBookmarkHelper.GetBookmarks(inputPdf);

                // Simple console output to verify the hierarchy.
                PrintBookmarks(bookmarks, 0);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error extracting bookmarks: {ex.Message}");
            }
        }

        // Helper to display the bookmark tree with indentation.
        private static void PrintBookmarks(IEnumerable<BookmarkInfo> items, int level)
        {
            string indent = new string(' ', level * 2);
            foreach (var bm in items)
            {
                Console.WriteLine($"{indent}- Title: {bm.Title}, Page: {bm.PageNumber}");
                if (bm.Children.Count > 0)
                    PrintBookmarks(bm.Children, level + 1);
            }
        }
    }
}