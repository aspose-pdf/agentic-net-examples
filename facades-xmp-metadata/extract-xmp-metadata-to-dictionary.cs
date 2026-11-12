using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

public static class XmpHelper
{
    /// <summary>
    /// Extracts XMP metadata from a PDF and returns a flat dictionary of key/value pairs.
    /// </summary>
    /// <param name="pdfPath">Path to the PDF file.</param>
    /// <returns>Dictionary where the key is the metadata name and the value is the metadata value.</returns>
    public static Dictionary<string, string> GetXmpMetadata(string pdfPath)
    {
        if (string.IsNullOrWhiteSpace(pdfPath))
            throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

        if (!File.Exists(pdfPath))
            throw new FileNotFoundException("PDF file not found.", pdfPath);

        // Load the PDF document. Aspose.Pdf.Document exposes the XMP metadata via the Metadata dictionary.
        using (var doc = new Document(pdfPath))
        {
            // If the PDF does not contain XMP metadata, return an empty dictionary.
            if (doc.Metadata == null || doc.Metadata.Count == 0)
                return new Dictionary<string, string>();

            // Copy the metadata entries into a new dictionary to avoid exposing the internal collection.
            var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (var kvp in doc.Metadata)
            {
                // Convert the XmpValue to its string representation. XmpValue implements ToString() which returns the underlying value.
                string value = kvp.Value?.ToString() ?? string.Empty;

                // Avoid duplicate keys – keep the first occurrence.
                if (!result.ContainsKey(kvp.Key))
                    result.Add(kvp.Key, value);
            }

            return result;
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Example usage – replace with your own PDF path or pass it via command‑line arguments.
        string pdfPath = args.Length > 0 ? args[0] : "sample.pdf";

        try
        {
            var metadata = XmpHelper.GetXmpMetadata(pdfPath);
            if (metadata.Count == 0)
            {
                Console.WriteLine("No XMP metadata found in the PDF.");
            }
            else
            {
                foreach (var kvp in metadata)
                {
                    Console.WriteLine($"{kvp.Key} = {kvp.Value}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}