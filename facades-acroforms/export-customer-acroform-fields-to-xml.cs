using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXml = "customer_fields.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Initialize the Form facade on the loaded document
            using (Form form = new Form(doc))
            {
                // Prepare an XML document to hold the filtered fields
                XmlDocument xmlDoc = new XmlDocument();
                XmlElement root = xmlDoc.CreateElement("Fields");
                xmlDoc.AppendChild(root);

                // Iterate over all form field names
                foreach (string fieldName in form.FieldNames)
                {
                    // Keep only fields whose names start with "Customer"
                    if (fieldName.StartsWith("Customer", StringComparison.OrdinalIgnoreCase))
                    {
                        // Retrieve the field value; GetField may return null
                        object rawValue = form.GetField(fieldName);
                        string value = rawValue?.ToString() ?? string.Empty;

                        // Create an XML element for the field
                        XmlElement fieldElem = xmlDoc.CreateElement("Field");
                        fieldElem.SetAttribute("Name", fieldName);
                        fieldElem.InnerText = value;
                        root.AppendChild(fieldElem);
                    }
                }

                // Write the filtered XML to the output file
                using (FileStream fs = new FileStream(outputXml, FileMode.Create, FileAccess.Write))
                {
                    xmlDoc.Save(fs);
                }
            }
        }

        Console.WriteLine($"Filtered fields exported to '{outputXml}'.");
    }
}