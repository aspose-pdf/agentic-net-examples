using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace PdfUtilities
{
    /// <summary>
    /// Provides functionality to remove XMP metadata from PDF files.
    /// </summary>
    public static class PdfMetadataCleaner
    {
        /// <summary>
        /// Removes the entire XMP metadata block from the specified PDF file
        /// and saves a new metadata‑free PDF.
        /// </summary>
        /// <param name="inputPdfPath">Path to the source PDF.</param>
        /// <param name="outputPdfPath">Path where the cleaned PDF will be saved.</param>
        public static void RemoveXmpMetadata(string inputPdfPath, string outputPdfPath)
        {
            // Ensure the input file exists before proceeding.
            if (!File.Exists(inputPdfPath))
                throw new FileNotFoundException($"Input PDF not found: {inputPdfPath}");

            // PdfXmpMetadata is a facade that works directly on the PDF file.
            // It implements IDisposable, so we wrap it in a using block to guarantee disposal.
            using (PdfXmpMetadata xmp = new PdfXmpMetadata())
            {
                // Bind the facade to the source PDF.
                xmp.BindPdf(inputPdfPath);

                // Clear removes all XMP elements from the document.
                xmp.Clear();

                // Save the resulting PDF without XMP metadata.
                xmp.Save(outputPdfPath);
            }
        }
    }

    /// <summary>
    /// Simple console entry point required for compilation as an executable.
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input PDF path and output PDF path.
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: PdfMetadataCleaner <inputPdfPath> <outputPdfPath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            try
            {
                PdfMetadataCleaner.RemoveXmpMetadata(inputPath, outputPath);
                Console.WriteLine("Metadata removed successfully.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}