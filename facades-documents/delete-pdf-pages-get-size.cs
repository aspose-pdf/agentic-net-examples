using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace AsposePdfApi
{
    public static class PdfUtilities
    {
        /// <summary>
        /// Deletes the specified pages from a PDF file and returns the size (in bytes) of the resulting PDF.
        /// </summary>
        /// <param name="pdfPath">Full path to the source PDF file.</param>
        /// <param name="pagesToDelete">Array of page numbers to delete (1‑based indexing).</param>
        /// <returns>File size of the PDF after deletion, in bytes.</returns>
        public static long DeletePagesAndGetSize(string pdfPath, int[] pagesToDelete)
        {
            if (string.IsNullOrEmpty(pdfPath))
                throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

            if (!File.Exists(pdfPath))
                throw new FileNotFoundException("Input PDF not found.", pdfPath);

            if (pagesToDelete == null || pagesToDelete.Length == 0)
                throw new ArgumentException("At least one page number must be specified.", nameof(pagesToDelete));

            // Create a temporary output file path in the same directory.
            string directory = Path.GetDirectoryName(pdfPath) ?? string.Empty; // Guard against null
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
            string tempOutputPath = Path.Combine(directory, $"{fileNameWithoutExt}_deleted_{Guid.NewGuid()}.pdf");

            // Use PdfFileEditor to delete the pages.
            PdfFileEditor editor = new PdfFileEditor();
            bool success = editor.Delete(pdfPath, pagesToDelete, tempOutputPath);

            if (!success)
                throw new InvalidOperationException("Failed to delete pages from the PDF.");

            // Get the size of the resulting PDF.
            FileInfo info = new FileInfo(tempOutputPath);
            long sizeInBytes = info.Length;

            // Replace the original file with the new one (optional).
            File.Delete(pdfPath);
            File.Move(tempOutputPath, pdfPath);

            return sizeInBytes;
        }
    }

    // Minimal entry point to satisfy the compiler for a console‑type project.
    internal class Program
    {
        static void Main(string[] args)
        {
            // No operation – the library methods can be called from other code or tests.
        }
    }
}
