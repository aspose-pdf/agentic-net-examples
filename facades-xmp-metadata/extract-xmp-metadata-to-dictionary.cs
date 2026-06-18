using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf;

public static class XmpHelper
{
    /// <summary>
    /// Extracts XMP metadata from a PDF and returns it as a dictionary of string key/value pairs.
    /// </summary>
    /// <param name="pdfPath">Path to the PDF file.</param>
    /// <returns>Dictionary where each key is the XMP property name and the value is its string representation.</returns>
    public static Dictionary<string, string> GetXmpMetadataDictionary(string pdfPath)
    {
        // Ensure the PDF file exists before attempting to bind.
        if (!File.Exists(pdfPath))
            throw new FileNotFoundException($"PDF file not found: {pdfPath}");

        // PdfXmpMetadata implements IDisposable, so wrap it in a using block for deterministic cleanup.
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            // Bind the XMP facade to the target PDF.
            xmp.BindPdf(pdfPath);

            // Prepare the result dictionary.
            var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            // PdfXmpMetadata implements IDictionary<string, XmpValue>, so we can enumerate its entries directly.
            foreach (KeyValuePair<string, XmpValue> entry in xmp)
            {
                // Convert the XmpValue to a readable string.
                // XmpValue provides ToStringValue() for a clean string representation.
                string valueString = entry.Value?.ToStringValue() ?? string.Empty;

                // Add to the dictionary.
                result[entry.Key] = valueString;
            }

            return result;
        }
    }
}

// Dummy entry point required for a console‑type project. The helper can be used from other code or unit tests.
public class Program
{
    public static void Main(string[] args)
    {
        // No operation – this method only satisfies the compiler's requirement for an entry point.
        // Example usage (optional):
        // if (args.Length > 0)
        // {
        //     var metadata = XmpHelper.GetXmpMetadataDictionary(args[0]);
        //     foreach (var kv in metadata)
        //         Console.WriteLine($"{kv.Key}: {kv.Value}");
        // }
    }
}