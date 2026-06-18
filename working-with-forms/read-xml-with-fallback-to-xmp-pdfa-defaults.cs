using System;
using System.IO;
using System.Xml;
using Aspose.Pdf; // Provides XmpPdfAExtensionSchema constants

class XmpFallbackExample
{
    static void Main()
    {
        const string xmlPath = "source.xml";

        // Verify the XML source exists
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"File not found: {xmlPath}");
            return;
        }

        // Load the XML document
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(xmlPath);

        // Retrieve values with fallbacks to the Aspose.Pdf defaults
        string valueNamespaceUri = GetValueOrDefault(
            xmlDoc,
            "//pdfaExtension/valueNamespaceUri",
            XmpPdfAExtensionSchema.DefaultValueNamespaceUri);

        string valueTypePrefix = GetValueOrDefault(
            xmlDoc,
            "//pdfaExtension/valueTypeNamespacePrefix",
            XmpPdfAExtensionSchema.DefaultValueTypeNamespacePrefix);

        string fieldNamespacePrefix = GetValueOrDefault(
            xmlDoc,
            "//pdfaExtension/fieldNamespacePrefix",
            XmpPdfAExtensionSchema.DefaultFieldNamespacePrefix);

        string fieldNamespaceUri = GetValueOrDefault(
            xmlDoc,
            "//pdfaExtension/fieldNamespaceUri",
            XmpPdfAExtensionSchema.DefaultFieldNamespaceUri);

        // Output the resolved values
        Console.WriteLine($"Value Namespace URI: {valueNamespaceUri}");
        Console.WriteLine($"Value Type Prefix: {valueTypePrefix}");
        Console.WriteLine($"Field Namespace Prefix: {fieldNamespacePrefix}");
        Console.WriteLine($"Field Namespace URI: {fieldNamespaceUri}");
    }

    // Returns the inner text of the node identified by xpath,
    // or the supplied default if the node is missing or empty.
    private static string GetValueOrDefault(XmlDocument doc, string xpath, string defaultValue)
    {
        XmlNode node = doc.SelectSingleNode(xpath);
        if (node == null || string.IsNullOrWhiteSpace(node.InnerText))
        {
            // Use the Aspose.Pdf constant as fallback
            return defaultValue;
        }
        return node.InnerText.Trim();
    }
}