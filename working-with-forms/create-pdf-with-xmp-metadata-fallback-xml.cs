using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source XML file
        const string xmlPath = "source.xml";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML document
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(xmlPath);

        // Retrieve a field named "Author" from the XML.
        // If the field is missing, use a fallback default value.
        string author = GetFieldValueOrDefault(xmlDoc, "Author", "Unknown Author");

        // Create a PDF and embed the author value into its XMP metadata.
        using (Document pdfDoc = new Document())
        {
            // Ensure the PDF has at least one page.
            pdfDoc.Pages.Add();

            // Register a custom namespace (optional – you can use an existing one).
            pdfDoc.Metadata.RegisterNamespaceUri("my", "http://example.com/custom");

            // Add a custom XMP property using the metadata indexer.
            pdfDoc.Metadata["my:Author"] = author;

            // Save the PDF.
            pdfDoc.Save("output.pdf");
        }

        Console.WriteLine("PDF created with fallback XMP field values.");
    }

    /// <summary>
    /// Retrieves the inner text of an XML element with the specified name.
    /// If the element does not exist, returns the provided fallback value.
    /// </summary>
    /// <param name="xmlDoc">The loaded XmlDocument.</param>
    /// <param name="elementName">The name of the element to search for.</param>
    /// <param name="fallback">The value to return if the element is missing.</param>
    /// <returns>The element's text or the fallback.</returns>
    private static string GetFieldValueOrDefault(XmlDocument xmlDoc, string elementName, string fallback)
    {
        // Attempt to find the element anywhere in the document.
        XmlNode node = xmlDoc.SelectSingleNode($"//{elementName}");
        if (node != null && !string.IsNullOrWhiteSpace(node.InnerText))
        {
            return node.InnerText.Trim();
        }

        // Return the fallback default.
        return fallback;
    }
}
