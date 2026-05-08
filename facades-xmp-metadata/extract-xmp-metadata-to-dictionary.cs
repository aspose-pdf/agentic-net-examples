using System;
using System.Collections.Generic;
using Aspose.Pdf.Facades;   // PdfXmpMetadata resides here
using Aspose.Pdf;          // XmpValue type

namespace XmpHelper
{
    /// <summary>
    /// Provides utility methods for extracting XMP metadata from a PDF file.
    /// </summary>
    public static class XmpMetadataExtractor
    {
        /// <summary>
        /// Reads the XMP metadata of the specified PDF and returns it as a dictionary.
        /// Each entry contains the metadata key and its string representation.
        /// </summary>
        /// <param name="pdfPath">Full path to the PDF file.</param>
        /// <returns>Dictionary with XMP keys and their corresponding values.</returns>
        public static Dictionary<string, string> GetMetadataDictionary(string pdfPath)
        {
            // Ensure the PDF file exists before attempting to bind.
            if (string.IsNullOrWhiteSpace(pdfPath))
                throw new ArgumentException("PDF path must be a non‑empty string.", nameof(pdfPath));

            // PdfXmpMetadata implements IDisposable via SaveableFacade, so use a using block.
            using (PdfXmpMetadata xmp = new PdfXmpMetadata())
            {
                // Bind the PDF document to the facade.
                xmp.BindPdf(pdfPath);

                // Prepare the result dictionary.
                var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

                // Iterate over all keys in the XMP dictionary.
                foreach (string key in xmp.Keys)
                {
                    // Retrieve the XmpValue for the current key.
                    XmpValue value = xmp[key];

                    // Convert the XmpValue to a readable string.
                    // For complex structures you could call value.ToDictionary() etc.,
                    // but for a simple helper we use ToString().
                    string stringValue = value?.ToString() ?? string.Empty;

                    result[key] = stringValue;
                }

                return result;
            }
        }
    }

    // ---------------------------------------------------------------------
    // Entry point required for a console‑style project.  The Main method does
    // not perform any work; it simply exists so the compiler can produce an
    // executable.  Users of the library can call XmpMetadataExtractor from
    // their own code or from this stub for quick testing.
    // ---------------------------------------------------------------------
    internal class Program
    {
        static void Main(string[] args)
        {
            // Optional quick‑test when the project is run directly.
            if (args.Length > 0)
            {
                try
                {
                    var dict = XmpMetadataExtractor.GetMetadataDictionary(args[0]);
                    foreach (var kvp in dict)
                    {
                        Console.WriteLine($"{kvp.Key}: {kvp.Value}");
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Usage: XmpHelper <pdf-path>");
            }
        }
    }
}
