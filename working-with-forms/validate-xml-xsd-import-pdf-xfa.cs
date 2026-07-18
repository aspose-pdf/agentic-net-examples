using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    // Validates an XML file against an XSD schema.
    // Returns true if valid; otherwise false and fills validationErrors with details.
    static bool ValidateXml(string xmlPath, string xsdPath, out string validationErrors)
    {
        bool isValid = true;
        StringBuilder errors = new StringBuilder();

        XmlReaderSettings settings = new XmlReaderSettings();
        settings.Schemas.Add(null, xsdPath);
        settings.ValidationType = ValidationType.Schema;
        settings.ValidationEventHandler += (sender, e) =>
        {
            isValid = false;
            errors.AppendLine(e.Message);
        };

        using (XmlReader reader = XmlReader.Create(xmlPath, settings))
        {
            try
            {
                while (reader.Read()) { }
            }
            catch (XmlException ex)
            {
                isValid = false;
                errors.AppendLine(ex.Message);
            }
        }

        validationErrors = errors.ToString();
        return isValid;
    }

    static void Main()
    {
        const string pdfPath = "form.pdf";          // Existing PDF with a form (XFA)
        const string xmlPath = "data.xml";          // XML data to import
        const string xsdPath = "schema.xsd";        // XSD schema for validation
        const string outputPdf = "filled_form.pdf"; // Resulting PDF

        // Ensure all required files exist.
        if (!File.Exists(pdfPath) || !File.Exists(xmlPath) || !File.Exists(xsdPath))
        {
            Console.Error.WriteLine("One or more required files are missing.");
            return;
        }

        // Validate the XML against the XSD.
        if (!ValidateXml(xmlPath, xsdPath, out string validationErrors))
        {
            Console.Error.WriteLine("XML validation failed:");
            Console.Error.WriteLine(validationErrors);
            return;
        }

        // Load the PDF document.
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Load the XML into an XmlDocument.
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);

            // If the PDF contains an XFA form, assign the XML data.
            if (pdfDoc.Form != null && pdfDoc.Form.HasXfa)
            {
                pdfDoc.Form.AssignXfa(xmlDoc);
            }

            // Save the updated PDF.
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with imported data saved to '{outputPdf}'.");
    }
}