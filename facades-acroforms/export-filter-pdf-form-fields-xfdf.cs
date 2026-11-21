using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xmlPath = "filtered_fields.xml";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(pdfPath))
        {
            // Access the form fields collection (fully qualified to avoid ambiguity)
            Aspose.Pdf.Forms.Form pdfForm = doc.Form;

            // Prepare an XML document to hold the filtered fields
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement root = xmlDoc.CreateElement("FormFields");
            xmlDoc.AppendChild(root);

            // Iterate over all form fields
            foreach (var field in pdfForm.Fields)
            {
                // Get the field's full name
                string fieldName = field.FullName;

                // Include only fields whose names start with "Customer"
                if (!string.IsNullOrEmpty(fieldName) &&
                    fieldName.StartsWith("Customer", StringComparison.OrdinalIgnoreCase))
                {
                    // Retrieve the field's value as a string
                    string fieldValue = field.Value?.ToString() ?? string.Empty;

                    // Create an XML element for this field
                    XmlElement fieldElem = xmlDoc.CreateElement("Field");
                    fieldElem.SetAttribute("Name", fieldName);
                    fieldElem.SetAttribute("Value", fieldValue);
                    root.AppendChild(fieldElem);
                }
            }

            // Save the filtered fields to XML
            xmlDoc.Save(xmlPath);
        }

        Console.WriteLine($"Filtered form fields saved to '{xmlPath}'.");
    }
}