using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace AsposePdfUtilities
{
    public static class PdfUtilities
    {
        /// <summary>
        /// Deletes the specified pages from a PDF file and returns the size (in bytes) of the resulting file.
        /// </summary>
        /// <param name="inputPdfPath">Full path to the source PDF.</param>
        /// <param name="pagesToDelete">Array of page numbers to delete (1‑based indexing).</param>
        /// <returns>File size of the PDF after deletion, in bytes.</returns>
        public static long DeletePagesAndGetSize(string inputPdfPath, int[] pagesToDelete)
        {
            if (string.IsNullOrWhiteSpace(inputPdfPath))
                throw new ArgumentException("Input PDF path must be provided.", nameof(inputPdfPath));

            if (!File.Exists(inputPdfPath))
                throw new FileNotFoundException("Input PDF not found.", inputPdfPath);

            if (pagesToDelete == null || pagesToDelete.Length == 0)
                throw new ArgumentException("At least one page number must be specified.", nameof(pagesToDelete));

            // Create a temporary file for the output PDF.
            string tempOutputPath = Path.Combine(Path.GetDirectoryName(inputPdfPath) ?? string.Empty,
                                                 Guid.NewGuid().ToString("N") + ".pdf");

            // Use Aspose.Pdf.Facades.PdfFileEditor to delete pages.
            PdfFileEditor editor = new PdfFileEditor();
            bool success = editor.Delete(inputPdfPath, pagesToDelete, tempOutputPath);

            if (!success)
                throw new InvalidOperationException("Failed to delete pages using PdfFileEditor.");

            // Get the size of the resulting PDF.
            long fileSize = new FileInfo(tempOutputPath).Length;

            // Overwrite the original file with the modified one.
            File.Copy(tempOutputPath, inputPdfPath, overwrite: true);
            File.Delete(tempOutputPath);

            return fileSize;
        }
    }

    // Dummy entry point to satisfy the compiler when the project is built as an executable.
    internal class Program
    {
        private static void Main(string[] args)
        {
            // No operation – the library methods are intended to be called from other code.
            // This placeholder allows the project to compile without requiring a real console UI.
        }
    }
}
