using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfTemplatePath = "template.pdf";   // existing PDF (can be blank)
        const string xmlDefinitionPath = "formDefinition.xml"; // XML that defines XFA form fields
        const string outputPdfPath = "filled_form.pdf";

        // Verify input files exist
        if (!File.Exists(pdfTemplatePath))
        {
            Console.Error.WriteLine($"Template PDF not found: {pdfTemplatePath}");
            return;
        }
        if (!File.Exists(xmlDefinitionPath))
        {
            Console.Error.WriteLine($"XML definition not found: {xmlDefinitionPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(pdfTemplatePath))
        {
            // Load the XML that describes the form (XFA)
            XmlDocument xfaXml = new XmlDocument();
            xfaXml.Load(xmlDefinitionPath);

            // Assign the XFA data to the PDF form
            doc.Form.AssignXfa(xfaXml);

            // Set default values for each form field
            foreach (Field field in doc.Form.Fields)
            {
                // Example default: use the field's full name prefixed with "Default_"
                // Replace with any custom logic as needed
                field.Value = $"Default_{field.FullName}";
            }

            // Save the resulting PDF with populated form fields
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with form fields created and saved to '{outputPdfPath}'.");
    }
}