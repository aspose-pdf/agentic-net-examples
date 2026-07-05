using System;
using System.Collections.Generic;
using Aspose.Pdf.Facades;   // PdfXmpMetadata resides here
using Aspose.Pdf;          // XmpValue type

namespace XmpUtility
{
    /// <summary>
    /// Helper class for extracting XMP metadata from a PDF and converting it to a simple dictionary.
    /// </summary>
    public static class XmpHelper
    {
        /// <summary>
        /// Reads the XMP metadata of the specified PDF file and returns a dictionary where
        /// the key is the metadata name (e.g., "dc:creator") and the value is its string representation.
        /// </summary>
        /// <param name="pdfPath">Full path to the PDF file.</param>
        /// <returns>Dictionary of metadata name/value pairs.</returns>
        public static Dictionary<string, string> GetMetadataDictionary(string pdfPath)
        {
            // Ensure the PDF file exists before attempting to bind it.
            if (string.IsNullOrWhiteSpace(pdfPath))
                throw new ArgumentException("PDF path must be a non‑empty string.", nameof(pdfPath));

            // PdfXmpMetadata implements IDisposable via SaveableFacade, so we wrap it in a using block.
            using (PdfXmpMetadata xmp = new PdfXmpMetadata())
            {
                // Bind the PDF document to the facade.
                xmp.BindPdf(pdfPath);

                // The facade implements IDictionary<string, XmpValue>, allowing enumeration of all entries.
                var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

                foreach (KeyValuePair<string, XmpValue> entry in xmp)
                {
                    // XmpValue can represent simple types, arrays, structures, etc.
                    // For most scenarios a string representation is sufficient.
                    // If a more complex handling is required, inspect entry.Value.IsArray, IsStructure, etc.
                    string valueString = entry.Value?.ToString() ?? string.Empty;
                    result[entry.Key] = valueString;
                }

                return result;
            }
        }
    }

    // ---------------------------------------------------------------------
    // The original project was compiled as an executable, therefore a
    // static Main entry point is required. Adding a minimal Program class
    // satisfies the compiler while keeping the library functionality
    // unchanged. The Main method is intentionally lightweight; it can be
    // expanded for demo or testing purposes.
    // ---------------------------------------------------------------------
    internal class Program
    {
        static void Main(string[] args)
        {
            // Optional demonstration (commented out to keep the helper pure).
            // if (args.Length > 0)
            // {
            //     var metadata = XmpHelper.GetMetadataDictionary(args[0]);
            //     foreach (var kvp in metadata)
            //         Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            // }
        }
    }
}
