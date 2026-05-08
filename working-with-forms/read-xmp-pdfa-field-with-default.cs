using System;
using System.IO;
using System.Xml;
using Aspose.Pdf; // Core Aspose.Pdf namespace

// Minimal replacement for the missing XMP schema class.
// Provides the default namespace values used by PDF/A extension fields.
internal static class XmpPdfAExtensionSchema
{
    // Prefix used in example XML for PDF/A fields (commonly "pdfaField").
    public const string DefaultFieldNamespacePrefix = "pdfaField";

    // Namespace URI for PDF/A extension fields.
    public const string DefaultFieldNamespaceUri = "http://www.aiim.org/pdfa/ns/field#";

    // Fallback value namespace URI – used when a field is missing.
    public const string DefaultValueNamespaceUri = "http://www.aiim.org/pdfa/ns/value#";
}

public static class XmpFieldHelper
{
    /// <summary>
    /// Retrieves the value of an XMP PDF/A extension field from an XML document.
    /// If the field is missing, returns a fallback default value taken from the
    /// XmpPdfAExtensionSchema constants.
    /// </summary>
    /// <param name="xmlPath">Path to the source XML file.</param>
    /// <param name="fieldName">Name of the field to retrieve (case‑sensitive).</param>
    /// <returns>Field value if present; otherwise a default value.</returns>
    public static string GetFieldValueOrDefault(string xmlPath, string fieldName)
    {
        if (string.IsNullOrEmpty(xmlPath))
            throw new ArgumentException("XML path is required.", nameof(xmlPath));
        if (string.IsNullOrEmpty(fieldName))
            throw new ArgumentException("Field name is required.", nameof(fieldName));

        // If the file does not exist we cannot read any fields – return the default.
        if (!File.Exists(xmlPath))
            return XmpPdfAExtensionSchema.DefaultValueNamespaceUri;

        // Load the XML document. No special load options are required for plain XML.
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(xmlPath);

        // Build an XPath that looks for the element in the default field namespace.
        string nsPrefix = XmpPdfAExtensionSchema.DefaultFieldNamespacePrefix; // e.g., "pdfaField"
        string nsUri    = XmpPdfAExtensionSchema.DefaultFieldNamespaceUri;    // e.g., "http://www.aiim.org/pdfa/ns/field#"

        // Register the namespace so XPath can resolve it.
        XmlNamespaceManager nsMgr = new XmlNamespaceManager(xmlDoc.NameTable);
        nsMgr.AddNamespace(nsPrefix, nsUri);

        // XPath selects the element with the given local name inside the field namespace.
        string xpath = $"//{nsPrefix}:{fieldName}";
        XmlNode? node = xmlDoc.SelectSingleNode(xpath, nsMgr); // nullable to silence CS8600

        if (node != null && !string.IsNullOrWhiteSpace(node.InnerText))
        {
            // Field exists – return its trimmed text.
            return node.InnerText.Trim();
        }

        // Field not found – fall back to the SDK‑provided default value namespace URI.
        return XmpPdfAExtensionSchema.DefaultValueNamespaceUri;
    }

    /// <summary>
    /// Example usage: reads a set of fields and prints their values (or defaults).
    /// </summary>
    public static void Demo()
    {
        // Assume we have an XML file that may or may not contain the fields.
        const string xmlFile = "metadata.xml";

        // Example field names defined in the PDF/A extension schema.
        string[] fields = { "Creator", "Producer", "CustomField" };

        foreach (string field in fields)
        {
            string value = GetFieldValueOrDefault(xmlFile, field);
            Console.WriteLine($"{field}: {value}");
        }
    }
}

// Entry point for a console application.
class Program
{
    static void Main()
    {
        // Run the demonstration.
        XmpFieldHelper.Demo();
    }
}
