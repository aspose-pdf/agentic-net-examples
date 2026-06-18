using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths to the source PDF (with a filled form) and the XML output file.
        const string inputPdfPath = "input.pdf";
        const string outputXmlPath = "tax_output.xml";

        // Verify that the PDF exists.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{inputPdfPath}'.");
            return;
        }

        // Open the PDF document.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Name of the form field that holds the calculated tax value.
            const string taxFieldName = "TaxAmount";

            // Retrieve the field from the document's form collection.
            // The field is expected to be a TextBoxField; adjust the cast if a different type is used.
            TextBoxField taxField = pdfDoc.Form[taxFieldName] as TextBoxField;

            if (taxField == null)
            {
                Console.Error.WriteLine($"Error: Form field '{taxFieldName}' not found or is not a TextBoxField.");
                return;
            }

            // Extract the value entered/calculated in the tax field.
            string taxValue = taxField.Value ?? string.Empty;

            // Build a simple XML document containing the tax value.
            XmlDocument xmlDoc = new XmlDocument();

            // Create the root element.
            XmlElement root = xmlDoc.CreateElement("FormData");
            xmlDoc.AppendChild(root);

            // Add the tax amount element.
            XmlElement taxElement = xmlDoc.CreateElement("TaxAmount");
            taxElement.InnerText = taxValue;
            root.AppendChild(taxElement);

            // Save the XML to the specified file.
            xmlDoc.Save(outputXmlPath);

            Console.WriteLine($"Tax value '{taxValue}' exported successfully to '{outputXmlPath}'.");
        }
    }
}