using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace AsposePdfApi
{
    public static class BookletGenerator
    {
        /// <summary>
        /// Generates a booklet using only the right (odd-numbered) pages from the second half of the source PDF.
        /// </summary>
        /// <param name="inputPdfPath">Path to the source PDF file.</param>
        /// <param name="outputPdfPath">Path where the booklet PDF will be saved.</param>
        /// <returns>True if the booklet was created successfully; otherwise false.</returns>
        public static bool CreateBookletFromSecondHalfRightPages(string inputPdfPath, string outputPdfPath)
        {
            if (string.IsNullOrEmpty(inputPdfPath) || string.IsNullOrEmpty(outputPdfPath))
                throw new ArgumentException("Input and output file paths must be provided.");

            if (!File.Exists(inputPdfPath))
                throw new FileNotFoundException($"Source PDF not found: {inputPdfPath}");

            // Determine the total number of pages in the source document.
            int totalPages;
            using (Document srcDoc = new Document(inputPdfPath))
            {
                totalPages = srcDoc.Pages.Count;
            }

            // Calculate the start page of the second half (1‑based indexing).
            int half = totalPages / 2;
            int secondHalfStart = half + 1; // pages greater than half belong to the second half

            // Collect right (odd) page numbers from the second half.
            List<int> rightPagesList = new List<int>();
            for (int pageNum = secondHalfStart; pageNum <= totalPages; pageNum++)
            {
                if (pageNum % 2 == 1) // odd page numbers are considered "right" pages
                    rightPagesList.Add(pageNum);
            }

            // Left pages array is empty because we only want right pages.
            int[] leftPages = new int[0];
            int[] rightPages = rightPagesList.ToArray();

            // Create the booklet using PdfFileEditor.
            PdfFileEditor editor = new PdfFileEditor();
            bool success = editor.MakeBooklet(inputPdfPath, outputPdfPath, leftPages, rightPages);

            return success;
        }
    }

    // Dummy entry point to satisfy the compiler when the project is built as an executable.
    // The method does not perform any operation; it merely provides the required Main method.
    public class Program
    {
        public static void Main(string[] args)
        {
            // No action required. The library methods can be invoked from external callers or unit tests.
        }
    }
}
