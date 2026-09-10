using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;
using Aspose.Pdf.Facades; // PdfXmpMetadata resides here

public static class XmpHelper
{
    /// <summary>
    /// Extracts XMP metadata from a PDF and returns it as a dictionary of string keys and string values.
    /// </summary>
    /// <param name="pdfPath">Path to the source PDF file.</param>
    /// <returns>Dictionary where each key is the XMP property name and each value is its string representation.</returns>
    public static Dictionary<string, string> GetXmpMetadata(string pdfPath)
    {
        // Validate input.
        if (string.IsNullOrWhiteSpace(pdfPath) || !File.Exists(pdfPath))
            throw new ArgumentException("PDF file not found.", nameof(pdfPath));

        // PdfXmpMetadata implements IDisposable, so use a using block.
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            // Bind the PDF document to the facade.
            xmp.BindPdf(pdfPath);

            // Retrieve the raw XMP XML bytes.
            byte[] rawBytes = xmp.GetXmpMetadata();
            if (rawBytes == null || rawBytes.Length == 0)
                return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            // Convert the byte array to a UTF‑8 string.
            string rawXml = Encoding.UTF8.GetString(rawBytes);
            if (string.IsNullOrWhiteSpace(rawXml))
                return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            // Parse the XML and flatten it into a dictionary.
            var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            XDocument doc = XDocument.Parse(rawXml);

            // Walk through all leaf elements (elements that contain only text and no child elements).
            foreach (var element in doc.Descendants())
            {
                if (!element.HasElements)
                {
                    // Use the element's fully qualified name (including namespace prefix if any) as the key.
                    string key = element.Name.LocalName;
                    // If the element belongs to a namespace, prepend the prefix for clarity.
                    if (!string.IsNullOrEmpty(element.Name.NamespaceName))
                    {
                        // Try to get the prefix from the document's namespace declarations.
                        string prefix = element.GetPrefixOfNamespace(element.Name.Namespace);
                        if (!string.IsNullOrEmpty(prefix))
                            key = $"{prefix}:{key}";
                    }
                    string value = element.Value ?? string.Empty;
                    // Avoid duplicate keys – later values overwrite earlier ones.
                    result[key] = value;
                }
            }

            return result;
        }
    }
}

// Minimal entry point required for a console‑application project.
public static class Program
{
    public static void Main(string[] args)
    {
        // Optional demo: if a PDF path is supplied, print its XMP metadata.
        if (args.Length > 0 && File.Exists(args[0]))
        {
            var metadata = XmpHelper.GetXmpMetadata(args[0]);
            foreach (var kvp in metadata)
            {
                Console.WriteLine($"{kvp.Key} = {kvp.Value}");
            }
        }
    }
}