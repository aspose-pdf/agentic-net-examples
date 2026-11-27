using System;
using System.IO;
using Aspose.Pdf;
using System.Xml.Linq;

class Program
{
    static void Main()
    {
        const string inputPath  = "FormA.pdf";
        const string outputPath = "FormA_fields.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create the root element for the XML output
            XElement root = new XElement("FormFields");

            // Enumerate all form fields in the document
            foreach (var field in doc.Form.Fields)
            {
                // Retrieve field name (FullName if available, otherwise Name)
                string name = field.FullName ?? field.Name;

                // Retrieve field value as string (handle nulls)
                string value = field.Value?.ToString() ?? string.Empty;

                // Add a <Field Name="..." Value="..."/> element to the XML tree
                XElement fieldElement = new XElement("Field",
                    new XAttribute("Name", name),
                    new XAttribute("Value", value));
                root.Add(fieldElement);
            }

            // Build the XDocument and save it to the specified XML file
            XDocument xmlDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), root);
            xmlDoc.Save(outputPath);
        }

        Console.WriteLine($"Form field data exported to '{outputPath}'.");
    }
}