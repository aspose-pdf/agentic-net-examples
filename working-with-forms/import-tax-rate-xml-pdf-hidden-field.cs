using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "invoice.pdf";               // existing PDF with a hidden field
        const string xmlPath = "taxrates.xml";              // XML file containing tax rates
        const string outputPdfPath = "invoice_with_tax.pdf"; // result PDF

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

        // Load the PDF document inside a using block (document disposal rule)
        using (Document pdfDoc = new Document(pdfPath))
        {
            // -----------------------------------------------------------------
            // 1. Read tax rate from the XML file.
            // -----------------------------------------------------------------
            // Example XML structure:
            // <TaxInfo>
            //     <Rate>0.075</Rate>
            // </TaxInfo>
            // Adjust the XPath according to the actual XML schema.
            double taxRate = 0.0;
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath);
                XmlNode rateNode = xmlDoc.SelectSingleNode("//Rate");
                if (rateNode != null && double.TryParse(rateNode.InnerText, out double parsedRate))
                {
                    taxRate = parsedRate;
                }
                else
                {
                    Console.Error.WriteLine("Tax rate not found or invalid in XML.");
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error reading XML: {ex.Message}");
                return;
            }

            // -----------------------------------------------------------------
            // 2. Populate the hidden form field with the tax rate.
            // -----------------------------------------------------------------
            // Assume the hidden field is a text field named "TaxRate".
            // The field must exist in the PDF; otherwise, create it.
            const string hiddenFieldName = "TaxRate";

            // Try to get the field; if it does not exist, create a new one.
            Field taxField = pdfDoc.Form[hiddenFieldName] as Field;
            if (taxField == null)
            {
                // Create a new text box field. The constructor expects a Document, not a Form.
                var textBoxField = new TextBoxField(pdfDoc)
                {
                    PartialName = hiddenFieldName
                };
                // Adding the field to the form registers it; we do not add a visual widget
                // to any page, so the field remains hidden.
                pdfDoc.Form.Add(textBoxField);
                taxField = textBoxField;
            }

            // Set the field value (as string) – this value will be used in calculations.
            taxField.Value = taxRate.ToString("F4"); // e.g., "0.0750"

            // -----------------------------------------------------------------
            // 3. Save the updated PDF.
            // -----------------------------------------------------------------
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with tax rate populated: {outputPdfPath}");
    }
}
