using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXml = "selected_fields.xml";

        // List the exact form field names you want to export
        string[] fieldsToExport = new[] { "FirstName", "LastName", "Email" };

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Export only the specified fields to XML (custom implementation)
                ExportSelectedFieldsToXml(doc, fieldsToExport, outputXml);
            }

            Console.WriteLine($"Selected form fields exported to '{outputXml}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Writes an XML file that contains only the form fields whose names are present in <paramref name="fieldNames"/>.
    /// The XML structure is simple: <Fields><Field name=\"fieldName\">fieldValue</Field>...</Fields>
    /// </summary>
    private static void ExportSelectedFieldsToXml(Document doc, IEnumerable<string> fieldNames, string outputPath)
    {
        if (doc?.Form?.Fields == null)
            throw new InvalidOperationException("The document does not contain a form.");

        // Build a hash‑set for fast lookup of the requested field names (case‑sensitive as Aspose stores them)
        var wanted = new HashSet<string>(fieldNames);

        // Create the root element
        var root = new XElement("Fields");

        foreach (Field field in doc.Form.Fields)
        {
            // Some fields may have a FullName; fall back to Name if FullName is null
            string name = field.FullName ?? field.Name;
            if (name == null || !wanted.Contains(name))
                continue;

            // The Value property can be null; convert to string safely
            string value = field.Value?.ToString() ?? string.Empty;
            var fieldElement = new XElement("Field", new XAttribute("name", name)) { Value = value };
            root.Add(fieldElement);
        }

        // Save the XML document to the specified file path
        var xmlDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), root);
        xmlDoc.Save(outputPath);
    }
}
