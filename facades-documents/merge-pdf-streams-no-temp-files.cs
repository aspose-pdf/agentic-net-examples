using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Pdf.Facades;

namespace PdfMergeApp
{
    public static class PdfMergeUtility
    {
        /// <summary>
        /// Merges multiple PDF streams into a single PDF file without creating intermediate files.
        /// </summary>
        /// <param name="pdfStreams">Enumerable of input PDF streams (e.g., network streams).</param>
        /// <param name="outputPath">Path to the merged PDF file to be saved locally.</param>
        public static void MergePdfStreams(IEnumerable<Stream> pdfStreams, string outputPath)
        {
            if (pdfStreams == null) throw new ArgumentNullException(nameof(pdfStreams));
            if (string.IsNullOrWhiteSpace(outputPath)) throw new ArgumentException("Output path must be provided.", nameof(outputPath));

            // Convert the enumerable to an array as required by PdfFileEditor.Concatenate.
            Stream[] inputArray = pdfStreams.ToArray();

            // Ensure the output directory exists.
            string directory = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Create the output file stream.
            using (FileStream outStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                // PdfFileEditor does NOT implement IDisposable; do not wrap it in a using block.
                PdfFileEditor editor = new PdfFileEditor();

                // Concatenate the input streams into the output stream.
                // This method merges the PDFs directly without temporary files.
                editor.Concatenate(inputArray, outStream);
            }
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            // Simple demonstration of the utility.
            // Expected arguments: <outputPath> <inputPdfPath1> [<inputPdfPath2> ...]
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: PdfMergeApp <outputPath> <inputPdfPath1> [<inputPdfPath2> ...]");
                return;
            }

            string outputPath = args[0];
            var inputPaths = args.Skip(1);

            var streams = new List<Stream>();
            try
            {
                foreach (var path in inputPaths)
                {
                    streams.Add(new FileStream(path, FileMode.Open, FileAccess.Read));
                }

                PdfMergeUtility.MergePdfStreams(streams, outputPath);
                Console.WriteLine($"Merged PDF saved to: {outputPath}");
            }
            finally
            {
                // Ensure all input streams are closed even if an exception occurs.
                foreach (var s in streams)
                {
                    s.Dispose();
                }
            }
        }
    }
}
