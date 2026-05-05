using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace AsposePdfApi
{
    /// <summary>
    /// Provides PDF page deletion utilities using Aspose.Pdf.Facades.
    /// </summary>
    public static class PdfCleaner
    {
        /// <summary>
        /// Deletes the specified pages from each PDF file and returns the paths of the cleaned PDFs.
        /// </summary>
        /// <param name="inputFiles">
        /// Array of full file paths to the source PDF documents.
        /// </param>
        /// <param name="pagesToDelete">
        /// Array of integer arrays, where each inner array contains the page numbers (1‑based) to delete
        /// from the corresponding PDF in <paramref name="inputFiles"/>.
        /// The length of <paramref name="pagesToDelete"/> must match the length of <paramref name="inputFiles"/>.
        /// </param>
        /// <returns>
        /// Array of full file paths to the cleaned PDF documents. Each cleaned file is saved in the same
        /// directory as the source file with a "_cleaned" suffix before the extension.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown when the input arrays have mismatched lengths.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the Aspose.Pdf.Facades operation fails for any file.
        /// </exception>
        public static string[] DeletePagesFromPdfs(string[] inputFiles, int[][] pagesToDelete)
        {
            if (inputFiles == null) throw new ArgumentNullException(nameof(inputFiles));
            if (pagesToDelete == null) throw new ArgumentNullException(nameof(pagesToDelete));
            if (inputFiles.Length != pagesToDelete.Length)
                throw new ArgumentException("The number of input files must match the number of page‑deletion specifications.");

            // Result array to hold the paths of the cleaned PDFs.
            string[] cleanedFiles = new string[inputFiles.Length];

            // Iterate over each source PDF.
            for (int i = 0; i < inputFiles.Length; i++)
            {
                string sourcePath = inputFiles[i];
                int[] pages = pagesToDelete[i] ?? Array.Empty<int>();

                // Validate source file existence.
                if (!File.Exists(sourcePath))
                    throw new FileNotFoundException($"Source PDF not found: {sourcePath}");

                // Build the output file path (same folder, "_cleaned" suffix).
                string directory = Path.GetDirectoryName(sourcePath) ?? string.Empty;
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(sourcePath);
                string outputPath = Path.Combine(directory, $"{fileNameWithoutExt}_cleaned.pdf");

                // Use Aspose.Pdf.Facades.PdfFileEditor to delete pages.
                // The Delete method loads the source PDF, removes the specified pages,
                // and saves the result to the output path.
                PdfFileEditor editor = new PdfFileEditor();
                try
                {
                    editor.Delete(sourcePath, pages, outputPath);
                }
                catch (Exception ex)
                {
                    // Wrap any Aspose‑specific exception in a more generic one for callers.
                    throw new InvalidOperationException($"Failed to delete pages from '{sourcePath}'.", ex);
                }

                cleanedFiles[i] = outputPath;
            }

            return cleanedFiles;
        }
    }

    // Entry point required for a console‑style project.
    internal class Program
    {
        /// <summary>
        /// Optional demonstration entry point. It does not perform any operation by default.
        /// </summary>
        /// <param name="args">Command‑line arguments (unused).</param>
        private static void Main(string[] args)
        {
            // The library can be used by calling PdfCleaner.DeletePagesFromPdfs(...).
            // No action is required here for the build to succeed.
        }
    }
}
