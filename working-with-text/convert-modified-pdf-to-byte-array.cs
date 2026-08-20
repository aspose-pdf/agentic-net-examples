using System;
using System.IO;
using Aspose.Pdf;

namespace PdfUtility
{
    /// <summary>
    /// Provides helper methods for PDF manipulation.
    /// </summary>
    public static class PdfHelper
    {
        /// <summary>
        /// Loads a PDF, optionally modifies it, and returns the document as a byte array.
        /// </summary>
        /// <param name="inputPath">Path to the source PDF file.</param>
        /// <returns>Byte array containing the PDF data.</returns>
        public static byte[] GetPdfBytes(string inputPath)
        {
            if (!File.Exists(inputPath))
                throw new FileNotFoundException($"File not found: {inputPath}");

            // Load the PDF document inside a using block for deterministic disposal.
            using (Document doc = new Document(inputPath))
            {
                // Example modification: add a blank page at the end.
                doc.Pages.Add();

                // Save the document into a memory stream.
                using (MemoryStream ms = new MemoryStream())
                {
                    doc.Save(ms);               // Document.Save(Stream) writes PDF bytes to the stream.
                    return ms.ToArray();        // Retrieve the byte array for further processing or transmission.
                }
            }
        }
    }

    /// <summary>
    /// Entry point required for a console application.
    /// Demonstrates how to call <see cref="PdfHelper.GetPdfBytes"/>.
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            // Expect the first argument to be the path of the PDF to process.
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: PdfUtility <input-pdf-path>");
                return;
            }

            string inputPath = args[0];
            try
            {
                byte[] pdfBytes = PdfHelper.GetPdfBytes(inputPath);
                Console.WriteLine($"PDF processed successfully. Byte array length: {pdfBytes.Length}");

                // Optional: write the modified PDF back to disk for verification.
                string outputPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? string.Empty,
                                                 Path.GetFileNameWithoutExtension(inputPath) + "_modified.pdf");
                File.WriteAllBytes(outputPath, pdfBytes);
                Console.WriteLine($"Modified PDF saved to: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
