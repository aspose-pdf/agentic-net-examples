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
        const string inputPdf = "input.pdf";
        const string outputXml = "selected_fields.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Ensure the document actually contains a form (AcroForm).
            if (doc.Form == null)
            {
                Console.Error.WriteLine("The PDF does not contain any form fields.");
                return;
            }

            // Names of the form fields that should be exported.
            string[] fieldsToExport = { "FirstName", "LastName", "Email" };

            // Build a simple XML document that contains only the selected fields.
            XDocument xmlDoc = new XDocument(new XElement("FormData"));
            XElement root = xmlDoc.Root;

            foreach (string fieldName in fieldsToExport)
            {
                // Retrieve the field from the AcroForm by matching its PartialName.
                Field field = doc.Form.Fields.FirstOrDefault(f => f.PartialName == fieldName);
                if (field != null)
                {
                    // The Value property can be null; convert it to a string safely.
                    string value = field.Value?.ToString() ?? string.Empty;
                    root.Add(new XElement("Field",
                                         new XAttribute("Name", fieldName),
                                         new XAttribute("Value", value)));
                }
                else
                {
                    // If the field is missing we still add an empty entry so the output structure is predictable.
                    root.Add(new XElement("Field",
                                         new XAttribute("Name", fieldName),
                                         new XAttribute("Value", string.Empty)));
                }
            }

            // Save the generated XML to the specified file.
            xmlDoc.Save(outputXml);
        }

        Console.WriteLine($"Selected form fields have been exported to '{outputXml}'.");
    }
}
