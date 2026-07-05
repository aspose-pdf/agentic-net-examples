using System;
using System.IO;
using System.Xml;
using Aspose.Pdf; // Provides XmpPdfAExtensionSchema and related types

class Program
{
    static void Main()
    {
        const string xmlPath = "source.xml";
        const string outputPath = "output.xml";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"File not found: {xmlPath}");
            return;
        }

        // Load the source XML document
        XmlDocument sourceDoc = new XmlDocument();
        sourceDoc.Load(xmlPath);

        // Create a new XML document that will contain the result (with fallbacks)
        XmlDocument resultDoc = new XmlDocument();
        XmlElement root = resultDoc.CreateElement("PdfAExtension");
        resultDoc.AppendChild(root);

        // Example: read a custom field "ValueNamespaceUri"
        // If the element is missing, use the default constant from XmpPdfAExtensionSchema
        string valueNamespaceUri = GetElementValueOrDefault(
            sourceDoc,
            "//ValueNamespaceUri",
            XmpPdfAExtensionSchema.DefaultValueNamespaceUri);

        // Example: read a custom field "ValueTypeNamespacePrefix"
        string valueTypeNamespacePrefix = GetElementValueOrDefault(
            sourceDoc,
            "//ValueTypeNamespacePrefix",
            XmpPdfAExtensionSchema.DefaultValueTypeNamespacePrefix);

        // Example: read a custom field "FieldNamespacePrefix"
        string fieldNamespacePrefix = GetElementValueOrDefault(
            sourceDoc,
            "//FieldNamespacePrefix",
            XmpPdfAExtensionSchema.DefaultFieldNamespacePrefix);

        // Example: read a custom field "FieldNamespaceUri"
        string fieldNamespaceUri = GetElementValueOrDefault(
            sourceDoc,
            "//FieldNamespaceUri",
            XmpPdfAExtensionSchema.DefaultFieldNamespaceUri);

        // Populate the result XML with the resolved values
        AppendElement(resultDoc, root, "ValueNamespaceUri", valueNamespaceUri);
        AppendElement(resultDoc, root, "ValueTypeNamespacePrefix", valueTypeNamespacePrefix);
        AppendElement(resultDoc, root, "FieldNamespacePrefix", fieldNamespacePrefix);
        AppendElement(resultDoc, root, "FieldNamespaceUri", fieldNamespaceUri);

        // Save the resulting XML (fallback values are now present)
        resultDoc.Save(outputPath);
        Console.WriteLine($"Processed XML saved to '{outputPath}'.");
    }

    /// <summary>
    /// Retrieves the inner text of the first XML node matching the XPath expression.
    /// If the node does not exist or its value is empty, returns the provided fallback.
    /// </summary>
    static string GetElementValueOrDefault(XmlDocument doc, string xpath, string fallback)
    {
        XmlNode node = doc.SelectSingleNode(xpath);
        if (node == null || string.IsNullOrWhiteSpace(node.InnerText))
        {
            // Use the default constant from Aspose.Pdf.XmpPdfAExtensionSchema
            return fallback;
        }
        return node.InnerText.Trim();
    }

    /// <summary>
    /// Helper to create and append a child element with a text value.
    /// </summary>
    static void AppendElement(XmlDocument doc, XmlElement parent, string name, string value)
    {
        XmlElement elem = doc.CreateElement(name);
        elem.InnerText = value;
        parent.AppendChild(elem);
    }
}