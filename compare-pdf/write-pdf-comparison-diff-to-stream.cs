using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

namespace AsposePdfComparisonDemo
{
    /// <summary>
    /// Provides a helper method that compares two PDF files and writes the resulting diff PDF
    /// directly to a supplied <see cref="Stream"/>. This implementation is suitable for a
    /// console‑application or any non‑ASP.NET environment where <c>Microsoft.AspNetCore</c>
    /// assemblies are unavailable.
    /// </summary>
    public static class PdfComparisonHelper
    {
        /// <summary>
        /// Compares <paramref name="firstPdfPath"/> with <paramref name="secondPdfPath"/> and writes
        /// the diff document to <paramref name="output"/>.
        /// </summary>
        /// <param name="firstPdfPath">Full path to the first PDF file.</param>
        /// <param name="secondPdfPath">Full path to the second PDF file.</param>
        /// <param name="output">A writable stream that will receive the diff PDF.</param>
        public static void WriteComparison(string firstPdfPath, string secondPdfPath, Stream output)
        {
            // Validate input files
            if (string.IsNullOrWhiteSpace(firstPdfPath) || !File.Exists(firstPdfPath))
                throw new FileNotFoundException($"File not found: {firstPdfPath}");

            if (string.IsNullOrWhiteSpace(secondPdfPath) || !File.Exists(secondPdfPath))
                throw new FileNotFoundException($"File not found: {secondPdfPath}");

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            // Load the source documents
            using (Document doc1 = new Document(firstPdfPath))
            using (Document doc2 = new Document(secondPdfPath))
            {
                // Customize comparison options if required
                ComparisonOptions options = new ComparisonOptions();

                // The overload that accepts an output Stream writes the diff PDF directly to it.
                Document.Compare(doc1, doc2, output, options);
            }

            // Ensure all buffered data is pushed to the underlying stream.
            output.Flush();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Example usage:
            //   dotnet run <first.pdf> <second.pdf> <outputDiff.pdf>
            if (args.Length != 3)
            {
                Console.WriteLine("Usage: AsposePdfComparisonDemo <firstPdfPath> <secondPdfPath> <outputDiffPath>");
                return;
            }

            string firstPdf = args[0];
            string secondPdf = args[1];
            string outputPdf = args[2];

            try
            {
                using (FileStream outStream = new FileStream(outputPdf, FileMode.Create, FileAccess.Write))
                {
                    PdfComparisonHelper.WriteComparison(firstPdf, secondPdf, outStream);
                }

                Console.WriteLine($"Comparison PDF written to: {outputPdf}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
