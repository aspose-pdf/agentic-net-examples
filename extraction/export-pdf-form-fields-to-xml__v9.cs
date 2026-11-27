using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "FormH.pdf";
        const string xmlPath = "FormH_fields.xml";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Create the root XML element
                XElement root = new XElement("FormFields");

                // Enumerate all form fields in the document
                foreach (var field in doc.Form.Fields)
                {
                    // Retrieve field name and its current value
                    string name = field.FullName;
                    string value = field.Value?.ToString() ?? string.Empty;

                    // Add an XML element representing the field
                    XElement fieldElement = new XElement("Field",
                        new XAttribute("Name", name),
                        new XAttribute("Value", value));

                    root.Add(fieldElement);
                }

                // Build the XML document and save it to disk
                XDocument xmlDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), root);
                xmlDoc.Save(xmlPath);
            }

            Console.WriteLine($"Form fields exported to '{xmlPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}