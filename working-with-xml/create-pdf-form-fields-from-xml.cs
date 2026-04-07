using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input PDF (can be an empty PDF or a template)
        const string inputPdfPath  = "template.pdf";
        // XML file that defines the form fields (XFA)
        const string formXmlPath   = "formDefinition.xml";
        // Output PDF with form fields and default values
        const string outputPdfPath = "filled_form.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(formXmlPath))
        {
            Console.Error.WriteLine($"Form XML not found: {formXmlPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Load the XML that describes the form (XFA)
            XmlDocument xfaXml = new XmlDocument();
            xfaXml.Load(formXmlPath);

            // Assign the XFA data to the document's form
            pdfDoc.Form.AssignXfa(xfaXml);

            // Example: set default values for specific fields
            // The dictionary maps field full names to their default values
            var defaultValues = new Dictionary<string, string>
            {
                { "FirstName", "John" },
                { "LastName",  "Doe" },
                { "Email",     "john.doe@example.com" },
                { "Country",   "USA" }
            };

            // Iterate over the fields and assign values where a default is defined
            foreach (Field field in pdfDoc.Form.Fields)
            {
                if (defaultValues.TryGetValue(field.FullName, out string value))
                {
                    field.Value = value;
                }
            }

            // Save the resulting PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with form fields saved to '{outputPdfPath}'.");
    }
}