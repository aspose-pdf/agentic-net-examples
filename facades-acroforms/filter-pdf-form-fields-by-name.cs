using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputXmlPath = "filtered.xml";

        // Verify that the source PDF exists before attempting to open it.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file '{inputPdfPath}' not found.");
            return;
        }

        // Initialize the Form facade with the source PDF.
        using (Form form = new Form(inputPdfPath))
        {
            // Export all form fields to an in‑memory XML stream.
            using (MemoryStream xmlStream = new MemoryStream())
            {
                form.ExportXml(xmlStream);
                xmlStream.Position = 0; // rewind for reading

                // Load the exported XML.
                XDocument xmlDoc = XDocument.Load(xmlStream);

                // Guard against a missing root element (should never happen for a valid export).
                if (xmlDoc.Root == null)
                {
                    throw new InvalidOperationException("Exported XML does not contain a root element.");
                }

                // Keep only fields whose names start with "Customer".
                // Use the null‑conditional operator to avoid CS8600/CS8602 warnings.
                var filteredRoot = new XElement(
                    xmlDoc.Root.Name,
                    xmlDoc.Root.Elements()
                        .Where(e => e.Attribute("name")?.Value?.StartsWith("Customer", StringComparison.Ordinal) ?? false)
                );

                // Preserve the XML declaration when saving.
                var filteredDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), filteredRoot);
                filteredDoc.Save(outputXmlPath);
            }
        }

        Console.WriteLine($"Filtered XML saved to '{outputXmlPath}'.");
    }
}
