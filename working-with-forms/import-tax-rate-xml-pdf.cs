using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string pdfInputPath   = "invoice.pdf";          // PDF with a hidden form field named "TaxRate"
        const string xmlInputPath   = "taxrates.xml";        // XML file containing tax rate information
        const string pdfOutputPath  = "invoice_with_tax.pdf";

        // Verify files exist
        if (!File.Exists(pdfInputPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfInputPath}");
            return;
        }
        if (!File.Exists(xmlInputPath))
        {
            Console.Error.WriteLine($"XML not found: {xmlInputPath}");
            return;
        }

        // Load tax rate from XML
        string taxRateValue = LoadTaxRateFromXml(xmlInputPath);

        // Open PDF, set hidden field, and save
        using (Document pdfDoc = new Document(pdfInputPath))
        {
            // Retrieve the hidden field (assumed to be a text field named "TaxRate")
            // Document.Form indexer returns a WidgetAnnotation, so we must cast it to Field.
            Field taxField = pdfDoc.Form["TaxRate"] as Field;
            if (taxField != null)
            {
                // Set the field value – this will be used in later calculations
                taxField.Value = taxRateValue;
            }
            else
            {
                Console.Error.WriteLine("Hidden field 'TaxRate' not found in the PDF form.");
            }

            // Save the updated PDF
            pdfDoc.Save(pdfOutputPath);
        }

        Console.WriteLine($"PDF saved with tax rate populated: {pdfOutputPath}");
    }

    // Helper method to extract the tax rate from the XML file
    private static string LoadTaxRateFromXml(string xmlPath)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(xmlPath);

        // Example XML structure:
        // <TaxInfo>
        //     <Rate>0.075</Rate>
        // </TaxInfo>
        // Adjust the XPath according to the actual XML schema.
        XmlNode rateNode = xmlDoc.SelectSingleNode("//TaxInfo/Rate");
        if (rateNode == null)
        {
            Console.Error.WriteLine("Tax rate element not found in XML.");
            return string.Empty;
        }

        return rateNode.InnerText.Trim();
    }
}