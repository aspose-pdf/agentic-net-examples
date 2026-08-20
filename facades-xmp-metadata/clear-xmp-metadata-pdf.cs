using System;
using Aspose.Pdf.Facades;

namespace XmpMetadataDemo
{
    /// <summary>
    /// Helper class that clears all XMP metadata from a PDF while preserving the mandatory PDF schema header.
    /// </summary>
    public static class XmpMetadataHelper
    {
        /// <summary>
        /// Clears all XMP metadata from the specified PDF, preserving only the mandatory PDF schema header.
        /// </summary>
        /// <param name="inputPdfPath">Path to the source PDF file.</param>
        /// <param name="outputPdfPath">Path where the cleaned PDF will be saved.</param>
        public static void ClearAllXmpMetadata(string inputPdfPath, string outputPdfPath)
        {
            // Ensure the input file exists before proceeding.
            if (!System.IO.File.Exists(inputPdfPath))
                throw new System.IO.FileNotFoundException($"Input PDF not found: {inputPdfPath}");

            // PdfXmpMetadata implements SaveableFacade and is IDisposable.
            // Use a using block for deterministic disposal (document-disposal-with-using rule).
            using (PdfXmpMetadata xmp = new PdfXmpMetadata())
            {
                // Bind the PDF file to the facade.
                xmp.BindPdf(inputPdfPath);

                // Remove all XMP entries. The required PDF schema header is retained automatically.
                xmp.Clear();

                // Save the modified PDF to the output location.
                xmp.Save(outputPdfPath);
            }
        }
    }

    /// <summary>
    /// Minimal console entry point required for a buildable executable.
    /// </summary>
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Expect exactly two arguments: input PDF path and output PDF path.
            if (args.Length == 2)
            {
                try
                {
                    XmpMetadataHelper.ClearAllXmpMetadata(args[0], args[1]);
                    Console.WriteLine("XMP metadata cleared successfully.");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Usage: XmpMetadataDemo <inputPdfPath> <outputPdfPath>");
            }
        }
    }
}