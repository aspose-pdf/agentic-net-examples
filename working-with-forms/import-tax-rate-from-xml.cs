using System;
using System.IO;
using System.Xml;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xmlPath = "taxrates.xml";
        const string outputPath = "output.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML not found: {xmlPath}");
            return;
        }

        // Load the tax rate value from the XML file.
        string taxRateValue = LoadTaxRateFromXml(xmlPath);

        // Open the PDF, set the hidden form field, and save the result.
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Verify that the PDF contains a form and the expected hidden field.
            if (pdfDoc.Form != null && pdfDoc.Form.Fields != null && pdfDoc.Form.Fields.Any(f => f.Name == "TaxRate"))
            {
                // Retrieve the field and set its value. The Form indexer returns a WidgetAnnotation, so we cast it to Field.
                Field taxField = pdfDoc.Form["TaxRate"] as Field;
                if (taxField != null)
                {
                    taxField.Value = taxRateValue;
                }
                else
                {
                    Console.Error.WriteLine("Unable to retrieve the 'TaxRate' field instance or the field is not a form field.");
                }
            }
            else
            {
                Console.Error.WriteLine("Hidden field 'TaxRate' not found in the PDF form.");
            }

            // Save the modified PDF.
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with tax rate field populated: {outputPath}");
    }

    // Reads the tax rate from an XML file.
    // Expected XML format:
    // <TaxRates>
    //     <Rate>0.07</Rate>
    // </TaxRates>
    static string LoadTaxRateFromXml(string xmlFile)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(xmlFile);
        XmlNode rateNode = xmlDoc.SelectSingleNode("//TaxRates/Rate");
        return rateNode?.InnerText ?? string.Empty;
    }
}
