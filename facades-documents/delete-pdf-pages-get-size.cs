using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Aspose.Pdf;

namespace AsposePdfApi
{
    public static class PdfUtility
    {
        /// <summary>
        /// Deletes the specified pages from a PDF file using Aspose.Pdf.Document and returns the size (in bytes) of the resulting file.
        /// </summary>
        /// <param name="pdfPath">Full path to the source PDF file.</param>
        /// <param name="pagesToDelete">Collection of 1‑based page numbers to delete.</param>
        /// <returns>File size of the PDF after deletion, in bytes.</returns>
        public static long DeletePagesAndGetSize(string pdfPath, IEnumerable<int> pagesToDelete)
        {
            if (string.IsNullOrWhiteSpace(pdfPath))
                throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

            if (!File.Exists(pdfPath))
                throw new FileNotFoundException("PDF file not found.", pdfPath);

            // Ensure page numbers are distinct and sorted descending to avoid index shift during deletion.
            var pages = pagesToDelete
                .Where(p => p > 0)
                .Distinct()
                .OrderByDescending(p => p)
                .ToArray();

            if (pages.Length == 0)
                return new FileInfo(pdfPath).Length; // nothing to delete

            // Load the PDF with Document (which implements IDisposable).
            using (var doc = new Document(pdfPath))
            {
                foreach (int pageNumber in pages)
                {
                    // Guard against out‑of‑range page numbers.
                    if (pageNumber <= doc.Pages.Count)
                    {
                        doc.Pages.Delete(pageNumber);
                    }
                }

                // Overwrite the original file.
                doc.Save(pdfPath);
            }

            // Return the size of the updated PDF.
            return new FileInfo(pdfPath).Length;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Simple demonstration: first argument is the PDF path, the rest are page numbers to delete.
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: <pdfPath> <page1> [page2] ...");
                return;
            }

            string pdfPath = args[0];
            var pages = args.Skip(1)
                            .Select(s => int.TryParse(s, out var p) ? p : -1)
                            .Where(p => p > 0);

            try
            {
                long newSize = PdfUtility.DeletePagesAndGetSize(pdfPath, pages);
                Console.WriteLine($"Updated PDF size: {newSize} bytes");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}