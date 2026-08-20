using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

namespace AsposePdfApi
{
    public static class PdfCleaner
    {
        /// <summary>
        /// Deletes the specified pages from each PDF file in <paramref name="inputFiles"/>
        /// and returns the paths of the cleaned PDFs.
        /// </summary>
        /// <param name="inputFiles">Full paths of the source PDF files.</param>
        /// <param name="pagesToDelete">
        /// Zero‑based page numbers to delete (e.g., new int[] { 1, 2 } will delete pages 2 and 3,
        /// because Aspose.Pdf.Facades uses 1‑based indexing internally).
        /// </param>
        /// <returns>List of file paths for the cleaned PDFs.</returns>
        public static List<string> DeletePagesFromPdfs(IEnumerable<string> inputFiles, int[] pagesToDelete)
        {
            if (pagesToDelete == null || pagesToDelete.Length == 0)
                throw new ArgumentException("pagesToDelete must contain at least one page number.", nameof(pagesToDelete));

            var cleanedFiles = new List<string>();
            var editor = new PdfFileEditor(); // Aspose.Pdf.Facades editor

            foreach (var inputPath in inputFiles)
            {
                if (string.IsNullOrWhiteSpace(inputPath))
                    continue;

                if (!File.Exists(inputPath))
                    throw new FileNotFoundException($"Input PDF not found: {inputPath}");

                // Build output path: same folder, same name with "_cleaned" suffix
                string directory = Path.GetDirectoryName(inputPath) ?? string.Empty; // Guard against null
                string filenameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
                string outputPath = Path.Combine(directory, $"{filenameWithoutExt}_cleaned.pdf");

                // Delete the pages and save to outputPath
                editor.Delete(inputPath, pagesToDelete, outputPath);

                cleanedFiles.Add(outputPath);
            }

            // PdfFileEditor does not implement IDisposable, so no using block needed.
            return cleanedFiles;
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            // Optional demo – can be removed or replaced with real arguments.
            // var cleaned = PdfCleaner.DeletePagesFromPdfs(new[] { "sample.pdf" }, new[] { 2, 3 });
        }
    }
}