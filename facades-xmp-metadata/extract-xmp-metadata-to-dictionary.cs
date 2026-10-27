using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

// ---------------------------------------------------------------------------
// Minimal stub for the Aspose.Pdf.Facades namespace so the project compiles
// without requiring the actual Aspose.Pdf NuGet package. In a real project
// you should reference the official Aspose.Pdf library which provides the
// PdfXmpMetadataExtractor class.
// ---------------------------------------------------------------------------
namespace Aspose.Pdf.Facades
{
    /// <summary>
    /// Stub implementation that mimics the behaviour of Aspose.Pdf.Facades.PdfXmpMetadataExtractor.
    /// The real class extracts the raw XMP XML string from a PDF file. This stub simply
    /// returns an empty string, which means the helper will return an empty dictionary
    /// when used with this stub.
    /// </summary>
    public class PdfXmpMetadataExtractor
    {
        /// <summary>
        /// Returns the XMP metadata XML contained in the specified PDF file.
        /// </summary>
        /// <param name="pdfPath">Path to the PDF file.</param>
        /// <returns>Raw XMP XML string or an empty string if none is found.</returns>
        public string ExtractXmpMetadata(string pdfPath)
        {
            // A real implementation would parse the PDF and return the XMP block.
            // For compilation purposes we return an empty string.
            return string.Empty;
        }
    }
}

public static class XmpHelper
{
    /// <summary>
    /// Extracts XMP metadata from a PDF file and returns a dictionary of simple key/value pairs.
    /// Only leaf elements (elements without child elements) are added to the dictionary.
    /// </summary>
    /// <param name="pdfPath">Full path to the PDF file.</param>
    /// <returns>Dictionary where the key is the element's local name and the value is its text content.</returns>
    public static Dictionary<string, string> ExtractXmpToDictionary(string pdfPath)
    {
        if (string.IsNullOrWhiteSpace(pdfPath))
            throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

        if (!File.Exists(pdfPath))
            throw new FileNotFoundException("PDF file not found.", pdfPath);

        // Use the (stubbed) Facades API to get the raw XMP XML string.
        var extractor = new Aspose.Pdf.Facades.PdfXmpMetadataExtractor();
        string xmpXml = extractor.ExtractXmpMetadata(pdfPath);

        var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        // If the PDF does not contain XMP metadata, return an empty dictionary.
        if (string.IsNullOrWhiteSpace(xmpXml))
            return result;

        // Parse the XML.
        XDocument doc = XDocument.Parse(xmpXml);

        // XMP uses RDF syntax; locate the rdf:Description element(s).
        XNamespace rdfNs = "http://www.w3.org/1999/02/22-rdf-syntax-ns#";

        foreach (var description in doc.Descendants(rdfNs + "Description"))
        {
            // Iterate over child elements of rdf:Description.
            foreach (var element in description.Elements())
            {
                // Skip elements that have further child elements (complex structures).
                if (element.HasElements)
                    continue;

                string key = element.Name.LocalName;
                string value = element.Value?.Trim();

                if (!string.IsNullOrEmpty(key) && !result.ContainsKey(key))
                {
                    result[key] = value ?? string.Empty;
                }
            }
        }

        return result;
    }
}

// ---------------------------------------------------------------------------
// Minimal entry point required by the project. In a library scenario this would
// not be needed, but the build configuration expects a static Main method.
// ---------------------------------------------------------------------------
public class Program
{
    public static void Main(string[] args)
    {
        // No operation – the purpose of this project is to provide the XmpHelper.
        // You can uncomment the lines below to see a quick demo when running the
        // compiled executable.
        //
        // if (args.Length > 0)
        // {
        //     var dict = XmpHelper.ExtractXmpToDictionary(args[0]);
        //     foreach (var kvp in dict)
        //         Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        // }
    }
}
