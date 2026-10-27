using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "form.pdf";
        const string xmlOutput = "formData.xml";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Load the PDF containing the form
        using (Document doc = new Document(pdfPath))
        {
            // Example: fill some form fields before exporting
            if (doc.Form != null && doc.Form.Count > 0)
            {
                // Text box field named "Name"
                if (doc.Form["Name"] is TextBoxField nameField)
                    nameField.Value = "John Doe";

                // Check box field named "Subscribe"
                if (doc.Form["Subscribe"] is CheckboxField subscribeField)
                    subscribeField.Checked = true;
            }

            // Ensure the directory for the XML file exists
            string xmlDir = Path.GetDirectoryName(xmlOutput);
            if (!string.IsNullOrEmpty(xmlDir) && !Directory.Exists(xmlDir))
                Directory.CreateDirectory(xmlDir);

            // Export form data to a local XML file (offline collection)
            ExportFormDataToXml(doc, xmlOutput);
        }

        Console.WriteLine($"Form data exported to {xmlOutput}");
    }

    private static void ExportFormDataToXml(Document doc, string filePath)
    {
        var root = new XElement("FormData");

        if (doc?.Form?.Fields != null)
        {
            foreach (var field in doc.Form.Fields)
            {
                string name = field.Name ?? field.FullName ?? "Unnamed";
                string value = field.Value?.ToString() ?? string.Empty;

                var fieldElement = new XElement("Field",
                    new XAttribute("Name", name),
                    new XAttribute("Type", field.GetType().Name),
                    new XAttribute("Value", value));

                root.Add(fieldElement);
            }
        }

        var xmlDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), root);
        xmlDoc.Save(filePath);
    }
}