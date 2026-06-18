using System;
using Aspose.Pdf.Facades;

namespace AsposePdfApi
{
    public static class PdfMetadataHelper
    {
        /// <summary>
        /// Clears all XMP metadata from the specified PDF, preserving only the required PDF schema header.
        /// </summary>
        /// <param name="inputPdfPath">Path to the source PDF file.</param>
        /// <param name="outputPdfPath">Path where the cleaned PDF will be saved.</param>
        public static void ClearXmpMetadata(string inputPdfPath, string outputPdfPath)
        {
            // Validate input arguments.
            if (string.IsNullOrEmpty(inputPdfPath))
                throw new ArgumentException("Input PDF path must be provided.", nameof(inputPdfPath));

            if (string.IsNullOrEmpty(outputPdfPath))
                throw new ArgumentException("Output PDF path must be provided.", nameof(outputPdfPath));

            // Use the PdfXmpMetadata facade to manipulate XMP metadata.
            // The facade implements IDisposable, so wrap it in a using block for deterministic cleanup.
            using (PdfXmpMetadata xmp = new PdfXmpMetadata())
            {
                // Bind the existing PDF document.
                xmp.BindPdf(inputPdfPath);

                // Remove all XMP entries. The required PDF schema header remains intact.
                xmp.Clear();

                // Save the modified PDF to the target location.
                xmp.Save(outputPdfPath);
            }
        }
    }

    class Program
    {
        /// <summary>
        /// Entry point required for a console application.
        /// Accepts two arguments: input PDF path and output PDF path.
        /// </summary>
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: AsposePdfApi <inputPdfPath> <outputPdfPath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            try
            {
                PdfMetadataHelper.ClearXmpMetadata(inputPath, outputPath);
                Console.WriteLine($"XMP metadata cleared successfully. Output saved to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
