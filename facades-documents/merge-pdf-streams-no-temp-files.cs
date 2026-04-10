using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

namespace AsposePdfUtility
{
    public static class PdfMergeUtility
    {
        /// <summary>
        /// Merges multiple PDF streams into a single PDF file without creating temporary files.
        /// </summary>
        /// <param name="inputStreams">Enumerable of input PDF streams (e.g., network streams).</param>
        /// <param name="outputFilePath">Path where the merged PDF will be saved.</param>
        public static void MergePdfStreams(IEnumerable<Stream> inputStreams, string outputFilePath)
        {
            if (inputStreams == null) throw new ArgumentNullException(nameof(inputStreams));
            if (string.IsNullOrWhiteSpace(outputFilePath))
                throw new ArgumentException("Output path must be provided.", nameof(outputFilePath));

            // Convert the enumerable to an array as required by PdfFileEditor.Concatenate.
            Stream[] sourceStreams = new List<Stream>(inputStreams).ToArray();

            // Ensure the output directory exists.
            string outputDir = Path.GetDirectoryName(outputFilePath);
            if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            // Create the output file stream. Using ensures the stream is closed after saving.
            using (FileStream outputStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
            {
                // PdfFileEditor does NOT implement IDisposable; do NOT wrap it in a using block.
                PdfFileEditor editor = new PdfFileEditor();

                // Concatenate all source streams into the output stream.
                editor.Concatenate(sourceStreams, outputStream);
            }
        }
    }

    // Minimal entry point required for a console‑application project.
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Example usage (optional, can be removed in production).
            // var streams = new List<Stream>
            // {
            //     File.OpenRead("first.pdf"),
            //     File.OpenRead("second.pdf")
            // };
            // PdfMergeUtility.MergePdfStreams(streams, "merged.pdf");
        }
    }
}