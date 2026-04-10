using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

namespace AsposePdfParallel
{
    public static class PdfMetadataParallelProcessor
    {
        /// <summary>
        /// Applies the same metadata (Title, Author, Keywords) to a collection of PDF files in parallel.
        /// Each input file is saved to the corresponding output path.
        /// </summary>
        /// <param name="inputFiles">Array of source PDF file paths.</param>
        /// <param name="outputFiles">Array of destination PDF file paths (must match inputFiles length).</param>
        /// <param name="title">Title to set for each PDF.</param>
        /// <param name="author">Author to set for each PDF.</param>
        /// <param name="keywords">Keywords to set for each PDF.</param>
        public static void ApplyMetadataParallel(
            string[] inputFiles,
            string[] outputFiles,
            string title,
            string author,
            string keywords)
        {
            if (inputFiles == null) throw new ArgumentNullException(nameof(inputFiles));
            if (outputFiles == null) throw new ArgumentNullException(nameof(outputFiles));
            if (inputFiles.Length != outputFiles.Length)
                throw new ArgumentException("Input and output arrays must have the same length.");

            // Parallel.ForEach with index to keep input/output pairing
            Parallel.ForEach(
                source: inputFiles,
                body: (inputPath, state, index) =>
                {
                    string outputPath = outputFiles[index];

                    // Validate existence of source file
                    if (!File.Exists(inputPath))
                    {
                        Console.Error.WriteLine($"Source file not found: {inputPath}");
                        return;
                    }

                    // Ensure the output directory exists
                    string outDir = Path.GetDirectoryName(outputPath);
                    if (!string.IsNullOrEmpty(outDir) && !Directory.Exists(outDir))
                    {
                        Directory.CreateDirectory(outDir);
                    }

                    // Use PdfFileInfo facade to modify metadata
                    using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
                    {
                        pdfInfo.Title = title;
                        pdfInfo.Author = author;
                        pdfInfo.Keywords = keywords;
                        pdfInfo.SaveNewInfo(outputPath);
                    }

                    Console.WriteLine($"Metadata applied: {inputPath} → {outputPath}");
                });
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            // Example usage – adjust the paths to match your environment.
            string[] sources = { "doc1.pdf", "doc2.pdf", "doc3.pdf" };
            string[] destinations = { "out/doc1_updated.pdf", "out/doc2_updated.pdf", "out/doc3_updated.pdf" };

            PdfMetadataParallelProcessor.ApplyMetadataParallel(
                sources,
                destinations,
                "My Title",
                "John Doe",
                "sample,metadata");
        }
    }
}
