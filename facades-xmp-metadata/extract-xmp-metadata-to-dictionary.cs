using System;
using System.Collections.Generic;
using Aspose.Pdf.Facades; // PdfXmpMetadata resides here
using Aspose.Pdf;          // XmpValue resides here

public static class XmpHelper
{
    /// <summary>
    /// Extracts XMP metadata from a PDF and returns it as a dictionary of string key/value pairs.
    /// </summary>
    /// <param name="pdfPath">Full path to the source PDF file.</param>
    /// <returns>Dictionary where each key is an XMP property name and the value is its string representation.</returns>
    public static Dictionary<string, string> ExtractMetadata(string pdfPath)
    {
        // Prepare the result container
        var metadata = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        // Use the PdfXmpMetadata facade to bind to the PDF (load operation)
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(pdfPath); // loads the PDF for XMP processing

            // PdfXmpMetadata implements IDictionary<string, XmpValue>
            // Iterate over all keys and retrieve their corresponding values
            foreach (string key in xmp.Keys)
            {
                // Retrieve the XmpValue; it may be null for missing entries
                XmpValue value = xmp[key];

                // Convert the XmpValue to a readable string.
                // XmpValue overrides ToString() to provide a suitable representation.
                string stringValue = value?.ToString() ?? string.Empty;

                // Store in the result dictionary
                metadata[key] = stringValue;
            }
        } // Dispose of the facade automatically

        return metadata;
    }
}

// Dummy entry point to satisfy the compiler when building as an executable.
public class Program
{
    public static void Main(string[] args)
    {
        // Optional demonstration (can be removed in production).
        // if (args.Length > 0)
        // {
        //     var dict = XmpHelper.ExtractMetadata(args[0]);
        //     foreach (var kvp in dict)
        //         Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        // }
    }
}