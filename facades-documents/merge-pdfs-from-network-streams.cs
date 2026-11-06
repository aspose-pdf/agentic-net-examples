using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Pdf;

namespace AsposePdfApi
{
    /// <summary>
    /// Utility class that merges multiple PDF streams into a single PDF file without creating temporary files on disk.
    /// </summary>
    public static class PdfMergerUtility
    {
        /// <summary>
        /// Merges the supplied PDF streams and writes the combined document to <paramref name="outputPath"/>.
        /// </summary>
        /// <param name="pdfStreams">A collection of input PDF streams (e.g., network streams).</param>
        /// <param name="outputPath">Full path where the merged PDF will be saved.</param>
        public static void MergeFromStreams(IEnumerable<Stream> pdfStreams, string outputPath)
        {
            if (pdfStreams == null) throw new ArgumentNullException(nameof(pdfStreams));
            if (string.IsNullOrWhiteSpace(outputPath)) throw new ArgumentException("Output path must be provided.", nameof(outputPath));

            // Ensure none of the input streams are null and are positioned at the beginning.
            foreach (var s in pdfStreams)
            {
                if (s == null) throw new ArgumentException("One of the input streams is null.", nameof(pdfStreams));
                if (s.CanSeek) s.Position = 0;
            }

            // Create an empty document that will hold the merged result.
            Document mergedDocument = new Document();

            // Append pages from each source document.
            foreach (var stream in pdfStreams)
            {
                // Load the source PDF from the stream.
                Document srcDoc = new Document(stream);
                // Add all pages of the source document to the merged document.
                mergedDocument.Pages.Add(srcDoc.Pages);
            }

            // Save the merged PDF to the specified path.
            mergedDocument.Save(outputPath);
        }
    }

    class Program
    {
        /// <summary>
        /// Entry point required by the build system. Demonstrates merging PDFs that are read from a folder
        /// (simulating network streams) and writing the result to a file named "merged.pdf" in the same folder.
        /// </summary>
        static void Main()
        {
            // Folder that contains the PDFs to be merged – replace with your actual source.
            string inputFolder = "InputPdfs";
            Directory.CreateDirectory(inputFolder);

            // Collect all PDF files in the folder and open them as read‑only streams.
            var pdfStreams = Directory.GetFiles(inputFolder, "*.pdf")
                                      .Select(path => (Stream)File.OpenRead(path))
                                      .ToList();

            // Define where the merged PDF will be saved.
            string outputPath = Path.Combine(inputFolder, "merged.pdf");

            // Perform the merge.
            PdfMergerUtility.MergeFromStreams(pdfStreams, outputPath);

            // Clean‑up the opened streams.
            foreach (var s in pdfStreams) s.Dispose();

            Console.WriteLine($"Merged PDF saved to: {outputPath}");
        }
    }
}
