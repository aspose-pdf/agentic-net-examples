using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputXmlPath = "selected_fields.xml";

        // Names of the form fields to export
        string[] fieldNames = new string[]
        {
            "FirstName",
            "LastName",
            "Email"
        };

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Build an XML document that contains only the selected fields
            XElement root = new XElement("FormData",
                fieldNames.Select(name =>
                {
                    // Find the field with the given name
                    var field = pdfDoc.Form?.Fields?.FirstOrDefault(f => f.Name == name);
                    // Get its value (or empty string if not present)
                    string value = field?.Value?.ToString() ?? string.Empty;
                    // Create an element for this field
                    return new XElement("Field",
                        new XAttribute("name", name),
                        value);
                })
            );

            // Save the XML to the requested file path
            root.Save(outputXmlPath);
        }

        Console.WriteLine($"Selected form fields exported to '{outputXmlPath}'.");
    }
}
