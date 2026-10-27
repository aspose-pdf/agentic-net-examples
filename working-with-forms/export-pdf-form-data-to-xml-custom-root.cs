using System;
using System.IO;
using System.Xml.Linq;
using System.Linq; // Needed for Count() extension method
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // source PDF containing form fields
        const string outputXmlPath = "formData.xml";       // destination XML file
        const string customRoot = "ExternalSystemRoot";   // required root element name

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF inside a using block to ensure deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            var form = pdfDoc.Form;
            // Use Count() (extension method) because FormFieldCollection exposes Count as a method, not a property
            if (form == null || form.Fields == null || form.Fields.Count() == 0)
            {
                Console.WriteLine("No form fields found in the PDF.");
                return;
            }

            // Build XML manually using the Fields collection
            var root = new XElement(customRoot);
            foreach (var field in form.Fields)
            {
                // Guard against possible nulls on Name / FullName
                string fieldName = field?.Name ?? field?.FullName ?? "UnnamedField";
                string fieldValue = field?.Value?.ToString() ?? string.Empty;
                root.Add(new XElement(fieldName, fieldValue));
            }

            var xmlDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), root);
            xmlDoc.Save(outputXmlPath);
        }

        Console.WriteLine($"Form data exported to XML with root '{customRoot}' at '{outputXmlPath}'.");
    }
}
